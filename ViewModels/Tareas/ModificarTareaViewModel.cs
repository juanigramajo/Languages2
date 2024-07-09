using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_juanigramajo.Models;


namespace tl2_tp10_2023_juanigramajo.ViewModels.Tareas
{
    public class ModificarTareaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id del tablero")]
        public int IdTablero { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre de la tarea no puede contener mas de 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")] 
        [StringLength(200, ErrorMessage = "La descripción de la tarea no puede contener mas de 200 caracteres")]
        public string Descripcion { get; set; }

        [Display(Name = "Color")]
        [StringLength(15, ErrorMessage = "El color de la tarea no puede contener mas de 15 caracteres")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")] 
        public EstadoTarea Estado { get; set; }        

        [Display(Name = "Asignar un usuario")]
        public int IdUsuarioAsignado { get; set; }

        public List<Tablero> ListadoTableros { get; set; }
        public List<Usuario> ListadoUsuarios { get; set; }


        public ModificarTareaViewModel(){

        }

        public ModificarTareaViewModel(Tarea tarea, List<Tablero> listadoTableros, List<Usuario> listadoUsuarios){
            this.Id = tarea.Id;
            this.IdTablero = tarea.IdTablero;
            this.Nombre = tarea.Nombre;
            this.Descripcion = tarea.Descripcion;
            this.Color = tarea.Color;
            this.Estado = tarea.Estado;
            this.IdUsuarioAsignado = tarea.IdUsuarioAsignado;
            this.ListadoTableros = listadoTableros;
            this.ListadoUsuarios = listadoUsuarios;
        }
    }
}