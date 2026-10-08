namespace hrmana.HRManagement.Application.DTOs
{
    public class UserResponseDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }

    public class UpdateUserDTO
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
