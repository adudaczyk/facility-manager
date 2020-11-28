using FacilityManager.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityManager.BusinessLogic.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> GetUser(string guid);
        Task AddUser(UserDto userDto);
        Task UpdateUser(UserDto userDto);
        Task DeleteUser(string guid);
        Task SendResetPasswordLink(string email);
        Task ResetPassword(UserDto userDto);
        Task VerifyEmail(UserDto userDto);
        Task<bool> EmailLookup(string email);
    }
}