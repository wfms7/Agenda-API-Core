using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data.Dto.Telefone;
using WebNotebook.Services;

namespace WebNotebook.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TelefoneController : ControllerBase
    {
        private TelefoneService _telefoneService;

        public TelefoneController(TelefoneService telefoneService)
        {
            _telefoneService = telefoneService;
        }


        [HttpPost]

        public IActionResult CreateTelefone([FromBody] List<CreateTelefoneDto> dto)
        {
            try
            {
                List<ReadTelefoneDto> readDto = _telefoneService.CreateTelefone(dto);
                return Ok();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message.ToString());
            }

        }

        [HttpGet]

        public IActionResult RecuperaTelefone([FromQuery] string Telefone = null, [FromQuery] int id=-1)

        {
            List<ReadTelefoneDto> readDto = _telefoneService.RecuperaTelefone(Telefone,id);
            if (readDto != null)
            {
                return Ok(readDto);
            }
            return NotFound();
        }

        [HttpGet("tel/{id}")]

        public IActionResult RecuperaTelefoneID(int id)
        {
            ReadTelefoneDto readDto = _telefoneService.RecuperaTelefoneID(id);
            if(readDto != null)
                {

                return Ok(readDto);

            }

            return NotFound();

        }


        


        [HttpPut("{id}")]

        public Result AlterarTelefone(int id,UpdateTelefoneDto dto)
        {
            Result resultado = _telefoneService.AtulizaTelefone(id,dto);
            if (resultado.IsFailed)
            {
                return Result.Fail(resultado.IsFailed.ToString());
            }

            return Result.Ok();

        }

        [HttpDelete("{id}")]
        public Result DeletaTelefone(int id)
        {
            Result resultado = _telefoneService.DeletaTelefone(id);
            if (resultado.IsFailed)
            {
                return Result.Fail(resultado.IsFailed.ToString());
            }

            return Result.Ok();
        }

    }
}
