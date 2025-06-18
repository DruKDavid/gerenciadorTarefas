using System.Collections.Generic;
using gerenciadorTarefas.Models;

namespace gerenciadorTarefas.ViewModels
{
    public class GestaoTarefaViewModel
    {
        public List<TarefaModel> Tarefas { get; set; }
        public List<UsuarioModel> Usuarios { get; set; }
    }
}
