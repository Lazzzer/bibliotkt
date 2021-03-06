using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Services.Interfaces;
using server.Utils;

namespace server.Services;

/// <summary>
/// Implémentation d'un service récupérant des données sur les exemplaires.
/// La connexion à la base de donnée est ouverte et fermée à chaque requête
/// </summary>
public class ExemplaireService : IExemplaireService
{
    private static NpgsqlConnection _connection = new();

    /// <summary>
    /// Constructeur du service
    /// La connection string de la base de données est transmise par injection de dépendances
    /// </summary>
    public ExemplaireService(IOptions<DbConnection> options)
    {
        _connection =
            new NpgsqlConnection(options.Value.ConnectionString);
    }

    /// <summary>
    /// Crée un record Exemplaire champs par champs depuis un reader obtenu d'une commande à la base de données
    /// </summary>
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

    public List<Exemplaire> GetExemplairesByIssn(int issn)
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

    public int GetNbExemplaires(int issn)
    {
        int nbExemplaire = 0;
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT COUNT(*) AS nbExemplaires FROM Exemplaire WHERE issnLivre = @issnLivre";
            command.Parameters.AddWithValue("@issnLivre", issn);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    nbExemplaire = reader.GetInt32(reader.GetOrdinal("nbExemplaires"));;
                }
            }
        }
        _connection.Close();
        
        return nbExemplaire;
    }

    public Exemplaire? GetExemplaireById(int id)
    {
        Exemplaire? exemplaire = null;
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Exemplaire where id =  @id";
            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    exemplaire = PopulateExemplaireRecord(reader);
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
            command.CommandText = @"INSERT INTO Exemplaire (issnLivre, idEdition) VALUES (@issnLivre, @idEdition) returning id";
            command.Parameters.AddWithValue("@issnLivre", exemplaire.issnLivre);
            command.Parameters.AddWithValue("@idEdition", exemplaire.idEdition);
            id = (int)(command.ExecuteScalar() ?? -1);
        }
        _connection.Close();
        return id;
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