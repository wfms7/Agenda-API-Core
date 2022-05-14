using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotebook.Data.Dto.User
{
    public class CreateUsuarioDto
    {

        [Required]
        public string Username { get; set; }

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }

        public string RG { get; set; }

        public string CRM { get; set; }

        public string NomeMae { get; set; }

        public string NomePai { get; set; }

        public string Role { get; set; }

       
    }
}
