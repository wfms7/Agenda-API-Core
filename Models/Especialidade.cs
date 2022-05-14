using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebNotebook.Models
{
    public class Especialidade
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Especialidades { get; set; }

        [JsonIgnore]
        public virtual List<EspecialidadeDR> EspecialidadeDRs { get; set; }
    }
}
