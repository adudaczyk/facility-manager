using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FacilityManager.BusinessLogic.Services;
using FacilityManager.Api.Models.Requests;
using System.Security.Authentication;

namespace FacilityManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest authRequest)
        {
            var auth = await _authService.Authenticate(authRequest.Email, authRequest.Password, IPAddress());

            if (auth == null)
            {
                throw new ArgumentException("Email or password is incorrect");
            }

            SetTokenCookie(auth.RefreshToken);

            return Ok(auth);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var auth = await _authService.RefreshToken(refreshToken, IPAddress());

            if (auth == null)
            {
                throw new AuthenticationException("Invalid token");
            }

            SetTokenCookie(auth.RefreshToken);

            return Ok(auth);
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] string refreshToken)
        {
            // accept token from request body or cookie
            var token = refreshToken ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is required");
            }

            var response = await _authService.RevokeToken(token, IPAddress());

            if (!response)
            {
                throw new ArgumentException("Token not found");
            }

            return Ok("Token revoked");
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string IPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}