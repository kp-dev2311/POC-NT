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
            CreateMap<Users, UsersDto>().ReverseMap();
            CreateMap<UserActivities, UserActivitesDto>().ReverseMap();
        }
    }
}
