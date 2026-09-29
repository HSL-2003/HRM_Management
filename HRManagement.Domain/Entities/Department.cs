using System;
using System.Collections.Generic;
using System.Text;

namespace Hrmanagement.HRManagement.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Position> Positions { get; set; } = new List<Position>();
        public ICollection<EmployeeInfo> EmployeeInfos { get; set; } = new List<EmployeeInfo>();
    }

}
