using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using P01.Work.Models;

namespace P01.Work.App.Core.Dtos
{
    public class WorkDbProfile:Profile
    {
        public WorkDbProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, ManagerDto>()
                .ForMember(dest => dest.EmployeeDto,from => from.MapFrom(x => x.ManagerEmployee))
                .ReverseMap();
            CreateMap<Employee, EmployeePersonalInfoDto>().ReverseMap();
        }
    }
}
