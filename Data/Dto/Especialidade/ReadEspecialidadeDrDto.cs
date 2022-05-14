using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotebook.Data.Dto.Especialidade
{
    public class ReadEspecialidadeDrDto
    {

        public int ApplicationUserId { get; set; }
      
        public string Nome { get; set; }
      
        public int EspecialidadeId { get; set; }

        public string Especialidades { get; set; }

    }
}
