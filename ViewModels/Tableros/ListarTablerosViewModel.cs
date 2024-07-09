using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_juanigramajo.Models;


namespace tl2_tp10_2023_juanigramajo.ViewModels.Tableros
{
    public class ListarTablerosViewModel
    {
        public List<Tablero> ListadoMisTableros { get; set; }
        public List<Tablero> ListadoTareasEnOtroTablero { get; set; }
        public List<Tablero> ListadoOtrosTableros { get; set; }
        public string IdUsuarioPropietario { get; set; }


        public ListarTablerosViewModel(){
            
        }

        public ListarTablerosViewModel(List<Tablero> misTableros, List<Tablero> TareasEnOtrosTableros, List<Tablero> otrosTableros, string idUser){
            this.ListadoMisTableros = misTableros;
            this.ListadoTareasEnOtroTablero = TareasEnOtrosTableros;
            this.ListadoOtrosTableros = otrosTableros;
            this.IdUsuarioPropietario = idUser;
        }
    }
}