using AutoMapper;
using DemoBusinessLogic.Data_Transfer_Objects.EmployeeDtos;
using DemoDataAccess.Models.DepartmentModel;
using DemoDataAccess.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLogic.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<src,Dest>();
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Department, options => options.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.Image, Options => Options.MapFrom(src => src.ImageName));

            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, options => options.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(dest => dest.Department, options => options.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.Image, Options => Options.MapFrom(src => src.ImageName));

            CreateMap<CreatedEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src=>src.HiringDate.ToDateTime(TimeOnly.MinValue)));

            CreateMap<UpdatedEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

        }
    }
}
