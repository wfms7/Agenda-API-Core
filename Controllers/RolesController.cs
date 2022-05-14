using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data.Dto.Roles;
using WebNotebook.Services;

namespace WebNotebook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private RolesService _rolesService;

        public RolesController(RolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpPost]
        public IActionResult AdicionarRoles([FromBody] CreateRolesDto dto)
        {
            Task<Result> read = _rolesService.AdicionarRoles(dto);

            //if (read.Result.IsFailed)
            //{
            //    return BadRequest(read.Result.Reasons);
            //}


                
            return read.Result.IsFailed ? BadRequest(read.Result.Reasons):  Ok();
        }

        [HttpGet("{id}")]

        public IActionResult recuperarRolesPorId( int id)
        {

            ReadRolesDto read = _rolesService.recuperarRolesPorId(id);



            return read != null ? Ok(read) : NotFound(); 

        }

        [HttpDelete("{id}")]

        public IActionResult deletarRoles(int id)
        {
            Result resultado = _rolesService.deletarRoles(id);

            return resultado.IsFailed ? NotFound(resultado) : Ok();
        }
    
        [HttpGet]

        public IActionResult recuperarRoles ()
        {
           List<ReadRolesDto> read = _rolesService.recuperarRoles();
            return Ok(read);
        }

    }
}
