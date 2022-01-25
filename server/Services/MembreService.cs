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
        var rue = reader.GetString(reader.GetOrdinal("rue"));
        var noRue = reader.GetInt32(reader.GetOrdinal("noRue"));
        var npa = reader.GetInt32(reader.GetOrdinal("npa"));
        var localité = reader.GetString(reader.GetOrdinal("localité"));
        var dateCreation = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("dateCreationCompte"));

        return new Membre(id,nom, prénom, rue, noRue, npa, localité, dateCreation, new List<Emprunt>());
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
        var emprunts = new List<Emprunt>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"SELECT
                                        *,
                                        emprunt.id AS idEmprunt
                                    FROM
                                        Membre
                                        INNER JOIN Personne ON personne.id = idPersonne
                                        LEFT JOIN emprunt ON membre.idpersonne = emprunt.idmembre
                                    WHERE
                                        idPersonne = @id";
            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    membre = PopulateMembreRecord(reader);
                    if (!reader.IsDBNull(reader.GetOrdinal("idemprunt")) && !emprunts.Exists(e => e.Id == reader.GetInt32(reader.GetOrdinal("idemprunt"))))
                    {
                        emprunts.Add(EmpruntService.PopulateEmpruntRecord(reader, "idemprunt"));
                    }
                }
            }
        }
        _connection.Close();
        if (membre != null) 
            return membre with {Emprunts = emprunts};
        
        return null;
    }

    public int Insert(Membre membre)
    {
        var id = InsertPersonne(membre);
        if (id == -1)
            return 0;
        
        int idMembre;
    
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Membre(idPersonne) VALUES (@id) RETURNING idpersonne";
            command.Parameters.AddWithValue("@id", id);
            id = (int)(command.ExecuteScalar() ?? -1);
        }
        _connection.Close();
    
        return id;
    }

    public int Update(Membre membre)
    {
        return UpdatePersonne(membre);
    }

    public int Delete(int id)
    {
        return DeletePersonne(id);
    }
    
    private int InsertPersonne(Personne membre)
    {
        int id;
        
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
            id = (int)(command.ExecuteScalar() ?? -1);
        }
        _connection.Close();
    
        return id;
    }
    
    private int UpdatePersonne(Personne membre)
    {
        int affectedRows;
    
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "UPDATE Personne SET nom = @nom, prénom = @prénom, rue = @rue, noRue = @noRue, npa = @npa, localité = @localité, datecreationcompte = @date WHERE id = @id";
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
    
    private int DeletePersonne(int id)
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