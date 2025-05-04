using DemoDataAccess.Contexts;
using DemoDataAccess.Repositories.interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        
        public UnitOfWork(IEmployeeRepository employeeRepository ,
            IDepartmentRepository departmentRepository , ApplicationDbContext dbContext)
        {
            _employeeRepository = new Lazy<IEmployeeRepository>(()=>new EmployeeRepository(dbContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(()=>new DepartmentRepository(dbContext));
            _dbContext = dbContext;
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public int SaveChanges() => _dbContext.SaveChanges();
    }
}
