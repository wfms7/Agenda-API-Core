using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data;
using WebNotebook.Data.Dto.Consulta;
using WebNotebook.Models;

namespace WebNotebook.Services
{
    public class ConsultaService
    {
        private AppDbContext _appDbContext;
        private IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;
        private Status _status;

        public ConsultaService(AppDbContext appDbContext, IMapper mapper, UserManager<ApplicationUser> userManager , Status status)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userManager = userManager;
             _status = status;
    }

        public Result agendarConsulta(CreateConsultaDto dto)
        {
            Result result = validaAgendamento(dto);

            if (result.IsFailed)
            {
                return Result.Fail(result.Reasons[0].ToString());
            }

            Consulta consulta = _mapper.Map<Consulta>(dto);
            _appDbContext.Consultas.Add(consulta);
            _appDbContext.SaveChanges();

            return Result.Ok() ;
        }

        public List<ReadConsultaDto> recuperaAllConsulta(int idAgenda, int idDr, int skip, int take, DateTime dateInicio, DateTime dateFim)
        {


           DateTime[] data = validardata(dateInicio, dateFim);
                       
           List< Consulta >consulta = _appDbContext.Consultas.Include(d => d.ApplicationUsers).Include(p=> p.Pacientes ).Where(c => c.AgendaCalendarioId == idAgenda && c.ApplicationUserId == idDr &&  c.DataConsulta >= data[0] && c.DataConsulta <= data[1]  ).ToList();

           return _mapper.Map<List<ReadConsultaDto>>(consulta);

        }

        public ReadConsultaDto recuperarOneConsulta(int id)
        {
            Consulta consulta = _appDbContext.Consultas.Include(d => d.ApplicationUsers).Include(p => p.Pacientes).FirstOrDefault(c => c.Id == id);
            return _mapper.Map<ReadConsultaDto>(consulta);

        }

        public Result atualizaConsulta(int id, UpdateConsultaDto dto)
        {
            Result result = validaAgendamento(dto);

            if (result.IsFailed)
            {
                return Result.Fail(result.Reasons[0].ToString());
            }

            Consulta consulta = _appDbContext.Consultas.FirstOrDefault(c => c.Id == id);

            if (consulta == null)
            {
                return Result.Fail("Consulta não encontrada");
            }

            if (consulta.PacienteId == dto.PacienteId)
            {
                consulta.DataConsulta = dto.DataConsulta;
                consulta.StatusId = dto.StatusId;

                _appDbContext.SaveChanges();
                return Result.Ok();
            }

           

            return Result.Fail("Não Atualizado");



        }

        private Result validaAgendamento(UpdateConsultaDto dto)
        {
            if (!validaDataMenorDiaAtual(dto.DataConsulta.Date))
            {
                return Result.Fail("Data menor do que hoje");
            }
            if(!validarStatusconsulta(dto.StatusId))
            {
                return Result.Fail($"Valor do status {dto.StatusId} incorreto");
            }
            Paciente paciente = buscaPaciente(dto.PacienteId);
            AgendaCalendario agendaCalendario = buscaAgenda(dto.ApplicationUserId, dto.AgendaCalendarioId);
            if (paciente == null)
            {
                return Result.Fail("Paciente Não encontrado");
            }
            else if (agendaCalendario == null)
            {
                return Result.Fail("Agenda não encontrada");
            }
            else if (!validaSemanaAgenda(agendaCalendario, dto.DataConsulta))
            {
                return Result.Fail($"{dto.DataConsulta.DayOfWeek} não esta liberado na Agenda");
            }
            else if (!validarHorarioDoAgendamento(agendaCalendario, dto.DataConsulta))
            {
                return Result.Fail($"fora do horario de atendimento ({agendaCalendario.HoraIncio.TimeOfDay} as {agendaCalendario.HoraFim.TimeOfDay})");
            }
           

            return Result.Ok();


        }

        private Result validaAgendamento(CreateConsultaDto dto )
        {

             

            if (!validaDataMenorDiaAtual(dto.DataConsulta.Date))
            {
                return Result.Fail("Data menor do que hoje");
            }
            if (!validarStatusconsulta(dto.StatusId))
            {
                return Result.Fail($"Valor do status {dto.StatusId} incorreto");
            }


            Paciente paciente = buscaPaciente(dto.PacienteId);
            AgendaCalendario agendaCalendario = buscaAgenda(dto.ApplicationUserId, dto.AgendaCalendarioId);
           // Paciente paciente = _appDbContext.Paciente.Where(p=> p.Id == dto.PacienteId).FirstOrDefault();
           // ApplicationUser applicationUser = _userManager.Users.FirstOrDefault(u => u.Id == dto.ApplicationUserId);
           // AgendaCalendario agendaCalendario = _appDbContext.agendaCalendarios.Include(u => u.ApplicationUser).FirstOrDefault(a => a.Id == dto.AgendaCalendarioId && a.ApplicationUserId == dto.ApplicationUserId);

            if (paciente == null)
            {
                return Result.Fail("Paciente Não encontrado");
            }
            else if( agendaCalendario == null)
            {
                return Result.Fail("Agenda não encontrada");
            }
            else if(!validaSemanaAgenda(agendaCalendario, dto.DataConsulta))
            {
                return Result.Fail($"{dto.DataConsulta.DayOfWeek} não esta liberado na Agenda") ;
            }
            else if(!validarHorarioDoAgendamento(agendaCalendario, dto.DataConsulta)) 
            {
                return Result.Fail($"fora do horario de atendimento ({agendaCalendario.HoraIncio.TimeOfDay} as {agendaCalendario.HoraFim.TimeOfDay})");
            }
            else if(!validarQTSDiaConsulta(agendaCalendario, dto))
            {
                return Result.Fail($"Nomero de agentamento por dia é {agendaCalendario.QuantidadeDia}");
            }

            return Result.Ok();
        }




       private bool validaDataMenorDiaAtual(DateTime data)
        {

            if (data.Date < DateTime.Now.Date)
            {
                return false;
            }

            return true;

        }


        private Paciente  buscaPaciente(int pacienteId )
        {

            Paciente paciente = _appDbContext.Paciente.Where(p => p.Id == pacienteId).FirstOrDefault();
            // ApplicationUser applicationUser = _userManager.Users.FirstOrDefault(u => u.Id == dto.ApplicationUserId);
          //  AgendaCalendario agendaCalendario = _appDbContext.agendaCalendarios.Include(u => u.ApplicationUser).FirstOrDefault(a => a.Id == AgendaCalendarioId && a.ApplicationUserId == ApplicationUserId);

            return paciente;
        }

        private AgendaCalendario buscaAgenda(int ApplicationUserId, int AgendaCalendarioId)
        {

          //  Paciente paciente = _appDbContext.Paciente.Where(p => p.Id == pacienteId).FirstOrDefault();
            // ApplicationUser applicationUser = _userManager.Users.FirstOrDefault(u => u.Id == dto.ApplicationUserId);
              AgendaCalendario agendaCalendario = _appDbContext.agendaCalendarios.Include(u => u.ApplicationUser).FirstOrDefault(a => a.Id == AgendaCalendarioId && a.ApplicationUserId == ApplicationUserId);

            return agendaCalendario;
        }


        private bool validaSemanaAgenda(AgendaCalendario agendaCalendario, DateTime data)

        {
            switch (data.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return agendaCalendario.Monday;
                case DayOfWeek.Tuesday:
                    return agendaCalendario.Tuesday;
                case DayOfWeek.Wednesday:
                    return agendaCalendario.Wednesday;
                case DayOfWeek.Thursday:
                    return agendaCalendario.Thursday;
                case DayOfWeek.Friday:
                    return agendaCalendario.Friday;
                case DayOfWeek.Saturday:
                    return agendaCalendario.Sartuday;
                case DayOfWeek.Sunday:
                    return agendaCalendario.Sunday;

                default:
                    return false;

            }


        }

        private bool validarHorarioDoAgendamento (AgendaCalendario agendaCalendario, DateTime data)
        {
            if(agendaCalendario.HoraIncio.TimeOfDay <= data.TimeOfDay  && agendaCalendario.HoraFim.TimeOfDay >= data.TimeOfDay)
            {
                return true;
            }

            return false;
        }

        private bool validarQTSDiaConsulta (AgendaCalendario agendaCalendario, CreateConsultaDto dto)
        {
            int count = 0;
            
            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $"select COUNT(*) as total from consultas where AgendacalendarioId ={dto.AgendaCalendarioId} and ApplicationUserId ={dto.ApplicationUserId} and StatusId in(0,1,2)  and  DataConsulta BETWEEN CAST('{dto.DataConsulta.Date.ToString("yyyy-MM-dd")} 00:00' AS DATETIME) AND CAST('{dto.DataConsulta.Date.ToString("yyyy-MM-dd")} 23:00' AS DATETIME);";
                _appDbContext.Database.OpenConnection();
                using (var result  = command.ExecuteReader())
                {
                    // var x = result.Read();
                    while (result.Read())
                    {
                        count =   Convert.ToInt16( result["total"]);
                    }


                }
                _appDbContext.Database.CloseConnection();
            }
            
                 
           // var count = _appDbContext.Database.ExecuteSqlInterpolated($"select id from consultas where AgendacalendarioId ={dto.AgendaCalendarioId} and ApplicationUserId ={dto.ApplicationUserId}  and  DataConsulta BETWEEN CAST('{dto.DataConsulta.Date.ToString("yyyy-MM-dd")} 00:00' AS DATETIME) AND CAST('{dto.DataConsulta.Date.ToString("yyyy-MM-dd")} 23:00' AS DATETIME);");

           
            if (count <= agendaCalendario.QuantidadeDia)
            {
                return true;
            }

            return false;
           
        }


        private   DateTime[] validardata (DateTime dateInicio , DateTime dateFim)
        {


            
            DateTime[] data = new DateTime[2];
            if (dateInicio <= dateFim)
            {


                if (dateInicio == DateTime.MinValue)
                {
                    data[0] = DateTime.Now.Date;
                }
                else
                {
                    data[0] = dateInicio.Date;

                }
                if (dateFim == DateTime.MinValue)
                {
                    data[1] = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                }
                else
                {
                    data[1] = dateFim.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                }
            }
            else
            {
                throw new Exception($"Data inicio { dateInicio } maio que data Fim {dateFim}");
            }
            


            return data;

        }

        private bool validarStatusconsulta(int status)
        {

            Status statusConsulta = new Status();

            return statusConsulta.validaStatus(status); 

        }


    }
}
