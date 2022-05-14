

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebNotebook.Data.Dto.Roles;
using WebNotebook.Models;

namespace WebNotebook.Profiles
{
    public class RolesProfile:Profile
    {
        public RolesProfile()
        {
            CreateMap<CreateRolesDto, Roles>();
            CreateMap<Roles,IdentityRole<int>>();
            CreateMap<Roles, ReadRolesDto>();
            CreateMap<IdentityRole<int>, Roles>();
            
        }
    }
}
