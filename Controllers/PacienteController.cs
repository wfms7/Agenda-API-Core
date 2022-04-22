using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data.Dto;
using WebNotebook.Data.Dto.Paciente;
using WebNotebook.Services;

namespace WebNotebook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PacienteController : ControllerBase
    {

        private PacienteService _pacienteService;

        public PacienteController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpPost]

        public IActionResult AdicionaPaciente([FromBody] CreatePacienteDto Dto)
        {
            ReadPacienteDto readPaciente = _pacienteService.AdionaPaciente(Dto);

            return CreatedAtAction(nameof(ReucperaPacientePorId), new { Id = readPaciente.Id }, readPaciente);

        }


        [HttpGet]

        public IActionResult RecuperarPaceinte([FromQuery] string Nome = null, int skip = 0 ,int take =10)
        {
           ( List<ReadPacienteDto> readDto , int countPaciente) = _pacienteService.RecuperarPaciente(Nome,skip,take);
            if (readDto != null)
            {
                return Ok( new { data = readDto, countPaciente });
            }
            return NotFound();

        }

        [HttpGet("{id}")]

        public IActionResult ReucperaPacientePorId(int id)
        {

            ReadPacienteDto readPaciente = _pacienteService.ReucperaPacientePorId(id);
            if (readPaciente != null) { return Ok(readPaciente); }
            return NotFound();


        }

        [HttpPut("{id}")]

        public IActionResult AtulizaPaciente (int id, [FromBody] UpdatePacienteDto pacienteDto)
        {

            Result resultado = _pacienteService.AtualizaPaciente(id, pacienteDto);
            if (resultado.IsFailed) 
            {
                return NotFound();
            }
            return Ok();

        }

        [HttpDelete("{id}")]

        public IActionResult DeletaPaciente(int id)
        {
            Result resultado = _pacienteService.DeletaPaciente(id);
            if (resultado.IsFailed)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
