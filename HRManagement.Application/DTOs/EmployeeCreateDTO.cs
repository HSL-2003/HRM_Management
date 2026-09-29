namespace hrmana.HRManagement.Application.DTOs
{
    public class EmployeeCreateDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public DateTime HiredDate { get; set; }
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }
    }
}
