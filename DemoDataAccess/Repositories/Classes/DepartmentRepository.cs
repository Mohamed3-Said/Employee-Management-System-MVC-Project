using DemoDataAccess.Contexts;
using DemoDataAccess.Models.DepartmentModel;
using DemoDataAccess.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataAccess.Repositories.Classes
{
    //Primary Constructor .Net8 
    public class DepartmentRepository(ApplicationDbContext dbContext) :GenericRepository<Department>(dbContext) , IDepartmentRepository
    {
     
    }
}
