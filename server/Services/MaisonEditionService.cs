using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Utils;

namespace server.Services;

public class MaisonEditionService : IMaisonEditionService
{
    private static NpgsqlConnection _connection = new();

    public MaisonEditionService(IOptions<DbConnection> options)
    {
        _connection =
            new NpgsqlConnection(options.Value.ConnectionString);
    }

    public static MaisonEdition PopulateMaisonEditionRecord(NpgsqlDataReader reader, string key = "id")
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var id = reader.GetInt32(reader.GetOrdinal(key));
        var nom = reader.GetString(reader.GetOrdinal("nom"));
        var email = reader.GetString(reader.GetOrdinal("email"));
        var rue = reader.GetString(reader.GetOrdinal("rue"));
        var noRue = reader.GetInt32(reader.GetOrdinal("noRue"));
        var npa = reader.GetInt32(reader.GetOrdinal("npa"));
        var localite = reader.GetString(reader.GetOrdinal("localité"));
        var pays = reader.GetString(reader.GetOrdinal("pays"));

        return new MaisonEdition(id,nom,email,rue,noRue,npa,localite,pays, new List<Edition>());
    }
    
    public IList<MaisonEdition> GetMaisons()
    {
        var list = new List<MaisonEdition>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM MaisonEdition";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(PopulateMaisonEditionRecord(reader));
                }
            }
        }
        _connection.Close();

        return list;
    }

    public MaisonEdition? GetMaisonById(int id)
    {
        MaisonEdition? maison = null;
        var editions = new List<Edition>();
        
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"SELECT
                                        *,
                                        edition.id AS idEdition
                                    FROM
                                        MaisonEdition
                                        LEFT JOIN Edition ON maisonedition.id = edition.idmaisonedition
                                    WHERE
                                    maisonedition.id = @id";
            command.Parameters.AddWithValue("id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    maison = PopulateMaisonEditionRecord(reader);
                    if (!reader.IsDBNull(reader.GetOrdinal("idedition")) && !editions.Exists(e => e.Id == reader.GetInt32(reader.GetOrdinal("idedition"))))
                    {
                        editions.Add(EditionService.PopulateEditionRecord(reader, "idedition"));
                    }
                }
            }
        }
        _connection.Close();

        if (maison != null)
            return maison with {Editions = editions};
        
        return null;
    }

    public int Insert(MaisonEdition maison)
    {
        int id;
        
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO MaisonEdition (nom, email, rue, noRue, npa, localité, pays) VALUES (@nom, @email, @rue, @noRue, @npa, @localite, @pays) returning id";
            command.Parameters.AddWithValue("@nom", maison.Nom);
            command.Parameters.AddWithValue("@email", maison.Email);
            command.Parameters.AddWithValue("@rue", maison.Rue);
            command.Parameters.AddWithValue("@noRue", maison.NoRue);
            command.Parameters.AddWithValue("@npa", maison.Npa);
            command.Parameters.AddWithValue("@localite", maison.Localite);
            command.Parameters.AddWithValue("@pays", maison.Pays);
            id = (int)(command.ExecuteScalar() ?? -1);
        }
        _connection.Close();
        return id;
    }

    public int Update(MaisonEdition maison)
    {
        int affectedRows;
    
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "UPDATE MaisonEdition SET nom = @nom, email = @email, rue = @rue, noRue = @noRue, npa = @npa, localité = @localite, pays = @pays WHERE id = @id";
            command.Parameters.AddWithValue("@id", maison.Id);
            command.Parameters.AddWithValue("@nom", maison.Nom);
            command.Parameters.AddWithValue("@email", maison.Email);
            command.Parameters.AddWithValue("@rue", maison.Rue);
            command.Parameters.AddWithValue("@noRue", maison.NoRue);
            command.Parameters.AddWithValue("@npa", maison.Npa);
            command.Parameters.AddWithValue("@localite", maison.Localite);
            command.Parameters.AddWithValue("@pays", maison.Pays);
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
            command.CommandText = "DELETE FROM MaisonEdition WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            affectedRows = command.ExecuteNonQuery();
        }
        _connection.Close();
    
        return affectedRows;
    }
}