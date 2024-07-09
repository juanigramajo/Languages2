using tl2_tp10_2023_juanigramajo.Models;

public interface ITareaRepository
{
    public Tarea Create(Tarea tarea);
    public void Update(int id, Tarea tarea);
    public Tarea GetById(int id);
    public List<Tarea> List();
    public List<Tarea> ListByUser(int idUser);
    public List<Tarea> ListByTableroYUser(int idTab, int idUser);
    public List<Tarea> ListByTableroYNOTUser(int idTab, int idUser);

    public List<Tarea> CantxEstado(EstadoTarea idTab);
    public void Remove(int id);
    public void Asignar(int idUser, int idTarea);
}