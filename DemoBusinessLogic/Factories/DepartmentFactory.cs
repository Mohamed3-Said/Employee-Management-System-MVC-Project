using DemoBusinessLogic.Data_Transfer_Objects;
using DemoDataAccess.Models.DepartmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLogic.Factories
{
    static class DepartmentFactory
    {
        //Extention Methods

        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            return new DepartmentDto
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation=DateOnly.FromDateTime(department.CreatedOn)
            };

        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
                CreatedBy=department.CreatedBy,
                LastModifiedBy=department.LastModifiedBy,
                LastModifiedOn= DateOnly.FromDateTime(department.LastModifiedOn),
                IsDeleted=department.IsDeleted,
            };

        }

        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {

                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())

            };

        }

        public static Department ToEntity(this UpdatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };

        }
    
    
    }
}
