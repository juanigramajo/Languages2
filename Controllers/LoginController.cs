using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;
using tl2_tp10_2023_juanigramajo.ViewModels;

namespace tl2_tp10_2023_juanigramajo.Controllers
{
    public class LoginController : Controller
    {
        private IUsuarioRepository repositorioUsuario;
        private readonly ILogger<LoginController> _logger;


        public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            repositorioUsuario = usuarioRepository;
        }


        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel LoginVM)
        {
            List<Usuario> ListadoUsuarios = repositorioUsuario.List();
            Usuario usuarioLoggeado = ListadoUsuarios.FirstOrDefault(user => user.NombreDeUsuario == LoginVM.NombreDeUsuario && user.Password == LoginVM.Password);
            try
            {
                if (usuarioLoggeado == null)
                {
                    TempData["MensajeError"] = "Nombre de usuario o contraseña incorrectos.";
                    _logger.LogWarning($"Intento de acceso invalido - Usuario: [ {LoginVM.NombreDeUsuario} ] Clave ingresada: [ {LoginVM.Password} ]");
                    return RedirectToAction("Index");
                } else
                {
                    loggearUsuario(usuarioLoggeado);
                    _logger.LogInformation($"El usuario [ {LoginVM.NombreDeUsuario} ] ingresó corecctamente.");
                    return View("~/Views/Home/Index.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToAction("Error");
            }
        }

        private void loggearUsuario(Usuario usuario)
        {
            HttpContext.Session.SetString("Id", usuario.Id.ToString());
            HttpContext.Session.SetString("NombreDeUsuario", usuario.NombreDeUsuario);
            HttpContext.Session.SetString("Rol", usuario.RolDelUsuario.ToString());
        }

        public IActionResult Logout()
        {
            try
            {
                Usuario userLogout = new Usuario(HttpContext.Session.GetString("NombreDeUsuario"));

                if(logoutUsuario())
                {
                    _logger.LogInformation($"El usuario [ {userLogout.NombreDeUsuario} ] cerró sesió corecctamente.");
                }

                return View(userLogout);         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error");
            }
        }

        private bool logoutUsuario()
        {
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("NombreDeUsuario");
            HttpContext.Session.Remove("Rol");
            HttpContext.Session.Clear();

            return true;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}