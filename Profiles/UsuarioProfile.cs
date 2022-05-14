using AutoMapper;
using WebNotebook.Data.Dto.User;
using WebNotebook.Data.Dto.Usuario;
using WebNotebook.Models;

namespace WebNotebook.Profiles
{
    public class UsuarioProfile:Profile
    {
        public UsuarioProfile()
        {

            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Usuario, ApplicationUser>().ReverseMap();
            CreateMap<Usuario, ReadUsuarioDto>();
            CreateMap<ReadUsuarioDto, Usuario>();
            CreateMap<UpdateUsuarioDto, Usuario>().ReverseMap();
        }
    }
}
