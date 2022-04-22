using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotebook.Data.Dto.Telefone
{
    public class CreateTelefoneDto
    {
        [Required]
        public string Tipo { get; set; }
        [Required]
        public string Telefones { get; set; }

        [Required]
        public int PacienteId { get; set; }
    }
}
