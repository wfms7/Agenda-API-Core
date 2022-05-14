

using AutoMapper;
using WebNotebook.Data.Dto.Especialidade;
using WebNotebook.Data.Dto.EspecialidadeMedica;
using WebNotebook.Models;

namespace WebNotebook.Profiles
{
    public class EspecialidadeProfile:Profile
    {

        public EspecialidadeProfile()
        {
            CreateMap<CreateEspecialidadeDto, Especialidade>();
            CreateMap<Especialidade, ReadEspecialidadeDto>();
            CreateMap<UpdateEspecialidade, Especialidade>();
            CreateMap<AddEspecialidadeDrDto, EspecialidadeDR>().ReverseMap();
            CreateMap<EspecialidadeDR,ReadEspecialidadeDrDto >().ReverseMap();


        }


    }
}
