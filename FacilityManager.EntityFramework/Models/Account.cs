using System.Collections.Generic;

namespace FacilityManager.EntityFramework.Models
{
    public class Account : Entity
    {
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Roles { get; set; }

        public bool IsEmailVerified { get; set; }

        public string VerificationEmailToken { get; set; }

        public string ResetPasswordToken { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}