using AutoMapper;
using FacilityManager.BusinessLogic.Helpers;
using FacilityManager.BusinessLogic.Models;
using FacilityManager.EntityFramework.Models;
using FacilityManager.EntityFramework.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityManager.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUser(string guid)
        {
            var user = await _userRepository.GetByGuid(guid);

            return _mapper.Map<UserDto>(user);
        }

        public async Task AddUser(UserDto userDto)
        {
            if (await _userRepository.GetByEmail(userDto.Email) != null)
            {
                throw new ArgumentException($"User with email {userDto.Email} already exist!");
            }

            var user = _mapper.Map<User>(userDto);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

            user.Guid = Guid.NewGuid();
            user.CreationDate = DateTime.Now;
            user.IsEmailVerified = false;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Roles = Role.User;
            user.VerificationEmailToken = GenerateRandomToken();

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            var emailHelper = new EmailHelper(_configuration);
            emailHelper.SendUserVerificationEmail(user.Email, user.VerificationEmailToken);
        }

        public async Task UpdateUser(UserDto userDto)
        {
            var user = await _userRepository.GetByGuid(userDto.Guid.ToString());

            if (user == null)
            {
                throw new KeyNotFoundException($"Cannot update user. User with guid {user.Guid} does not exist");
            }

            _mapper.Map(userDto, user);

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteUser(string guid)
        {
            var user = await _userRepository.GetByGuid(guid);

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task VerifyEmail(string email, string token)
        {
            var user = await _userRepository.GetByGuid("");
            user.IsEmailVerified = true;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task SendResetPasswordLink(string email)
        {
            var user = await _userRepository.GetByEmail(email);

            if (user == null) throw new ArgumentException("Invalid email");

            var token = GenerateRandomToken();
            user.ResetPasswordToken = token;
            _userRepository.Update(user);

            var emailHelper = new EmailHelper(_configuration);
            emailHelper.SendResetPasswordLink(email, token);

            await _userRepository.SaveChangesAsync();
        }

        public async Task ResetPassword(UserDto userDto)
        {
            var user = await _userRepository.GetByEmail(userDto.Email);

            if (user == null || user.ResetPasswordToken != userDto.ResetPasswordToken) throw new ArgumentException("Invalid token");

            _mapper.Map(userDto, user);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ResetPasswordToken = null;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task VerifyEmail(UserDto userDto)
        {
            var user = await _userRepository.GetByEmail(userDto.Email);

            if (user == null || user.VerificationEmailToken != userDto.VerificationEmailToken) throw new ArgumentException("Invalid token");

            user.IsEmailVerified = true;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private string GenerateRandomToken()
        {
            var guid = Guid.NewGuid();
            var token = Convert.ToBase64String(guid.ToByteArray());
            token = token.Replace("+", "");
            token = token.Replace("=", "");

            return token;
        }
    }
}
