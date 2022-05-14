using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotebook.Data.Dto.Roles
{
    public class ReadRolesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

         public string NormalizedName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
