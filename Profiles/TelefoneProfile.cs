using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data.Dto.Telefone;
using WebNotebook.Models;

namespace WebNotebook.Profiles
{
    public class TelefoneProfile: Profile
    {
        public TelefoneProfile()
        {
            CreateMap<CreateTelefoneDto, Telefone>();
            CreateMap<Telefone, ReadTelefoneDto>();
            CreateMap<UpdateTelefoneDto, Telefone>();
        }

    }
}
