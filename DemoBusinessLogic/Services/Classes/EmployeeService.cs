using AutoMapper;
using DemoBusinessLogic.Data_Transfer_Objects;
using DemoBusinessLogic.Data_Transfer_Objects.EmployeeDtos;
using DemoBusinessLogic.Services.AttachmentService;
using DemoBusinessLogic.Services.Interfaces;
using DemoDataAccess.Models.EmployeeModel;
using DemoDataAccess.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLogic.Services.Classes
{
    //Asck ClR inject in (Employee Ctor) smothing that implement "IEmployeeRepository"
    public class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper , IAttachmentService _attachmentService ) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployee(string? EmployeeSearchName)
        {
           // var Employees = _employeeRepository.GetAll(E=>E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            //Map:
            //src => Employee.
            //Dest => EmployeeDto.          
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
                employees = _unitOfWork.EmployeeRepository.GetAll();
            
            else
                employees = _unitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
                 
            var employeeDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(employees);
                return employeeDto;
        }

        public EmployeeDetailsDto? GetEmployeebyId(int id)
        {
            //AutoMapper:
            var employee = _unitOfWork.EmployeeRepository.GetBYId(id);
            return employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(employee);
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var Employee = _mapper.Map<CreatedEmployeeDto,Employee>(employeeDto);

            if(employeeDto.Image is not null)
            {
                Employee.ImageName = _attachmentService.Upload(employeeDto.Image , "Images");
            }
            _unitOfWork.EmployeeRepository.Add(Employee);
            return _unitOfWork.SaveChanges();
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto));
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetBYId(id);
            if(employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(employee);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

    }
}
