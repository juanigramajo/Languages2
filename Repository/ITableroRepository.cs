using tl2_tp10_2023_juanigramajo.Models;

public interface ITableroRepository
{
    public Tablero Create(int IdPropietario, Tablero tablero);
    public void Update(int id, Tablero tablero);
    public Tablero GetById(int id);
    public List<Tablero> List();
    public List<Tablero> ListByUser(int id);
    List<Tablero> ListByTareasEnOtroTablero(int idUser);
    public List<Tablero> RestoDeTablerosListByUser(int id);
    public void Remove(int id);
}