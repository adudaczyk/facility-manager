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
            try
            {
                await _accountService.CreateAccount(accountDto);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
            try
            {
                await _accountService.UpdateAccount(accountDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteAccount(string guid)
        {
            try
            {
                await _accountService.DeleteAccount(guid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] AccountDto accountDto)
        {
            try
            {
                await _accountService.VerifyEmail(accountDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("send-reset-password-link")]
        public async Task<IActionResult> SendResetPasswordLink(string email)
        {
            try
            {
                await _accountService.SendResetPasswordLink(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] AccountDto accountDto)
        {
            try
            {
                await _accountService.ResetPassword(accountDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("email-lookup")]
        public async Task<IActionResult> EmailLookup(string email)
        {
            try
            {
                var isEmailExisted = await _accountService.EmailLookup(email);
                return Ok(isEmailExisted);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
