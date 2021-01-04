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
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountService(IAccountRepository accountRepository, IMapper mapper, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<IEnumerable<AccountDto>> GetAccounts()
        {
            var accounts = await _accountRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<AccountDto>>(accounts);
        }

        public async Task<AccountDto> GetAccount(string guid)
        {
            var account = await _accountRepository.GetByGuid(guid);

            return _mapper.Map<AccountDto>(account);
        }

        public async Task CreateAccount(AccountDto accountDto)
        {
            if (await _accountRepository.GetByEmail(accountDto.Email) != null)
            {
                throw new ArgumentException($"Account with email '{accountDto.Email}' already exist!");
            }

            var account = _mapper.Map<Account>(accountDto);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(accountDto.Password, out passwordHash, out passwordSalt);

            account.Guid = Guid.NewGuid();
            account.CreationDate = DateTime.Now;
            account.IsEmailVerified = false;
            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;
            account.Roles = Role.User;
            account.VerificationEmailToken = GenerateRandomToken();

            _accountRepository.Add(account);
            await _accountRepository.SaveChangesAsync();

            // var emailHelper = new EmailHelper(_configuration);
            // emailHelper.SendAccountVerificationEmail(account.Email, account.VerificationEmailToken);
        }

        public async Task UpdateAccount(AccountDto accountDto)
        {
            var account = await _accountRepository.GetByGuid(accountDto.Guid.ToString());

            if (account == null)
            {
                throw new KeyNotFoundException($"Cannot update account. Account with guid '{account.Guid}' does not exist");
            }

            _mapper.Map(accountDto, account);

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(accountDto.Password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(accountDto.Password, out passwordHash, out passwordSalt);

                account.PasswordHash = passwordHash;
                account.PasswordSalt = passwordSalt;
            }

            _accountRepository.Update(account);
            await _accountRepository.SaveChangesAsync();
        }

        public async Task DeleteAccount(string guid)
        {
            var account = await _accountRepository.GetByGuid(guid);

            _accountRepository.Delete(account);
            await _accountRepository.SaveChangesAsync();
        }

        public async Task VerifyEmail(string email, string token)
        {
            var account = await _accountRepository.GetByGuid("");
            account.IsEmailVerified = true;

            _accountRepository.Update(account);
            await _accountRepository.SaveChangesAsync();
        }

        public async Task SendResetPasswordLink(string email)
        {
            var account = await _accountRepository.GetByEmail(email);

            if (account == null)
            {
                throw new ArgumentException("Invalid email");
            }

            var token = GenerateRandomToken();
            account.ResetPasswordToken = token;
            _accountRepository.Update(account);

            var emailHelper = new EmailHelper(_configuration);
            emailHelper.SendResetPasswordLink(email, token);

            await _accountRepository.SaveChangesAsync();
        }

        public async Task ResetPassword(AccountDto accountDto)
        {
            var account = await _accountRepository.GetByEmail(accountDto.Email);

            if (account == null || account.ResetPasswordToken != accountDto.ResetPasswordToken)
            {
                throw new ArgumentException("Invalid reset password token");
            }

            _mapper.Map(accountDto, account);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(accountDto.Password, out passwordHash, out passwordSalt);
            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;
            account.ResetPasswordToken = null;

            _accountRepository.Update(account);
            await _accountRepository.SaveChangesAsync();
        }

        public async Task VerifyEmail(AccountDto accountDto)
        {
            var account = await _accountRepository.GetByEmail(accountDto.Email);

            if (account == null || account.VerificationEmailToken != accountDto.VerificationEmailToken)
            {
                throw new ArgumentException("Invalid verification email token");
            }

            account.IsEmailVerified = true;
            _accountRepository.Update(account);
            await _accountRepository.SaveChangesAsync();
        }

        public async Task<bool> EmailLookup(string email)
        {
            var account = await _accountRepository.GetByEmail(email);

            return account != null ? true : false;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be empty or whitespace");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
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