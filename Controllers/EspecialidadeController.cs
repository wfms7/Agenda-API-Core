using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data.Dto.Especialidade;
using WebNotebook.Data.Dto.EspecialidadeMedica;
using WebNotebook.Services;

namespace WebNotebook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EspecialidadeController : ControllerBase
    {

        private EspecialidadeService _especilidadeService;

        public EspecialidadeController(EspecialidadeService especilidadeMedicaService)
        {
            _especilidadeService = especilidadeMedicaService;

        }

        [HttpPost]

        public IActionResult AdiocnarEspecialidade([FromBody] CreateEspecialidadeDto Dto)
        {
            ReadEspecialidadeDto readEspecilidade = _especilidadeService.AdiocnarEspecialidade(Dto);
            return CreatedAtAction(nameof(RecuperaEspecilidadePorId), new { Id = readEspecilidade.Id }, readEspecilidade);
        }


        [HttpGet]

        public IActionResult RecuperarEspecialidade([FromQuery] string nome = null, int skip = 0, int take = 10)
        {
            List<ReadEspecialidadeDto> read = _especilidadeService.RecuperarEspecialidade(nome, skip, take);
            if (read != null)
            {
                return Ok(read);
            }

            return NotFound();

        }





        [HttpGet("{id}")]
        public IActionResult RecuperaEspecilidadePorId(int id)
        {
            ReadEspecialidadeDto read = _especilidadeService.RecuperaEspecilidadePorId(id);
            if (read != null)
            {
                return Ok(read);

            }
            return NotFound();

        }


        [HttpPut("{id}")]

        public IActionResult AtualizarEspecialidade(int id, [FromBody] UpdateEspecialidade Dto)
        {

            Result result = _especilidadeService.AtualizarEspecialidade(id, Dto);
            if (result.IsFailed)
            {
                return NotFound();
            }
            return Ok();

        }

        [HttpDelete("{id}")]

        public IActionResult DeletarEspecilidade(int id)
        {
            Result result = _especilidadeService.DeletarEspecilidade(id);
            if (result.IsFailed)
            {
                return NotFound();
            }

            return Ok();

        }

        [HttpPost("add")]

        public IActionResult incluirEspecialidadeDR([FromQuery] int userId, int espId)
        {

            AddEspecialidadeDrDto dto = new AddEspecialidadeDrDto { ApplicationUserId = userId, EspecialidadeId = espId };
            Result result = _especilidadeService.incluirEspecialidadeDR(dto);

            return result.IsFailed ? BadRequest(result.ToString()): Ok();

        }

        [HttpDelete("deletar")]

        public IActionResult deletarEspecialidadeDR([FromQuery] int userId, int espId)
        {
            AddEspecialidadeDrDto dto = new AddEspecialidadeDrDto { ApplicationUserId = userId, EspecialidadeId = espId };
            Result result = _especilidadeService.deletarEspecialidadeDR(dto);

            return result.IsFailed ? BadRequest() : Ok();
        }

        [HttpGet("especialidadeDR")]

        public IActionResult getEspecialidadeDRIdEspecialidadeEDotor([FromQuery] int userId, int espId)
        {
            AddEspecialidadeDrDto dto = new AddEspecialidadeDrDto { ApplicationUserId = userId, EspecialidadeId = espId };
            ReadEspecialidadeDrDto read = _especilidadeService.getEspecialidadeDRIdEspecialidadeEDotor(dto);



            return read == null ? NotFound(): Ok(read);

        }

        [HttpGet("especialidadeDR/{id}")]

        public IActionResult getEspecilidadeDRPorIdDotor([FromRoute] int id)
        {
            List<ReadEspecialidadeDrDto> read = _especilidadeService.recuperarEspecilidadeDRPorIdDotor(id);

            return Ok(read);
        }




    }
}
