using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_juanigramajo.Models;


namespace tl2_tp10_2023_juanigramajo.ViewModels.Tareas
{
    public class ListarTareasViewModel
    {
        public List<Tarea> ListadoTareas { get; set; }
        public List<Tarea> ListadoOtrasTareas { get; set; }
        public string IdUsuarioAsignado { get; set; }


        public ListarTareasViewModel(){
            
        }

        public ListarTareasViewModel(List<Tarea> tareas){
            this.ListadoTareas = tareas;
        }

        public ListarTareasViewModel(List<Tarea> misTareas, List<Tarea> otrasTareas, string idUser){
            this.ListadoTareas = misTareas;
            this.ListadoOtrasTareas = otrasTareas;
            this.IdUsuarioAsignado = idUser;
        }
    }
}