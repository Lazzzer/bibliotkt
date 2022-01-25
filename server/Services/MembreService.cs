using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Services.Interfaces;
using server.Utils;

namespace server.Services;

public class MembreService : IMembreService
{
    private static NpgsqlConnection _connection = new();

    public MembreService(IOptions<DbConnection> options)
    {
        _connection =
            new NpgsqlConnection(options.Value.ConnectionString);
    }
    
    public static Membre PopulateMembreRecord(NpgsqlDataReader reader, string key = "id")
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var id = reader.GetInt32(reader.GetOrdinal(key));
        var nom = reader.GetString(reader.GetOrdinal("nom"));
        var prénom = reader.GetString(reader.GetOrdinal("prénom"));
        var droitEmprunt = reader.GetBoolean(reader.GetOrdinal("droitEmprunt"));
        var rue = reader.GetString(reader.GetOrdinal("rue"));
        var noRue = reader.GetInt32(reader.GetOrdinal("noRue"));
        var npa = reader.GetInt32(reader.GetOrdinal("npa"));
        var localité = reader.GetString(reader.GetOrdinal("localité"));
        var dateCreation = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("dateCreationCompte"));

        return new Membre(id,nom, prénom, rue, noRue, npa, localité, dateCreation, droitEmprunt, new List<Emprunt>());
    }

    public IList<Membre> GetMembres()
    {
        var list = new List<Membre>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Membre INNER JOIN Personne ON personne.id = idPersonne";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(PopulateMembreRecord(reader));
                }
            }
        }
        _connection.Close();
        return list;
    }

    public Membre? GetMembreById(int id)
    {
        Membre? membre = null;
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Membre INNER JOIN Personne ON personne.id = idPersonne WHERE idPersonne = @id";
            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    membre = PopulateMembreRecord(reader);
                }
            }
        }
        _connection.Close();
        return membre;
    }

    public Membre? GetMembreByIdWithEmprunt(int id)
    {
        Membre? membre = null;
        var emprunts = new List<Emprunt>();

        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText =
                "SELECT * FROM Membre INNER JOIN Personne ON personne.id = idPersonne LEFT JOIN Emprunt ON idPersonne = Emprunt.idMembre WHERE membre.idPersonne = @id";
            command.Parameters.AddWithValue("id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    membre = PopulateMembreRecord(reader);
                    if (!reader.IsDBNull(reader.GetOrdinal("ISSNLivre")))
                    {
                        emprunts.Add(EmpruntService.PopulateEmpruntRecord(reader));
                    }
                }
            }

            if (membre != null && emprunts.Count > 0)
            {
                return membre with {Emprunts = emprunts};
            }

            return membre;
        }
    }

    public int Insert(Membre membre)
    {
        int affectedRows;
    
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            int? id = InsertPersonne(membre);

            if (id == null)
            {
                throw new Exception("Problème lors de l'insert de personne");
            }
            command.CommandText = "INSERT INTO Membre(idPersonne, droitEmprunt) VALUES (1,FALSE)";
            command.Parameters.AddWithValue("@id", id);
            affectedRows = command.ExecuteNonQuery();
        }
        _connection.Close();
    
        return affectedRows;
    }

    public int Update(Membre membre)
    {
        int affectedRows;
    
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            UpdatePersonne(membre);
            command.CommandText = "UPDATE Membre SET droitEmprunt = @droit WHERE id = @id";
            command.Parameters.AddWithValue("@id", membre.Id);
            command.Parameters.AddWithValue("@droit", membre.DroitEmprunt);
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
            affectedRows = DeletePersonne(id);
        }
        _connection.Close();
    
        return affectedRows;
    }
    
    private int? InsertPersonne(Membre membre)
    {
        int? id;
        
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Personne (nom, prénom, rue, noRue, NPA, localité, dateCreationCompte) VALUES (@nom, @prénom, @rue, @noRue, @npa, @localité, @date) returning id";
            command.Parameters.AddWithValue("@nom", membre.Nom);
            command.Parameters.AddWithValue("@prénom", membre.Prenom);
            command.Parameters.AddWithValue("@rue", membre.Rue);
            command.Parameters.AddWithValue("@noRue", membre.NoRue);
            command.Parameters.AddWithValue("@npa", membre.Npa);
            command.Parameters.AddWithValue("@localité", membre.Localite);
            command.Parameters.AddWithValue("@date", membre.DateCreationCompte);
            id = (int?)((command.ExecuteScalar() ?? null));
        }
        _connection.Close();
    
        return id;
    }
    
    private int UpdatePersonne(Membre membre)
    {
        int affectedRows;
    
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "UPDATE Personne SET nom = @nom, prénom = @prenom, rue = @rue, noRue = @noRue, npa = @npa, localité = @localité, date = @date WHERE id = @id";
            command.Parameters.AddWithValue("@id", membre.Id);
            command.Parameters.AddWithValue("@nom", membre.Nom);
            command.Parameters.AddWithValue("@prénom", membre.Prenom);
            command.Parameters.AddWithValue("@rue", membre.Rue);
            command.Parameters.AddWithValue("@noRue", membre.NoRue);
            command.Parameters.AddWithValue("@npa", membre.Npa);
            command.Parameters.AddWithValue("@localité", membre.Localite);
            command.Parameters.AddWithValue("@date", membre.DateCreationCompte);
            affectedRows = command.ExecuteNonQuery();
        }
        _connection.Close();
    
        return affectedRows;
    }
    
    public int DeletePersonne(int id)
    {
        int affectedRows;
    
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM Personne WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            affectedRows = command.ExecuteNonQuery();
        }
        _connection.Close();
    
        return affectedRows;
    }
}