using tl2_tp10_2023_juanigramajo.ViewModels.Usuarios;

namespace tl2_tp10_2023_juanigramajo.Models
{
    public enum Rol
    {
        Administrador = 0,
        Operador = 1
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Password { get; set; }
        public Rol RolDelUsuario { get; set; }


        public Usuario() {
        
        }

        public Usuario(string nombre) {
            this.NombreDeUsuario = nombre;            
        }

        public Usuario(CrearUsuarioViewModel crearUsuarioVM){
            this.NombreDeUsuario = crearUsuarioVM.NombreDeUsuario;
            this.Password = crearUsuarioVM.Password;
            this.RolDelUsuario = crearUsuarioVM.RolDelUsuario;
        }

        public Usuario(ModificarUsuarioViewModel modificarUsuarioVM){
            this.Id = modificarUsuarioVM.Id;
            this.NombreDeUsuario = modificarUsuarioVM.NombreDeUsuario;
            this.Password = modificarUsuarioVM.Password;
            this.RolDelUsuario = modificarUsuarioVM.RolDelUsuario;
        }
    }
}