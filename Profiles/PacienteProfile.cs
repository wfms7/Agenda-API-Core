using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data.Dto;
using WebNotebook.Data.Dto.Paciente;
using WebNotebook.Models;

namespace WebNotebook.Profiles
{
    public class PacienteProfile:Profile
    {
        public PacienteProfile()
        {
            CreateMap<CreatePacienteDto, Paciente>();
            CreateMap<Paciente, ReadPacienteDto>();
            CreateMap<UpdatePacienteDto, Paciente>();
        }
    }
}
