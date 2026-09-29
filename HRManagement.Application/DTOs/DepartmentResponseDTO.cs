namespace hrmana.HRManagement.Application.DTOs
{
    public class DepartmentResponseDTO
    {
        public int id { get; set; }
        public string Name {get; set; }
        public string Description {get;set; }
        public int PositionCount {get; set; }
        public int EmployeeCount { get; set; }
    }
}
