using tl2_tp10_2023_juanigramajo.ViewModels.Tareas;

namespace tl2_tp10_2023_juanigramajo.Models
{
    public enum EstadoTarea
    {
        Ideas = 0,
        ToDo = 1, 
        Doing = 2, 
        Review = 3, 
        Done = 4
    }

    public class Tarea
    {
        public int Id { get; set; }
        public int IdTablero { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; } 
        public string Color { get; set; }
        public EstadoTarea Estado { get; set; }
        public int IdUsuarioAsignado { get; set; }


        public Tarea(){

        }

        public Tarea(CrearTareaViewModel CrearTareaVM){
            this.IdTablero = CrearTareaVM.IdTablero;
            this.Nombre = CrearTareaVM.Nombre;
            this.Descripcion = CrearTareaVM.Descripcion;
            this.Color = CrearTareaVM.Color;
            this.Estado = CrearTareaVM.Estado;
            this.IdUsuarioAsignado = CrearTareaVM.IdUsuarioAsignado;
        }

        public Tarea(ModificarTareaViewModel ModificarTareaVM){
            this.Id = ModificarTareaVM.Id;
            this.IdTablero = ModificarTareaVM.IdTablero;
            this.Nombre = ModificarTareaVM.Nombre;
            this.Descripcion = ModificarTareaVM.Descripcion;
            this.Color = ModificarTareaVM.Color;
            this.Estado = ModificarTareaVM.Estado;
            this.IdUsuarioAsignado = ModificarTareaVM.IdUsuarioAsignado;
        }  
    }
}