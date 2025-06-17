using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using gerenciadorTarefas.Enums;

namespace gerenciadorTarefas.Models
{
    public class TarefaModel
    {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Descrição da tarefa é obrigatória")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Setor é obrigatório")]
    public string Setor { get; set; }

    [Required(ErrorMessage = "Informe o nível de prioridade")]
    public string Prioridade { get; set; }

    [Required(ErrorMessage = "Informe o estado da tarefa")]
    public EstadoTarefa State { get; set; }

    public DateOnly Data { get; set; }

    [Required(ErrorMessage = "Usuário responsável é obrigatório")]
    public int UsuarioId { get; set; }

    [ForeignKey("UsuarioId")]
    [ValidateNever]
    public UsuarioModel Usuario { get; set; }
    }

}