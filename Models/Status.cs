using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotebook.Models
{
    public class Status
    {
        private List<StatusConsulta> _statusConsultas;

        public Status()
        {
            AddStatusConsulta();
        }

        private void AddStatusConsulta ()
        {

            _statusConsultas = new List<StatusConsulta>();

            _statusConsultas.Add(new StatusConsulta { Id = 0, tipoStatus = "Agendado" });
            _statusConsultas.Add(new StatusConsulta { Id = 1, tipoStatus = "Confirmado Agendamento" });
            _statusConsultas.Add(new StatusConsulta { Id = 2, tipoStatus = "Consulta Realizada" });
            _statusConsultas.Add(new StatusConsulta { Id = 3, tipoStatus = "Cancelado" });


        }

        public IEnumerable<StatusConsulta> getStatusConsulta ()
        {
            return _statusConsultas;
        }

        public bool validaStatus(int Id)
        {
            var result = _statusConsultas.Where(s => s.Id == Id);
            if (!result.Any())
            {
                return false;
            }

            return true;
        }

    }
}
