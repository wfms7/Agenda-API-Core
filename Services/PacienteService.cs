using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data;
using WebNotebook.Data.Dto;
using WebNotebook.Data.Dto.Paciente;
using WebNotebook.Models;

namespace WebNotebook.Services
{
    public class PacienteService
    {

        private AppDbContext _context;
        private IMapper _mapper;

        public PacienteService(AppDbContext appContext , IMapper mapper)
        {
            _context = appContext;
            _mapper = mapper;

        }


        public ReadPacienteDto AdionaPaciente(CreatePacienteDto dto)
        {
            Paciente paciente = _mapper.Map<Paciente>(dto);
            _context.Paciente.Add(paciente);
            _context.SaveChanges();

            return _mapper.Map<ReadPacienteDto>(paciente);
        }

        public ReadPacienteDto ReucperaPacientePorId(int id)
        {

           Paciente paciente =  _context.Paciente.Include(p=> p.Telefones).FirstOrDefault(p => p.Id == id);

            return _mapper.Map<ReadPacienteDto>(paciente);
        }

        public (List<ReadPacienteDto>, int ) RecuperarPaciente( string nome , int skip , int take)
        {
            int countPaciente;
            List<Paciente> pacientes;
            if (nome== null)
            {
                countPaciente = _context.Paciente.Include(p => p.Telefones).Count();
             pacientes = _context.Paciente.Include(p=>p.Telefones).Skip(skip).Take(take).ToList();
             List<ReadPacienteDto> readDto = _mapper.Map<List<ReadPacienteDto>>(pacientes);
             return (readDto, countPaciente);
            }
            else
            {
                countPaciente = _context.Paciente.Include(p => p.Telefones).Where(p => p.Nome.Contains(nome)).Count();
                pacientes = _context.Paciente.Include(p=> p.Telefones).Where(p => p.Nome.Contains(nome)).Skip(skip).Take(take).ToList();
                List<ReadPacienteDto> readDto = _mapper.Map<List<ReadPacienteDto>>(pacientes);
                return (readDto, countPaciente);

            }

            
        }

        public Result AtualizaPaciente(int id, UpdatePacienteDto pacienteDto)
        {

            Paciente paciente = _context.Paciente.FirstOrDefault(p => p.Id == id);
            if(paciente == null)
            {

                return Result.Fail("Paciente não encontrado");
            }
            _mapper.Map(pacienteDto,paciente );
            _context.SaveChanges();
            return Result.Ok();


        }

        public Result DeletaPaciente(int id)
        {
            Paciente paciente = _context.Paciente.FirstOrDefault(p => p.Id == id);
            if (paciente == null)
            {

                return Result.Fail("Paciente não encontrado");
            }
            _context.Remove(paciente);
            _context.SaveChanges();
            return Result.Ok();


        }
    }
}
