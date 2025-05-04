using DemoDataAccess.Contexts;
using DemoDataAccess.Models.DepartmentModel;
using DemoDataAccess.Models.Shared;
using DemoDataAccess.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataAccess.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContext dbContext) : GenericRepository<Employee>(dbContext),IEmployeeRepository
    {
       
    }
}
