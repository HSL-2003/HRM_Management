using System;
using System.Collections.Generic;
using System.Text;

namespace Hrmanagement.HRManagement.Domain.Entities
{
    public class EmployeeInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; } = 0;
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get;set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }

        public DateTime HiredDate { get; set; }

       
        

    }
}
