using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;
using tl2_tp10_2023_juanigramajo.ViewModels.Tareas;
using tl2_tp10_2023_juanigramajo.ViewModels.Tableros;

namespace tl2_tp10_2023_juanigramajo.Controllers
{
    public class TareaController : Controller
    {
        private ITareaRepository _repositorioTarea;
        private ITableroRepository _repositorioTablero;
        private IUsuarioRepository _repositorioUsuario;

        private readonly ILogger<TareaController> _logger;
        


        public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _repositorioTarea = tareaRepository;
            _repositorioTablero = tableroRepository;
            _repositorioUsuario = usuarioRepository;
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
                if (!isAdmin())
                {
                    var idUser = HttpContext.Session.GetString("Id");
                    List<Tarea> ListadoTareas = _repositorioTarea.ListByUser(Convert.ToInt32(idUser));
                    ListarTareasViewModel ListarTareasVM = new ListarTareasViewModel(ListadoTareas);

                    if (ListadoTareas != null)
                    {
                        return View(ListarTareasVM);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else 
                {
                    List<Tarea> ListadoTareas = _repositorioTarea.List();
                    ListarTareasViewModel ListarTareasVM = new ListarTareasViewModel(ListadoTareas);

                    if (ListadoTareas != null)
                    {
                        return View(ListarTareasVM);
                    }
                    else
                    {
                        return BadRequest();
                    }
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

/* 
        public IActionResult ListXEstado()
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!isAdmin())
                {
                    var idUser = HttpContext.Session.GetString("Id");
                    List<Tarea> ListadoTareas = _repositorioTarea.ListByUser(Convert.ToInt32(idUser));
                    ListarTareasViewModel ListarTareasVM = new ListarTareasViewModel(ListadoTareas);

                    if (ListadoTareas != null)
                    {
                        return View(ListarTareasVM);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else 
                {
                    List<Tarea> ListadoTareas = _repositorioTarea.List();
                    ListarTareasViewModel ListarTareasVM = new ListarTareasViewModel(ListadoTareas);

                    if (ListadoTareas != null)
                    {
                        return View(ListarTareasVM);
                    }
                    else
                    {
                        return BadRequest();
                    }
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
 */

        [HttpGet]
        public IActionResult CrearTarea()
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }

                return View(new CrearTareaViewModel(_repositorioTablero.List(), _repositorioUsuario.List()));        
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
        public IActionResult CrearTarea(CrearTareaViewModel crearTareaVM)
        {   
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if(!ModelState.IsValid) return RedirectToAction("CrearTarea");

                Tarea tarea = new Tarea(crearTareaVM);
                _repositorioTarea.Create(tarea);

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
        public IActionResult EditarTarea(int idTarea)
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if(!isAdmin() && !(_repositorioTarea.GetById(idTarea).IdUsuarioAsignado == Convert.ToInt32(HttpContext.Session.GetString("Id"))))
                {
                    var idUser = HttpContext.Session.GetString("Id");
                    List<Tarea> listadoPermitido = _repositorioTarea.ListByUser(Convert.ToInt32(idUser));
                    Tarea tarea = listadoPermitido.FirstOrDefault(tarea => tarea.Id == idTarea);
                    
                    if (tarea == null)
                    {
                        TempData["ErrorMessage"] = "No tienes permisos para editar esta tarea";
                        return RedirectToAction("Index");
                    }
                }
                      

                ModificarTareaViewModel modificarTareaVM = new ModificarTareaViewModel(_repositorioTarea.GetById(idTarea), _repositorioTablero.List(), _repositorioUsuario.List());
                
                return View(modificarTareaVM);                
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
        public IActionResult EditarTarea(ModificarTareaViewModel modificarTareaVM)
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if(!isAdmin() && !(modificarTareaVM.IdUsuarioAsignado == Convert.ToInt32(HttpContext.Session.GetString("Id"))))
                {
                    var idUser = HttpContext.Session.GetString("Id");
                    List<Tarea> listadoPermitido = _repositorioTarea.ListByUser(Convert.ToInt32(idUser));
                    Tarea tarea = listadoPermitido.FirstOrDefault(tarea => tarea.Id == modificarTareaVM.Id);
                    
                    if (tarea == null)
                    {
                        TempData["ErrorMessage"] = "No tienes permisos para editar esta tarea";
                        return RedirectToAction("Index");
                    }
                }
                if(!ModelState.IsValid) return RedirectToAction("EditarTarea");

                Tarea tarea2 = new Tarea(modificarTareaVM);
                _repositorioTarea.Update(tarea2.Id, tarea2);

                return RedirectToAction("ListByTablero", "Tablero", new { idTablero = modificarTareaVM.IdTablero });
                // return RedirectToAction("Index");                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToAction("Error");
            }
        }

        
        public IActionResult DeleteTarea(int idTarea)
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if(!isAdmin() && !(_repositorioTarea.GetById(idTarea).IdUsuarioAsignado == Convert.ToInt32(HttpContext.Session.GetString("Id"))))
                {
                    var idUser = HttpContext.Session.GetString("Id");
                    List<Tarea> listadoPermitido = _repositorioTarea.ListByUser(Convert.ToInt32(idUser));
                    Tarea tarea = listadoPermitido.FirstOrDefault(tarea => tarea.Id == idTarea);
                    
                    if (tarea == null)
                    {
                        TempData["ErrorMessage"] = "No tienes permisos para editar esta tarea";
                        return RedirectToAction("Index");
                    }
                }
                
                _repositorioTarea.Remove(idTarea);

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