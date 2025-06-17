using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace gerenciadorTarefas.Models
{
    public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor informe seu nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe seu email")]
        public string Email { get; set; }

    }
}