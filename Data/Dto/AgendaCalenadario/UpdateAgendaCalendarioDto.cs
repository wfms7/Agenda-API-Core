using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotebook.Data.Dto.AgendaCalenadario
{
    public class UpdateAgendaCalendarioDto
    {
        [Required]
        public int ApplicationUserId { get; set; }

        [Required]
        public string NomeAgenda { get; set; }

        public DateTime HoraIncio { get; set; }
        public DateTime HoraFim { get; set; }

        public bool Sunday { get; set; }
        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Sartuday { get; set; }

        public int QuantidadeDia { get; set; }
    }
}
