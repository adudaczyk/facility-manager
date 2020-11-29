using System.Collections.Generic;

namespace FacilityManager.BusinessLogic.Models
{
    public class AccountDto : EntityDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmailVerified { get; set; }
        public List<string> Roles { get; set; }
        public string VerificationEmailToken { get; set; }
        public string ResetPasswordToken { get; set; }
    }
}