using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotebook.Models
{
    
    public class EspecialidadeDR
    {
      
    

        public int ApplicationUserId { get; set; }

        public  ApplicationUser ApplicationUser { get; set; }

        
       
        public int EspecialidadeId { get; set; }

        public  Especialidade Especialidade { get; set; }


    }
}
