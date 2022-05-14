using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data;
using WebNotebook.Data.Dto.Especialidade;
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

        public Result AtualizarEspecialidade(int id, UpdateEspecialidade dto)
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

        public Result DeletarEspecilidade(int id)
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

        public Result incluirEspecialidadeDR(AddEspecialidadeDrDto dto)
        {

            EspecialidadeDR especialidadeDR = _mapper.Map<EspecialidadeDR>(dto);

            try
            {
                _appContext.EspecialidadeDRs.Add(especialidadeDR);
                _appContext.SaveChanges();

                return Result.Ok();
            }
            catch (Exception ex)
            {

                return Result.Fail(ex.InnerException.Message.ToString());
            }


         
            
        }

        public Result deletarEspecialidadeDR(AddEspecialidadeDrDto dto)
        {

            EspecialidadeDR especialidadeDR = _appContext.EspecialidadeDRs.Include(e=> e.Especialidade).Include(e=>e.ApplicationUser).FirstOrDefault(e=> e.ApplicationUserId ==dto.ApplicationUserId && e.EspecialidadeId == dto.EspecialidadeId );

            if (especialidadeDR == null)
            {
                return Result.Fail("Especialidade nãe encontrada");
            }

            _appContext.EspecialidadeDRs.Remove(especialidadeDR);
            _appContext.SaveChanges();

            return Result.Ok();
        }

        public ReadEspecialidadeDrDto recuperarEspecialidadeDR(AddEspecialidadeDrDto dto)
        {
            EspecialidadeDR especialidadeDR = _appContext.EspecialidadeDRs.Include(e => e.Especialidade).Include(e => e.ApplicationUser).FirstOrDefault(e => e.ApplicationUserId == dto.ApplicationUserId && e.EspecialidadeId == dto.EspecialidadeId);

            ReadEspecialidadeDrDto read = new ReadEspecialidadeDrDto();

            if (read != null)
            {
                read.ApplicationUserId = especialidadeDR.ApplicationUserId;
                read.Nome = especialidadeDR.ApplicationUser.Nome;
                read.EspecialidadeId = especialidadeDR.EspecialidadeId;
                read.Especialidades = especialidadeDR.Especialidade.Especialidades;
                
            }

            return read;
        }

        public List<ReadEspecialidadeDrDto> recuperarEspecilidadeDRall(int id)
        {
            List<EspecialidadeDR> especialidadeDR = _appContext.EspecialidadeDRs.Include(e=>e.ApplicationUser).Include(e=>e.Especialidade).Where(e=> e.ApplicationUserId == id).ToList();
            List<ReadEspecialidadeDrDto> read = new List<ReadEspecialidadeDrDto>();

            if (especialidadeDR == null)
            {
                return read;
            }

            foreach (var dr in especialidadeDR)
            {
                read.Add(new ReadEspecialidadeDrDto { 
                ApplicationUserId =dr.ApplicationUserId,
                EspecialidadeId = dr.EspecialidadeId,
                Especialidades= dr.Especialidade.Especialidades,
                Nome = dr.ApplicationUser.Nome
                
                }); 
            }

            return read;

        }

    
    }
}
