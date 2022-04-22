using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebNotebook.Data.Dto.Paciente;
using WebNotebook.Models;

namespace WebNotebook.Data.Dto.Telefone
{
    public class ReadTelefoneDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Tipo { get; set; }

        public string Telefones { get; set; }

       
        public int PacienteId { get; set; }

       
        public WebNotebook.Models.Paciente Paciente    { get; set; }



    }
}
