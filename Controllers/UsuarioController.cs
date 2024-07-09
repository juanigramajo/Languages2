using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;
using tl2_tp10_2023_juanigramajo.ViewModels.Usuarios;

namespace tl2_tp10_2023_juanigramajo.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioRepository _repositorioUsuario;
        private readonly ITareaRepository _repositorioTarea;
        private readonly ITableroRepository _repositorioTablero;




        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository, ITareaRepository tareaRepository, ITableroRepository tableroRepository)
        {
            _logger = logger;
            _repositorioUsuario = usuarioRepository;
            _repositorioTarea = tareaRepository;
            _repositorioTablero = tableroRepository;
        }


        public IActionResult Index()
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }

                List<Usuario> ListadoUsuarios = _repositorioUsuario.List();
                ListarUsuariosViewModel listarUsuariosVM = new ListarUsuariosViewModel(ListadoUsuarios);

                if (ListadoUsuarios != null)
                {
                    return View(listarUsuariosVM);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToAction("Error");
            }
        }


        [HttpGet]
        public IActionResult CrearUsuario()
        {   
            try
            {
                return View(new CrearUsuarioViewModel());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToAction("Error");
            }
        }

    
        [HttpPost]
        public IActionResult CrearUsuario(CrearUsuarioViewModel crearUsuarioVM)
        {   
            try
            {
                if(!ModelState.IsValid) return RedirectToAction("CrearUsuario");

                Usuario usuario = new Usuario(crearUsuarioVM);
                _repositorioUsuario.Create(usuario);
                
                return RedirectToAction("Index");                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToAction("Error");
            }
        }
    

        [HttpGet]
        public IActionResult EditarUsuario(int idUsuario)
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if(!isAdmin())
                {   
                    TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                    return RedirectToAction("Index");
                }

                ModificarUsuarioViewModel modificarUsuarioVM = new ModificarUsuarioViewModel(_repositorioUsuario.GetById(idUsuario));

                return View(modificarUsuarioVM);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToAction("Error");
            }
        }


        [HttpPost]
        public IActionResult EditarUsuario(ModificarUsuarioViewModel modificarUsuarioVM)
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if(!isAdmin())
                {   
                    TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                    return RedirectToAction("Index");
                }
                if(!ModelState.IsValid) return RedirectToAction("EditarUsuario");


                Usuario usuario2 = new Usuario(modificarUsuarioVM);
                _repositorioUsuario.Update(usuario2.Id, usuario2);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToAction("Error");
            }
        }

        
        public IActionResult DeleteUsuario(int idUsuario)
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if(!isAdmin())
                {   
                    TempData["ErrorMessage"] = "No tienes permisos para eliminar un usuario";
                    return RedirectToAction("Index");
                }

                List<Tarea> ListadoTareas = _repositorioTarea.ListByUser(idUsuario);
                foreach (var tarea in ListadoTareas)
                {
                    tarea.IdUsuarioAsignado = -1;
                    _repositorioTarea.Update(tarea.Id, tarea);
                }

                List<Tablero> ListadoTableros = _repositorioTablero.ListByUser(idUsuario);

                foreach (var tablero in ListadoTableros)
                {
                    tablero.IdUsuarioPropietario = -1;
                    _repositorioTablero.Update(tablero.Id, tablero);
                }
                
                _repositorioUsuario.Remove(idUsuario);

                return RedirectToAction("Index");                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToAction("Error");
            }
        }


        private bool isLogged()
        {
            if (HttpContext.Session.GetString("Id") != null) 
                return true;
                
            return false;
        }


        private bool isAdmin()
        {
            if (HttpContext.Session.GetString("Rol") == "Administrador") 
            
                return true;
                
            return false;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}