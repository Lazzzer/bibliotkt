using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Utils;

namespace server.Services;

public class ExemplaireService : IExemplaireService
{
    private static NpgsqlConnection _connection = new();

    public ExemplaireService(IOptions<DbConnection> options)
    {
        _connection =
            new NpgsqlConnection(options.Value.ConnectionString);
    }

    public static Exemplaire PopulateExemplaireRecord(NpgsqlDataReader reader, string key = "id")
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));
        
        var id = reader.GetInt32(reader.GetOrdinal(key));
        var idEdition = reader.GetInt32(reader.GetOrdinal("idEdition"));
        var issn = reader.GetInt32(reader.GetOrdinal("issnLivre"));

        return new Exemplaire(id, issn, idEdition);
    }
    
    public IList<Exemplaire> GetExemplaires()
    {
        var list = new List<Exemplaire>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Exemplaire";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(PopulateExemplaireRecord(reader));
                }
            }
        }
        
        _connection.Close();
        return list;
    }

    public List<Exemplaire> GetExemplaireByIssn(int issn)
    {
        var exemplaire = new List<Exemplaire>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Exemplaire WHERE issnLivre = @issn";
            command.Parameters.AddWithValue("@issn", issn);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    exemplaire.Add(PopulateExemplaireRecord(reader));
                }
            }
        }
        
        _connection.Close();
        return exemplaire;
    }

    public int Insert(Exemplaire exemplaire)
    {
        int id;

        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"INSERT INTO Exemplaire (issnLivre, idEdition) VALUES (@issn, @idEdition) returning id";
            command.Parameters.AddWithValue("@issnLivre", exemplaire.issnLivre);
            command.Parameters.AddWithValue("@idEdition", exemplaire.idEdition);
            id = (int)(command.ExecuteScalar() ?? -1);
        }
        _connection.Close();
        return id;
    }

    public int Update(Exemplaire exemplaire)
    {
        int affectedRows;
    
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"UPDATE Exemplaire SET issnLivre = @issnLivre, idEdition = @idEdition";
            command.Parameters.AddWithValue("@issnLivre", exemplaire.issnLivre);
            command.Parameters.AddWithValue("@idEdition", exemplaire.idEdition);
            affectedRows = command.ExecuteNonQuery();
        }
        _connection.Close();
    
        return affectedRows;
    }

    public int Delete(int id)
    {
        int affectedRows;
    
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM Exemplaire WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            affectedRows = command.ExecuteNonQuery();
        }
        _connection.Close();
    
        return affectedRows;
    }
}