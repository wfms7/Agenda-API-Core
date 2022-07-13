using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebNotebook.Models
{
    public class Consulta
    {
        [Key]
        [Required]

        public int Id { get; set; }
        
        [Required]
        public int PacienteId { get; set; }

        [JsonIgnore]
        public virtual Paciente Pacientes { get; set; }
        [Required]
        public DateTime DataConsulta { get; set; }

        [Required]
        
        public int StatusId { get; set; }
        
        [Required]
        public int ApplicationUserId { get; set; }

        public virtual ApplicationUser  ApplicationUsers{ get; set; }

        [Required]

        public int AgendaCalendarioId  { get; set; }

        public virtual AgendaCalendario  AgendaCalendarios { get; set; }

    }
}
