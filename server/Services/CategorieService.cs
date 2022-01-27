using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Services.Interfaces;
using server.Utils;

namespace server.Services;

/// <summary>
/// Implémentation d'un service récupérant des données sur les catégories.
/// La connexion à la base de donnée est ouverte et fermée à chaque requête
/// </summary>
public class CategorieService : ICategorieService
{
    private static NpgsqlConnection _connection;

    /// <summary>
    /// Constructeur du service
    /// La connection string de la base de données est transmise par injection de dépendances
    /// </summary>
    public CategorieService(IOptions<DbConnection> options)
    {
        _connection = new NpgsqlConnection(options.Value.ConnectionString);
    }

    /// <summary>
    /// Crée un record Categorie champs par champs depuis un reader obtenu d'une commande à la base de données
    /// </summary>
    public static Categorie PopulateCategorieRecord(NpgsqlDataReader reader, string key = "nom")
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var nom = reader.GetString(reader.GetOrdinal(key));

        return new Categorie(nom);
    }

    public IList<Categorie> GetCategories()
    {
        var list = new List<Categorie>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Catégorie";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(PopulateCategorieRecord(reader));
                }
            }
        }

        _connection.Close();

        return list;
    }

    public Categorie? GetCategorieByNom(string nom)
    {
        Categorie? cat = null;
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Catégorie WHERE nom = @nom";
            command.Parameters.AddWithValue("@nom", nom);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    cat = PopulateCategorieRecord(reader);
                }
            }
        }

        _connection.Close();

        return cat;
    }

    public string? Insert(string nomId)
    {
        string? nom;

        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Catégorie (nom) VALUES (@nom) returning nom";
            command.Parameters.AddWithValue("@nom", nomId);
            nom = (string?) (command.ExecuteScalar() ?? null);
        }

        _connection.Close();

        return nom;
    }

    public int Update(string nom, string newNom)
    {
        int affectedRows;

        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "UPDATE Catégorie SET nom = @newNom WHERE nom = @nom";
            command.Parameters.AddWithValue("@newNom", newNom);
            command.Parameters.AddWithValue("@nom", nom);
            affectedRows = command.ExecuteNonQuery();
        }

        _connection.Close();

        return affectedRows;
    }

    public int Delete(string nom)
    {
        int affectedRows;

        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM Catégorie WHERE nom = @nom";
            command.Parameters.AddWithValue("@nom", nom);
            affectedRows = command.ExecuteNonQuery();
        }

        _connection.Close();

        return affectedRows;
    }
}