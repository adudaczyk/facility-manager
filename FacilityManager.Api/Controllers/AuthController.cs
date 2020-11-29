using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FacilityManager.BusinessLogic.Models;
using FacilityManager.BusinessLogic.Services;

namespace FacilityManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;

        public AuthController(IAccountService accountService, IAuthService authService)
        {
            _accountService = accountService;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AccountDto accountDto)
        {
            var account = await _authService.Authenticate(accountDto.Email, accountDto.Password, ipAddress());

            if (account == null) return BadRequest(new { message = "Email or password is incorrect" });

            setTokenCookie(account.RefreshToken);

            return Ok(new
            {
                Email = account.Email,
                Token = account.JwtToken
            });
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var account = await _authService.RefreshToken(refreshToken, ipAddress());

            if (account == null) return Unauthorized(new { message = "Invalid token" });

            setTokenCookie(account.RefreshToken);

            return Ok(new
            {
                Email = account.Email,
                Token = account.JwtToken
            });
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] string refreshToken)
        {
            // accept token from request body or cookie
            var token = refreshToken ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token)) return BadRequest(new { message = "Token is required" });

            var response = await _authService.RevokeToken(token, ipAddress());

            if (!response) return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
