using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLogic.Data_Transfer_Objects
{
    public class DepartmentDto
    {
        public int DeptId { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly DateOfCreation { get; set; }
    }
}
