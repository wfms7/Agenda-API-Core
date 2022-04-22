using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Models;

namespace WebNotebook.Data.Dto
{
    public class CreatePacienteDto
    {

        [Required]
        [StringLength(250, ErrorMessage = "O Nome Paceinte não pode exceder 250 caracteres")]
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


        public string Email { get; set; }


    }
}
