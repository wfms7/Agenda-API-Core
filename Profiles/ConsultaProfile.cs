using AutoMapper;
using System;

using WebNotebook.Data.Dto.Consulta;
using WebNotebook.Models;

namespace WebNotebook.Profiles
{
    public class ConsultaProfile:Profile
    {
        public ConsultaProfile()
        {
            CreateMap<Consulta, CreateConsultaDto>().ReverseMap();
            CreateMap<Consulta, ReadConsultaDto>().ForMember( de=> de.NomeUser , map => map.MapFrom(a=> a.ApplicationUsers.Nome)).ReverseMap();
            CreateMap<Consulta, UpdateConsultaDto>().ReverseMap();
        }
    }
}
