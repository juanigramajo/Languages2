using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_juanigramajo.Models;

namespace tl2_tp10_2023_juanigramajo.ViewModels.Usuarios
{
    public class ModificarUsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(30, ErrorMessage = "El nombre de usuario no puede contener mas de 30 caracteres")]
        [Display(Name = "Nombre de Usuario")]
        public string NombreDeUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(30, ErrorMessage = "La contraseña no puede contener mas de 30 caracteres")]
        [PasswordPropertyText]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Rol")]
        public Rol RolDelUsuario { get; set; }



        public ModificarUsuarioViewModel(){            

        }
        
        public ModificarUsuarioViewModel(Usuario usuario){
            this.Id = usuario.Id;
            this.NombreDeUsuario = usuario.NombreDeUsuario;
            this.Password = usuario.Password;
            this.RolDelUsuario = usuario.RolDelUsuario;
        }
    }
}