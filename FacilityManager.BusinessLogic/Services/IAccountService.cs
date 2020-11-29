using FacilityManager.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityManager.BusinessLogic.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAccounts();
        Task<AccountDto> GetAccount(string guid);
        Task CreateAccount(AccountDto accountDto);
        Task UpdateAccount(AccountDto accountDto);
        Task DeleteAccount(string guid);
        Task SendResetPasswordLink(string email);
        Task ResetPassword(AccountDto accountDto);
        Task VerifyEmail(AccountDto accountDto);
        Task<bool> EmailLookup(string email);
    }
}