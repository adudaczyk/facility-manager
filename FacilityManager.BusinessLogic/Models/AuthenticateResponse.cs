namespace FacilityManager.BusinessLogic.Models
{
    public class AuthenticateResponse
    {
        public string Email { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }

        public AuthenticateResponse(string email, string jwtToken, string refreshToken)
        {
            Email = email;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}