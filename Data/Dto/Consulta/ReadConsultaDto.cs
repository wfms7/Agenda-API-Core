using System;

using System.ComponentModel.DataAnnotations;

using WebNotebook.Models;
namespace WebNotebook.Data.Dto.Consulta
{
    public class ReadConsultaDto
    {
        [Key]
        [Required]

        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }

        public virtual WebNotebook.Models.Paciente Pacientes { get; set; }
        [Required]
        public DateTime DataConsulta { get; set; }

        [Required]
        public int StatusId { get; set; }
        [Required]
        public int ApplicationUserId { get; set; }


        public string NomeUser { get; set; }

      //  public virtual ApplicationUser ApplicationUsers { get; set; }

        [Required]

        public int AgendaCalendarioId { get; set; }

        public virtual AgendaCalendario AgendaCalendarios { get; set; }
    }
}
