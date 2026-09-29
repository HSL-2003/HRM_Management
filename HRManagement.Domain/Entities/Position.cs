using System;
using System.Collections.Generic;
using System.Text;

namespace Hrmanagement.HRManagement.Domain.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public Department Department { get; set; }
        public ICollection<EmployeeInfo> EmployeeInfos { get; set; } = new List<EmployeeInfo>();

    }
}
