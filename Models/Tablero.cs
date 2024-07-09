using tl2_tp10_2023_juanigramajo.ViewModels.Tableros;

namespace tl2_tp10_2023_juanigramajo.Models
{
    public class Tablero
    {
        public int Id { get; set; }
        public int IdUsuarioPropietario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }


        public Tablero() {
                
        }

        public Tablero(CrearTableroViewModel CrearTableroVM) {
            this.IdUsuarioPropietario = CrearTableroVM.IdUsuarioPropietario;
            this.Nombre = CrearTableroVM.Nombre;
            this.Descripcion = CrearTableroVM.Descripcion;
        }

        public Tablero(ModificarTableroViewModel modificarTableroVM) {
            this.Id = modificarTableroVM.Id;
            this.IdUsuarioPropietario = modificarTableroVM.IdUsuarioPropietario;
            this.Nombre = modificarTableroVM.Nombre;
            this.Descripcion = modificarTableroVM.Descripcion;
        }   
    }
}