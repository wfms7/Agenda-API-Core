using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data.Dto.AgendaCalenadario;
using WebNotebook.Services;

namespace WebNotebook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgendaCalendarioController : ControllerBase
    {

        private AgendaCalendarioService _agendaCalendarioService;
        public AgendaCalendarioController(AgendaCalendarioService agendaCalendarioService)
        {
            _agendaCalendarioService = agendaCalendarioService;
        }

        [HttpPost]

        public IActionResult createAgendaCalendario(CreateAgendaCalendarioDto dto)
        {
            Task<Result> result = _agendaCalendarioService.createAgendaCalendarioAsync(dto);

            return result.Result.IsFailed ? BadRequest(result.Result.Reasons) : Ok();
        }

        [HttpGet]

        public IActionResult recuperaAgendaCalendario([FromQuery] string nomeAg = null, int skip = 0, int take = 10, int dr = 0)
        {

            List<ReadAgendaCalendarioDto> dto = _agendaCalendarioService.recuperaAgendaCalendario(nomeAg, skip, take, dr);


            return dto != null ? Ok(dto) : NotFound();

        }

        [HttpGet ("{id}")]

        public IActionResult recuperaAgendaCalendarioPorId ([FromRoute] int id)
        {
            ReadAgendaCalendarioDto dto = _agendaCalendarioService.recuperaAgendaCalendarioPorId(id);

            return dto != null ? Ok(dto) : NotFound(); 
        }

        [HttpPut ("{id}")]

        public IActionResult atualizaAgendaCalendario ([FromRoute] int id , [FromBody] UpdateAgendaCalendarioDto dto )
        {

            Result result = _agendaCalendarioService.atualizaAgendaCalendario(id, dto);

            return result.IsFailed ? NotFound(result.Reasons) : Ok();
        }

        [HttpDelete ("{id}")]

        public IActionResult deletarAgendacalendario([FromRoute] int id)
        {
            Result result = _agendaCalendarioService.deletarAgendacalendario(id);

            return result.IsFailed ? NotFound(result.Reasons) : Ok();
              

        }


    }
}
