using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebNotebook.Models
{
    public class AgendaCalendario
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ApplicationUserId { get; set; }

        [JsonIgnore]
        public virtual  ApplicationUser ApplicationUser { get; set; }

        public string NomeAgenda { get; set; }

        public DateTime HoraIncio { get; set; }
        public DateTime HoraFim { get; set; }

        public bool Sunday { get; set; }
        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday   { get; set; }

        public bool Sartuday { get; set; }

        public int QuantidadeDia { get; set; }

        [JsonIgnore]
        public virtual List<Consulta> Consultas { get; set; }

    }
}
