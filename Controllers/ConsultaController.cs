using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebNotebook.Data.Dto.Consulta;
using WebNotebook.Services;

namespace WebNotebook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultaController : ControllerBase
    {
        private ConsultaService _consultaService;

        public ConsultaController(ConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpPost]

        public IActionResult agendarConsulta([FromBody] CreateConsultaDto dto)
        {

            Result result = _consultaService.agendarConsulta(dto);

            return result.IsFailed ? BadRequest(result.Reasons) : Ok();

        }

        [HttpGet]

        public IActionResult recuperaAllConsulta([FromQuery] DateTime dateInicio, DateTime dateFim, int idAgenda = 0, int idDr = 0)
        {

            try
            {
                List<ReadConsultaDto> readConsultaDto = _consultaService.recuperaAllConsulta(idAgenda, idDr, dateInicio, dateFim);


                return readConsultaDto == null ? NotFound() : Ok(readConsultaDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet("{id}")]

        public IActionResult recuperarOneConsulta([FromRoute] int id=0)
        {
            ReadConsultaDto readConsultaDto = _consultaService.recuperarOneConsulta(id);

            return readConsultaDto == null ? NotFound() : Ok(readConsultaDto);
        }

        [HttpPut("{id}")]

        public IActionResult atualizaConsulta([FromRoute] int id , [FromBody] UpdateConsultaDto dto)
        {

            Result result = _consultaService.atualizaConsulta(id, dto);

            return result.IsFailed ? BadRequest(result.Reasons) : Ok();

        }


     
    }
}
