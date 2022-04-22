using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebNotebook.Models
{
    public class Paciente
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(250, ErrorMessage ="O Nome Paceinte não pode exceder 250 caracteres")]
        public string Nome { get; set; }

        public string CPF { get; set; }

        public string RG { get; set; }

        public DateTime DataNascimento { get; set; }

        [StringLength(250, ErrorMessage = "O Nome da mãe não pode exceder 250 caracteres")]
        public string NomeMae { get; set; }
        [StringLength(250, ErrorMessage = "O Nome do pai não pode exceder 250 caracteres")]
        public string NomePai { get; set; }

        public string Endereco { get; set; }

        public string Cep { get; set; }

        public string Bairro { get; set; }

        public string Complemento { get; set; }

        [JsonIgnore]
        public virtual List<Telefone> Telefones { get; set; }

        public string Email { get; set; }


    }
}
