using AutoMapper;
using POCNT.Application.DTOs;
using POCNT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCNT.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Entity to DTO mappings
            CreateMap<Admin, AdminDto>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
        }
    }
}
