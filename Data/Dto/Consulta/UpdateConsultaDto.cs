using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotebook.Data.Dto.Consulta
{
    public class UpdateConsultaDto
    {

        [Required]
        public int PacienteId { get; set; }

        [Required]
        public DateTime DataConsulta { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int ApplicationUserId { get; set; }

        [Required]
        public int AgendaCalendarioId { get; set; }
    }
}
