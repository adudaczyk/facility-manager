using System.Collections.Generic;

namespace FacilityManager.BusinessLogic.Models
{
    public class UserDto : EntityDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsEmailVerified { get; set; }
        public List<string> Roles { get; set; }
        public string VerificationEmailToken { get; set; }
        public string ResetPasswordToken { get; set; }
    }
}