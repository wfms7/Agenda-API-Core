using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data.Dto.AgendaCalenadario;
using WebNotebook.Models;

namespace WebNotebook.Profiles
{
    public class AgendaCalendarioProfile:Profile
    {
        public AgendaCalendarioProfile()
        {
            CreateMap<AgendaCalendario, CreateAgendaCalendarioDto>().ReverseMap();
            CreateMap<ReadAgendaCalendarioDto, AgendaCalendario>().ReverseMap();
            CreateMap<UpdateAgendaCalendarioDto, AgendaCalendario>().ReverseMap();
        }

    }
}
