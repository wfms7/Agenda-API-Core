using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data.Dto.User;
using WebNotebook.Data.Dto.Usuario;
using WebNotebook.Services;

namespace WebNotebook.Controllers
{
        [ApiController]
        [Route("[controller]")]
    public class UsuarioController: ControllerBase
    {
        private UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]

        public IActionResult criaUsuario (CreateUsuarioDto dto)
        {
           Task < Result>  resultado  = _usuarioService.criaUsuarioAsync(dto);

            return resultado.Result.IsFailed ? BadRequest(resultado.Result.ToString()) : Ok();

        }

        [HttpGet]
        public IActionResult recuperarUsuario([FromQuery] int skip = 0 , int take = 10)
        {
            List<ReadUsuarioDto> read = _usuarioService.recuperarUsuario(skip, take);

            return Ok(read); 

        }

        [HttpGet("{id}")]

        public IActionResult recuperaUsuarioPorId ([FromRoute] int id)
        {

            ReadUsuarioDto read = _usuarioService.recuperaUsuarioPorId(id);

            return read == null ? NotFound() : Ok(read);

        }

        [HttpPut("{id}")]

        public IActionResult atualizarUsuario ([FromRoute] int id , UpdateUsuarioDto dto)
        {
            Task<Result> result = _usuarioService.atualizarUsuarioAsync(id, dto);

            return result.Result.IsFailed ? BadRequest(result) : Ok();

        }

        [HttpDelete("{id}")]

        public IActionResult deletaUsuarui([FromRoute]int id)
        {
            Task<Result> result = _usuarioService.deletaUsuarui(id);

            return result.Result.IsFailed ? BadRequest(result) : Ok();
        }


    }
}
