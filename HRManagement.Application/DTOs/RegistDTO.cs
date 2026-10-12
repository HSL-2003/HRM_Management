using System.ComponentModel.DataAnnotations;
namespace hrmana.HRManagement.Application.DTOs
{
    public class RegistDTO
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password),ErrorMessage ="Not The Original Password Cuzzz!")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}
