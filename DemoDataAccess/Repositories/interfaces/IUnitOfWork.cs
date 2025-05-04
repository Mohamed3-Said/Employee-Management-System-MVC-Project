using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataAccess.Repositories.interfaces
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get;  }
        public IDepartmentRepository DepartmentRepository { get; }
        int SaveChanges();  
    }
}
