using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data;
using WebNotebook.Data.Dto.Telefone;
using WebNotebook.Models;

namespace WebNotebook.Services
{
    public class TelefoneService
    {

        private AppDbContext _appDbContext;
        private IMapper _mapper;

        public TelefoneService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }


        public List<ReadTelefoneDto> CreateTelefone(List<CreateTelefoneDto> dto)
        {

            List<Telefone> telefone = _mapper.Map<List<Telefone>>(dto);
            _appDbContext.Telefones.AddRange(telefone);
            _appDbContext.SaveChanges();

            return _mapper.Map<List<ReadTelefoneDto>>(telefone);
        }

        public List<ReadTelefoneDto> RecuperaTelefone(string telefoneDto,int idDto)
        {
            List<Telefone> telefone;

            if (telefoneDto == null && idDto <=0)
            {
                telefone =  _appDbContext.Telefones.Include(t => t.Paciente).ToList();

                return _mapper.Map<List<ReadTelefoneDto>>(telefone);
              
            }
            else if (idDto>0)
            {
                telefone = _appDbContext.Telefones.Include(t => t.Paciente).Where(t => t.PacienteId == (idDto)).ToList();

                return _mapper.Map<List<ReadTelefoneDto>>(telefone);
            }
            //telefone = _appDbContext.Telefones.Where(t => t.Telefones.Contains(telefoneDto)).ToList();
            telefone = _appDbContext.Telefones.Include(t => t.Paciente).Where(t=> t.Telefones.Contains(telefoneDto)).ToList();
            //var _telefone = Newtonsoft.Json.JsonConvert.SerializeObject(telefone);
            return _mapper.Map<List<ReadTelefoneDto>>(telefone);

        }

        public Result AtulizaTelefone(int id, UpdateTelefoneDto telefoneDto)
        {
            Telefone telefone = _appDbContext.Telefones.FirstOrDefault(t => t.Id == id);
            if (telefone == null)
            {
                return Result.Fail("Telefone não encontrado");
            }
            else if (telefone.PacienteId == telefoneDto.PacienteId)
            {
                _mapper.Map(telefoneDto, telefone);
                _appDbContext.SaveChanges();
                return Result.Ok();
            }

            return Result.Fail("Não Foi Atualizado");
        }

        public Result DeletaTelefone(int id)
        {
            Telefone telefone = _appDbContext.Telefones.FirstOrDefault(t => t.Id == id);
            if (telefone == null)
            {
                return Result.Fail("Telefone não encontrado");
            }
            _appDbContext.Remove(telefone);
            _appDbContext.SaveChanges();
            return Result.Ok();

        }
    }
}
