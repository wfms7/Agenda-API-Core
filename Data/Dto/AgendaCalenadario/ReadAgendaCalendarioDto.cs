using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Models;

namespace WebNotebook.Data.Dto.AgendaCalenadario
{
    public class ReadAgendaCalendarioDto
    {
        
        
        public int Id { get; set; }

        
       
        public string NomeAgenda { get; set; }
        public DateTime HoraIncio { get; set; }
        public DateTime HoraFim { get; set; }

        public bool Sunday { get; set; }
        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Sartuday { get; set; }

        public int QuantidadeDia { get; set; }

        public int ApplicationUserId { get; set; }

        public string ApplicationUserNome { get; set; }
        //  public ApplicationUser ApplicationUser_ { get; set; }
    }
}
