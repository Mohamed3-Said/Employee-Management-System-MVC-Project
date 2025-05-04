using DemoBusinessLogic.Data_Transfer_Objects;
using DemoBusinessLogic.Data_Transfer_Objects.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployee(string? EmployeeSearchName);
        EmployeeDetailsDto GetEmployeebyId(int id);
        int CreateEmployee(CreatedEmployeeDto employeeDto);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
    }
}
