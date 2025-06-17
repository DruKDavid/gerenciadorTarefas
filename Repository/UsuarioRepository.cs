using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using gerenciadorTarefas.Data;
using gerenciadorTarefas.Models;

namespace gerenciadorTarefas.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BancoContext _context;

        public UsuarioRepository(BancoContext context)
        {
            _context = context;
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public UsuarioModel ObterPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public List<UsuarioModel> ListarUsuarios(string searchTerm = null)
        {
            try
            {
                var query = _context.Usuarios.AsQueryable();
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(u =>
                        u.Nome.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        u.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        u.Id.ToString().Contains(searchTerm));
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro em ListarUsuarios: {ex.Message}");
                throw;
            }
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public bool Remover(int id)
        {
            var usuario = ObterPorId(id);
            if (usuario == null) return false;
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return true;
        }
    }
}
