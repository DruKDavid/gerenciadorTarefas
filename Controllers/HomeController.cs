using Microsoft.AspNetCore.Mvc;
using gerenciadorTarefas.Models;
using gerenciadorTarefas.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using gerenciadorTarefas.Enums;
using gerenciadorTarefas.ViewModels;

namespace gerenciadorTarefas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BancoContext _context;

        public HomeController(ILogger<HomeController> logger, BancoContext context)
        {
            _logger = logger;
            _context = context;
        }

        // ==================== CRUD DE USUÁRIOS ====================

        public IActionResult Usuarios()
        {
            var listaUsuarios = _context.Usuarios.ToList();
            return View(listaUsuarios);
        }

        // GET para criar usuário - opcional, caso tenha view separada para o form
        public IActionResult CriarUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarUsuario(UsuarioModel usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario); // retorna a mesma view para mostrar erros

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return RedirectToAction("Usuarios");
        }

        // GET para editar usuário
        public IActionResult EditarUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        [HttpPost]
        public IActionResult EditarUsuario(UsuarioModel usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);

            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
            return RedirectToAction("Usuarios");
        }

        public IActionResult ExcluirUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null) return NotFound();

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return RedirectToAction("Usuarios");
        }

        // ==================== CRUD DE TAREFAS ====================

        public IActionResult GestaoTarefa()
        {
            var tarefas = _context.Tarefas.Include(t => t.Usuario).ToList();
            return View(tarefas);
        }

        public IActionResult Tarefas()
        {
            var vm = new TarefaUsuarioViewModel
            {
                Tarefas = _context.Tarefas.Include(t => t.Usuario).ToList(),
                Usuarios = _context.Usuarios.ToList()
            };

            return View(vm);
        }

        // GET para criar tarefa
        public IActionResult CriarTarefa()
        {
            var vm = new TarefaUsuarioViewModel
            {
                Usuarios = _context.Usuarios.ToList(),
                Tarefa = new TarefaModel()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult CriarTarefa(TarefaUsuarioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Usuarios = _context.Usuarios.ToList();
                return View(vm);
            }

            _context.Tarefas.Add(vm.Tarefa);
            _context.SaveChanges();
            return RedirectToAction("GestaoTarefa");
        }

        // GET para editar tarefa
        public IActionResult EditarTarefa(int id)
        {
            var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null) return NotFound();

            var vm = new TarefaUsuarioViewModel
            {
                Tarefa = tarefa,
                Usuarios = _context.Usuarios.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult EditarTarefa(TarefaUsuarioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Usuarios = _context.Usuarios.ToList();
                return View(vm);
            }

            _context.Tarefas.Update(vm.Tarefa);
            _context.SaveChanges();
            return RedirectToAction("GestaoTarefa");
        }

        public IActionResult ExcluirTarefa(int id)
        {
            var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null) return NotFound();

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();
            return RedirectToAction("GestaoTarefa");
        }

        [HttpPost]
        public IActionResult AtualizarEstado(int id, string state)
        {
            var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa != null)
            {
                if (Enum.TryParse<EstadoTarefa>(state, out var estadoEnum))
                {
                    tarefa.State = estadoEnum;
                    _context.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("State", "Estado da tarefa inválido.");
                }
            }
            return RedirectToAction("GestaoTarefa");
        }

        public IActionResult Index() => View();

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
