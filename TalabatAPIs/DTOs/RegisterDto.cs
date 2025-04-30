using System.ComponentModel.DataAnnotations;

namespace TalabatAPIs.DTOs
{
    public class RegisterDto
    {
        public string DisplayName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
