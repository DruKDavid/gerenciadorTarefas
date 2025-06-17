using System.Collections.Generic;
using gerenciadorTarefas.Models;

namespace gerenciadorTarefas.ViewModels
{
    public class TarefaUsuarioViewModel
    {
        public List<TarefaModel> Tarefas { get; set; } = new List<TarefaModel>(); // lista de tarefas
        public List<UsuarioModel> Usuarios { get; set; } = new List<UsuarioModel>(); // lista de usuários
        public TarefaModel Tarefa { get; set; } = new TarefaModel(); // tarefa para criar/editar
    }
}
