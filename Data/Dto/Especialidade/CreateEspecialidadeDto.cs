using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotebook.Data.Dto.EspecialidadeMedica
{
    public class CreateEspecialidadeDto
    {

        [Required]
        public string Especialidades { get; set; }

    }
}
