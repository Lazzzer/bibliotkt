using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Services.Interfaces;
using server.Utils;

namespace server.Services;

public class EditionService : IEditionService
{
    private static NpgsqlConnection _connection = new();

    public EditionService(IOptions<DbConnection> options)
    {
        _connection =
            new NpgsqlConnection(options.Value.ConnectionString);
    }
    
    public static Edition PopulateEditionRecord(NpgsqlDataReader reader, string key = "id")
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var id = reader.GetInt32(reader.GetOrdinal(key));
        var issn = reader.GetInt32(reader.GetOrdinal("ISSNLivre"));
        var idMaison = reader.GetInt32(reader.GetOrdinal("idMaisonEdition"));
        var type = reader.GetFieldValue<TypeEdition>(reader.GetOrdinal("type"));
        var langue = reader.GetFieldValue<Langue>(reader.GetOrdinal("langue"));

        return new Edition(id,issn, idMaison, type, langue, new List<Exemplaire>());
    }
    
    public IList<Edition> GetEditions()
    {
        var list = new List<Edition>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Edition";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(PopulateEditionRecord(reader));
                }
            }
        }
        _connection.Close();

        return list;
    }

    public Edition? GetEditionById(int id)
    {
        Edition? edition = null;
        var exemplaires = new List<Exemplaire>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"SELECT
                                        *,
                                        edition.id AS idEdition,
                                        exemplaire.id AS idExemplaire
                                    FROM
                                        Edition
                                        LEFT JOIN Exemplaire ON edition.id = exemplaire.idedition
                                    WHERE
                                        edition.id = @id";
            command.Parameters.AddWithValue("id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    edition = PopulateEditionRecord(reader);
                    if (!reader.IsDBNull(reader.GetOrdinal("idexemplaire")) && !exemplaires.Exists(e => e.Id == reader.GetInt32(reader.GetOrdinal("idexemplaire"))))
                    {
                        exemplaires.Add(ExemplaireService.PopulateExemplaireRecord(reader, "idexemplaire"));
                    }
                }
            }
        }
        _connection.Close();
        if (edition != null) 
            return edition with {Exemplaires = exemplaires};
            
        return null;
    }

    public IList<Edition> GetEditionsByIssn(int issn)
    {
        var list = new List<Edition>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"SELECT * FROM Edition WHERE ISSNLivre = @issn";
            command.Parameters.AddWithValue("issn", issn);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(PopulateEditionRecord(reader));
                }
            }
        }
        _connection.Close();

        return list;
    }

    public int Insert(Edition edition)
    {
        int id;
        
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Edition (ISSNLivre, idMaisonEdition, type, langue) VALUES (@issn, @idMaison, @type, @langue) returning id";
            command.Parameters.AddWithValue("@issn", edition.issn);
            command.Parameters.AddWithValue("@idMaison", edition.idMaison);
            command.Parameters.AddWithValue("@type", edition.Type);
            command.Parameters.AddWithValue("@langue", edition.Langue);
            id = (int)(command.ExecuteScalar() ?? -1);
        }
        _connection.Close();
        return id;
    }

    public int Update(Edition edition)
    {
        int affectedRows;
    
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "UPDATE Edition SET ISSNLivre = @issn, idMaisonEdition = @idMaison, type = @type, langue = @langue WHERE id = @id";
            command.Parameters.AddWithValue("@id", edition.Id);
            command.Parameters.AddWithValue("@issn", edition.issn);
            command.Parameters.AddWithValue("@idMaison", edition.idMaison);
            command.Parameters.AddWithValue("@type", edition.Type);
            command.Parameters.AddWithValue("@langue", edition.Langue);
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
            command.CommandText = "DELETE FROM Edition WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            affectedRows = command.ExecuteNonQuery();
        }
        _connection.Close();
    
        return affectedRows;
    }
}