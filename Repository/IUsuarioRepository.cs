using System.Collections.Generic;
using gerenciadorTarefas.Models;

namespace gerenciadorTarefas.Repository
{
    public interface IUsuarioRepository
    {
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel ObterPorId(int id);
        List<UsuarioModel> ListarUsuarios(string searchTerm = null);
        UsuarioModel Atualizar(UsuarioModel usuario);
        bool Remover(int id);
    }
}
