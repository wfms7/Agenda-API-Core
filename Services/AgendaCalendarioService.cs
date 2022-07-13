using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data;
using WebNotebook.Data.Dto.AgendaCalenadario;
using WebNotebook.Models;

namespace WebNotebook.Services
{
    public class AgendaCalendarioService
    {


        private AppDbContext _appDbContext;
        private IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole<int>> _roleManager;

        public AgendaCalendarioService(AppDbContext appDbContext , IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userManager = userManager;
        }


        public async Task<Result> createAgendaCalendarioAsync(CreateAgendaCalendarioDto dto)
        {
            try
            {
                            
                ApplicationUser user = _userManager
                    .Users
                    .FirstOrDefault(r => r.Id == dto.ApplicationUserId);

                var role = await _userManager.GetRolesAsync(user);

            if( role.Contains("Doutor(a)"))
            {
                AgendaCalendario agenda = _mapper.Map<AgendaCalendario>(dto);
                _appDbContext.agendaCalendarios.Add(agenda);
                var result = _appDbContext.SaveChanges().ToResult();
            }
            else 
             {
               return Result.Fail("Usuario não é Doutor(a)");
             }

                return Result.Ok();

            }
            catch (Exception ex)
            {
               return Result.Fail(ex.Message);
            }

        }

        public Result atualizaAgendaCalendario(int id, UpdateAgendaCalendarioDto dto)
        {
            int roleId = _appDbContext.UserRoles.FirstOrDefault(x => x.UserId == dto.ApplicationUserId).RoleId;
            string role = _appDbContext.Roles.FirstOrDefault(r => r.Id == roleId).Name;

            if( role == ("Doutor(a)"))
            { 

            AgendaCalendario agendaCalendario = _appDbContext.agendaCalendarios.FirstOrDefault(a => a.Id == id);

            if (agendaCalendario == null)
            {
                return Result.Fail("Agenda Não encontrada");
            }

            _mapper.Map(dto, agendaCalendario);
            _appDbContext.SaveChanges();
            return Result.Ok();
            }

            return Result.Fail("Usuario não é Doutor(a)");
        }

        public Result deletarAgendacalendario(int id)
        {
            AgendaCalendario agendaCalendario = _appDbContext.agendaCalendarios.FirstOrDefault(a => a.Id == id);

            if (agendaCalendario == null)
            {
                return Result.Fail("Agenda não Encontrada");
            }

            _appDbContext.Remove(agendaCalendario);
            _appDbContext.SaveChanges();

            return Result.Ok();
        }

        public ReadAgendaCalendarioDto recuperaAgendaCalendarioPorId(int id)
        {

         ReadAgendaCalendarioDto  agendaCalendarioDto = _appDbContext.agendaCalendarios.Include(a => a.ApplicationUser).Select(a => new ReadAgendaCalendarioDto()
            {
                Id = a.Id,
                NomeAgenda = a.NomeAgenda,
                Monday = a.Monday,
                Tuesday = a.Tuesday,
                Wednesday = a.Wednesday,
                Thursday = a.Thursday,
                Friday = a.Friday,
                Sartuday = a.Sartuday,
                Sunday = a.Sunday,
                HoraIncio = a.HoraIncio,
                HoraFim = a.HoraFim,
                QuantidadeDia = a.QuantidadeDia,
                ApplicationUserId = a.ApplicationUserId,
                ApplicationUserNome = a.ApplicationUser.Nome
            }).FirstOrDefault( a=> a.Id == id);

            return agendaCalendarioDto;

        }

        public  List<ReadAgendaCalendarioDto> recuperaAgendaCalendario(string nomeAg, int skip, int take, int dr)
        {
            
            List<ReadAgendaCalendarioDto> agendaCalendarioDto = new List<ReadAgendaCalendarioDto>();
            if (nomeAg != null &&  dr >0)
            {
                agendaCalendarioDto = _appDbContext.agendaCalendarios.Take(take).Skip(skip).Include(a=> a.ApplicationUser).Select(a => new ReadAgendaCalendarioDto()
                {
                    Id = a.Id,
                    NomeAgenda = a.NomeAgenda,
                    Monday = a.Monday,
                    Tuesday = a.Tuesday,
                    Wednesday = a.Wednesday,
                    Thursday = a.Thursday,
                    Friday = a.Friday,
                    Sartuday = a.Sartuday,
                    Sunday = a.Sunday,
                    HoraIncio = a.HoraIncio,
                    HoraFim = a.HoraFim,
                    QuantidadeDia = a.QuantidadeDia,
                    ApplicationUserId = a.ApplicationUserId,
                    ApplicationUserNome = a.ApplicationUser.Nome
                }).Where(a => a.NomeAgenda.Contains(nomeAg) && a.ApplicationUserId == dr).ToList();
                  
            }
            else if (nomeAg!= null)
            {
                agendaCalendarioDto = _appDbContext.agendaCalendarios.Take(take).Skip(skip).Include(a => a.ApplicationUser).Select(a => new ReadAgendaCalendarioDto()
                {
                    Id = a.Id,
                    NomeAgenda = a.NomeAgenda,
                    Monday = a.Monday,
                    Tuesday = a.Tuesday,
                    Wednesday = a.Wednesday,
                    Thursday = a.Thursday,
                    Friday = a.Friday,
                    Sartuday = a.Sartuday,
                    Sunday = a.Sunday,
                    HoraIncio = a.HoraIncio,
                    HoraFim = a.HoraFim,
                    QuantidadeDia = a.QuantidadeDia,
                    ApplicationUserId = a.ApplicationUserId,
                    ApplicationUserNome = a.ApplicationUser.Nome
                }).Where(a => a.NomeAgenda.Contains(nomeAg) ).ToList();
            }
            else if( dr >0)
            {
                agendaCalendarioDto = _appDbContext.agendaCalendarios.Take(take).Skip(skip).Include(a => a.ApplicationUser).Select(a => new ReadAgendaCalendarioDto()
                {
                    Id = a.Id,
                    NomeAgenda = a.NomeAgenda,
                    Monday = a.Monday,
                    Tuesday = a.Tuesday,
                    Wednesday = a.Wednesday,
                    Thursday = a.Thursday,
                    Friday = a.Friday,
                    Sartuday = a.Sartuday,
                    Sunday = a.Sunday,
                    HoraIncio = a.HoraIncio,
                    HoraFim = a.HoraFim,
                    QuantidadeDia = a.QuantidadeDia,
                    ApplicationUserId = a.ApplicationUserId,
                    ApplicationUserNome = a.ApplicationUser.Nome
                }).Where(a=> a.ApplicationUserId == dr).ToList();
            }
            else
            {



                agendaCalendarioDto = _appDbContext.agendaCalendarios.Take(take).Skip(skip).Include(a => a.ApplicationUser).Select(a => new ReadAgendaCalendarioDto()
                { Id= a.Id,
                   NomeAgenda = a.NomeAgenda,
                     Monday = a.Monday,
                    Tuesday = a.Tuesday,
                    Wednesday = a.Wednesday,
                     Thursday = a.Thursday,
                     Friday = a.Friday,
                     Sartuday = a.Sartuday,
                     Sunday =a.Sunday,
                     HoraIncio = a.HoraIncio,
                     HoraFim = a.HoraFim,
                     QuantidadeDia = a.QuantidadeDia,
                    ApplicationUserId = a.ApplicationUserId,
                    ApplicationUserNome = a.ApplicationUser.Nome
                }).ToList();
            }

            return agendaCalendarioDto;

        }
    }
}
