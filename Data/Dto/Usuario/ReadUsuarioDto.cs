using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotebook.Data.Dto.Usuario
{
    public class ReadUsuarioDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }

        public string RG { get; set; }

        public string CRM { get; set; }

        public string NomeMae { get; set; }

        public string NomePai { get; set; }

        public WebNotebook.Models.Roles Roles { get; set; }
    }
}
