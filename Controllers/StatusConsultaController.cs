using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Models;

namespace WebNotebook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusConsultaController: ControllerBase

    {
        private Status _status;
        public StatusConsultaController(Status status)
        {
            _status = status;
        }

        [HttpGet]

        public IEnumerable getStatus ()
        {
                       
          return _status.getStatusConsulta();
        }
    }
}
