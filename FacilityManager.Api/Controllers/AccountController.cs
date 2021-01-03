using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FacilityManager.BusinessLogic;
using FacilityManager.BusinessLogic.Models;
using FacilityManager.BusinessLogic.Services;

namespace FacilityManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountDto accountDto)
        {
                await _accountService.CreateAccount(accountDto);
                return Ok("Register successfull!");
        }

        [AllowAnonymous]
        [HttpGet("get-accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            var allAccounts = await _accountService.GetAccounts();

            return Ok(allAccounts);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetAccount(string guid)
        {
            var account = await _accountService.GetAccount(guid);

            return Ok(account);
        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> UpdateAccount(string guid, [FromBody] AccountDto accountDto)
        {
            accountDto.Guid = new Guid(guid);
            await _accountService.UpdateAccount(accountDto);
            return Ok();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteAccount(string guid)
        {
            await _accountService.DeleteAccount(guid);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] AccountDto accountDto)
        {
            await _accountService.VerifyEmail(accountDto);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("send-reset-password-link")]
        public async Task<IActionResult> SendResetPasswordLink(string email)
        {
            await _accountService.SendResetPasswordLink(email);
            return Ok();
            
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] AccountDto accountDto)
        {
            await _accountService.ResetPassword(accountDto);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("email-lookup")]
        public async Task<IActionResult> EmailLookup(string email)
        {
            var isEmailExisted = await _accountService.EmailLookup(email);
            return Ok(isEmailExisted);
        }
    }
}