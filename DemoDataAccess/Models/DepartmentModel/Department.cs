using DemoDataAccess.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataAccess.Models.DepartmentModel
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();



    }
}
