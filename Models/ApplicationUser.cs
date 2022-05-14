using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotebook.Models
{
    public class ApplicationUser : IdentityUser<int>
    {

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }

        public string RG { get; set; }

        public string CRM { get; set; }

        public string NomeMae { get; set; }

        public string NomePai { get; set; }
    }
}
