using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data;
using WebNotebook.Data.Dto.EspecialidadeMedica;
using WebNotebook.Models;

namespace WebNotebook.Services
{
    public class EspecialidadeService
    {
        private AppDbContext _appContext;
        private IMapper _mapper;

        public EspecialidadeService(AppDbContext appContext, IMapper mapper)
        {
            _appContext = appContext;
            _mapper = mapper;
        }

        public ReadEspecialidadeDto AdiocnarEspecialidade ( CreateEspecialidadeDto Dto)
        {
            Especialidade skill = _mapper.Map<Especialidade>(Dto);
            _appContext.Especialidade.Add(skill);
            _appContext.SaveChanges();

            return _mapper.Map<ReadEspecialidadeDto>(skill);
        }

        public ReadEspecialidadeDto RecuperaEspecilidadePorId(int id)
        {
            Especialidade skil = _appContext.Especialidade.FirstOrDefault(s => s.Id == id);

            return _mapper.Map<ReadEspecialidadeDto>(skil);
        }

        public List<ReadEspecialidadeDto> RecuperarEspecialidade(string nome, int skip,int take)
        {
            List<Especialidade> skil;

            if (nome==null)
            {
                skil = _appContext.Especialidade.Skip(skip).Take(take).ToList();

                return _mapper.Map<List<ReadEspecialidadeDto>>(skil);
            }

            skil = _appContext.Especialidade.Where(s => s.Especialidades.Contains(nome)).Skip(skip).Take(take).ToList();
            return _mapper.Map<List<ReadEspecialidadeDto>>(skil);


        }

        internal Result AtualizarEspecialidade(int id, UpdateEspecialidade dto)
        {

            Especialidade especialidade = _appContext.Especialidade.FirstOrDefault(s => s.Id == id);

            if (especialidade==null)
            {
                return Result.Fail("Especialidade Não encontrado");
            }
            _mapper.Map(dto, especialidade);
            _appContext.SaveChanges();
            return Result.Ok();


        }

        internal Result DeletarEspecilidade(int id)
        {
            Especialidade especialidade = _appContext.Especialidade.FirstOrDefault(s=> s.Id == id);

            if (especialidade == null)
            {
                return Result.Fail("Especialidade Não encontrado");
            }

            _appContext.Remove(especialidade);
            _appContext.SaveChanges();

            return Result.Ok();
        }
    }
}
