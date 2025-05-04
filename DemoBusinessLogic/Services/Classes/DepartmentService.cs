using DemoBusinessLogic.Data_Transfer_Objects;
using DemoBusinessLogic.Factories;
using DemoBusinessLogic.Services.Interfaces;
using DemoDataAccess.Models.DepartmentModel;
using DemoDataAccess.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLogic.Services.Classes
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        //Get All Department
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return departments.Select(Dep => Dep.ToDepartmentDto());
        }

        //Get DepartmentById
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var departments = _unitOfWork.DepartmentRepository.GetBYId(id);
            return departments is null ? null : departments.ToDepartmentDetailsDto();
        }

        //Add Department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChanges();
        }


        //Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        //Delete Department
        public bool DeleteDepartment(int id)
        {
            var Department = _unitOfWork.DepartmentRepository.GetBYId(id);
            if (Department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Remove(Department);
                int Result =_unitOfWork.SaveChanges();
                return Result > 0 ? true : false;
            }

        }



    }
}
