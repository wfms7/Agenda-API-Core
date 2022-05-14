using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotebook.Data.Dto.Especialidade
{
    public class AddEspecialidadeDrDto
    {
        [Required]
        public int ApplicationUserId { get; set; }

        [Required]
        public int EspecialidadeId { get; set; }

    }
}
