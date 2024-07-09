using tl2_tp10_2023_juanigramajo.Models;

public interface IUsuarioRepository
{
    public void Create(Usuario usuario);
    public void Update(int id, Usuario usuario);
    public List<Usuario> List();
    public Usuario GetById(int id);
    public void Remove(int id);
}