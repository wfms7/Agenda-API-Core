using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebNotebook.Models
{
    public class Telefone
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Tipo { get; set; }
       
        public string Telefones { get; set; }

        public int PacienteId { get; set; }
        [JsonIgnore]
        public virtual Paciente Paciente { get; set; }

       
        
        

    }
}
