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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                await _userService.AddUser(userDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            var allUsers = await _userService.GetUsers();

            return Ok(allUsers);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetUser(string guid)
        {
            var user = await _userService.GetUser(guid);

            return Ok(user);
        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> UpdateUser(string guid, [FromBody] UserDto userDto)
        {
            userDto.Guid = new Guid(guid);
            try
            {
                await _userService.UpdateUser(userDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteUser(string guid)
        {
            try
            {
                await _userService.DeleteUser(guid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] UserDto userDto)
        {
            try
            {
                await _userService.VerifyEmail(userDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("send-reset-password-link")]
        public async Task<IActionResult> SendResetPasswordLink([FromBody] UserDto userDto)
        {
            try
            {
                await _userService.SendResetPasswordLink(userDto.Email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] UserDto userDto)
        {
            try
            {
                await _userService.ResetPassword(userDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
