namespace hrmana.HRManagement.Application.DTOs
{
    public class EmployeeResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public DateTime HiredDate { get; set; }

    }
}
