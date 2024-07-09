using Microsoft.Data.Sqlite;
using tl2_tp10_2023_juanigramajo.Models;

public class TableroRepository : ITableroRepository
{
    private readonly string _cadenaConexion;

    public TableroRepository(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }

    
    // Crear un nuevo tablero. (devuelve un objeto Tablero).
    public Tablero Create(int IdPropietario, Tablero tab)
    {
        var query = $"INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@idUserProp, @nombre, @desc)";
        using (SqliteConnection connection = new (_cadenaConexion))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);

            command.Parameters.Add(new SqliteParameter("@idUserProp", IdPropietario));
            command.Parameters.Add(new SqliteParameter("@nombre", tab.Nombre));
            command.Parameters.Add(new SqliteParameter("@desc", tab.Descripcion));

            var commandENonQ = command.ExecuteNonQuery();
            if (commandENonQ == 0) throw new Exception("Se produjo un error al crear el tablero");

            connection.Close();
        }

        return tab;
    }


    // Modificar un tablero existente. (recibe un id y un objeto Tablero).
    public void Update(int id, Tablero tablero)
    {
        var query = $"UPDATE Tablero SET nombre = @nombre, descripcion = @desc WHERE id = @idcambiar;";
        using (SqliteConnection connection = new (_cadenaConexion))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);

            // command.Parameters.Add(new SQLiteParameter("@idUserProp", tablero.IdUsuarioPropietario));
            command.Parameters.Add(new SqliteParameter("@nombre", tablero.Nombre));
            command.Parameters.Add(new SqliteParameter("@desc", tablero.Descripcion));
            command.Parameters.Add(new SqliteParameter("@idcambiar", id));

            var commandENonQ = command.ExecuteNonQuery();
            if (commandENonQ == 0) throw new Exception("Se produjo un error al actualizar el tablero");

            connection.Close();
        }
    }


    // Obtener detalles de un tablero por su ID. (recibe un id y devuelve un Tablero).
    public Tablero GetById(int id)
    {
        SqliteConnection connection = new (_cadenaConexion);

        var tab = new Tablero();

        SqliteCommand command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Tablero WHERE id = @idTablero";
        command.Parameters.Add(new SqliteParameter("@idTablero", id));
        

        connection.Open();
        using(SqliteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                tab.Id = Convert.ToInt32(reader["id"]);
                tab.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                tab.Nombre = reader["nombre"].ToString();
                tab.Descripcion = reader["descripcion"].ToString();
            }
        }
        connection.Close();

        if (tab == null) throw new Exception($"No se encontraron tableros en la base de datos");

        return (tab);
    }


    // Listar todos los tableros existentes. (devuelve un list de tableros).
    public List<Tablero> List()
    {
        var queryString = @"SELECT * FROM Tablero;";

        List<Tablero> ListaTableros = new List<Tablero>();


        using (SqliteConnection connection = new (_cadenaConexion))
        {
            SqliteCommand command = new SqliteCommand(queryString, connection);
            connection.Open();
        
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tab = new Tablero();
                    tab.Id = Convert.ToInt32(reader["id"]);
                    tab.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tab.Nombre = reader["nombre"].ToString();
                    tab.Descripcion = reader["descripcion"].ToString();
                    ListaTableros.Add(tab);
                }
            }
            connection.Close();
        }

        if (ListaTableros == null) throw new Exception($"No se encontraron tableros en la base de datos");

        return ListaTableros;
    }

    
    // Listar todos los tableros de un usuario específico. (recibe un IdUsuario, devuelve un list de tableros).
    public List<Tablero> ListByUser(int id)
    {
        SqliteConnection connection = new (_cadenaConexion);

        List<Tablero> ListaTableros = new List<Tablero>();

        SqliteCommand command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario";
        command.Parameters.Add(new SqliteParameter("@idUsuario", id));
        

        connection.Open();
        using(SqliteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var tab = new Tablero();
                tab.Id = Convert.ToInt32(reader["id"]);
                tab.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                tab.Nombre = reader["nombre"].ToString();
                tab.Descripcion = reader["descripcion"].ToString();
                ListaTableros.Add(tab);
            }
        }
        connection.Close();

        if (ListaTableros == null) throw new Exception($"No se encontraron tableros asignados al usuario en la base de datos");

        return ListaTableros;
    }


    public List<Tablero> ListByTareasEnOtroTablero(int idUser)
    {
        SqliteConnection connection = new (_cadenaConexion);

        List<Tablero> ListaTablerosTareasAsign = new List<Tablero>();

        SqliteCommand command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Tarea t" +
                            "INNER JOIN Tablero tab ON (id_tablero = tab.id)" +
                            "WHERE (id_usuario_asignado = @idUser AND id_tablero NOT IN (SELECT id FROM Tablero WHERE id_usuario_propietario = @idUser))";

        command.Parameters.Add(new SqliteParameter("@idUser", idUser));
        
        connection.Open();
        using(SqliteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var tab = new Tablero();
                tab.Id = Convert.ToInt32(reader["id"]);
                tab.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                tab.Nombre = reader["nombre"].ToString();
                tab.Descripcion = reader["descripcion"].ToString();
                ListaTablerosTareasAsign.Add(tab);
            }
        }
        connection.Close();

        if (ListaTablerosTareasAsign == null) throw new Exception($"No se encontraron tableros asignados al usuario en la base de datos");

        return ListaTablerosTareasAsign;
    }


    // Listar todos los tableros que NO son de un usuario específico. (recibe un IdUsuario, devuelve un list de tableros).
    public List<Tablero> RestoDeTablerosListByUser(int idUser)
    {
        SqliteConnection connection = new (_cadenaConexion);

        List<Tablero> ListaTableros = new List<Tablero>();

        SqliteCommand command = connection.CreateCommand();


        command.CommandText = "SELECT * FROM Tablero WHERE id_usuario_propietario != @idUsuario";
        command.Parameters.Add(new SqliteParameter("@idUsuario", idUser));
        

        connection.Open();
        using(SqliteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var tab = new Tablero();
                tab.Id = Convert.ToInt32(reader["id"]);
                tab.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                tab.Nombre = reader["nombre"].ToString();
                tab.Descripcion = reader["descripcion"].ToString();
                ListaTableros.Add(tab);
            }
        }
        connection.Close();

        if (ListaTableros == null) throw new Exception($"No se encontraron tableros asignados al usuario en la base de datos");

        return ListaTableros;
    }


    // Eliminar un tablero por ID.
    public void Remove(int id)
    {
        using(SqliteConnection connection = new (_cadenaConexion))
        {
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Tablero WHERE id = @idTablero;";
            command.Parameters.Add(new SqliteParameter("@idTablero", id));

            connection.Open();
            
            var commandENonQ = command.ExecuteNonQuery();
            if (commandENonQ == 0) throw new Exception("Se produjo un error al eliminar el tablero");

            connection.Close();
        }
    }
}