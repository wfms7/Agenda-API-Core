using AutoMapper;
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
