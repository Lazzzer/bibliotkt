using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Services.Interfaces;
using server.Utils;

namespace server.Services;

/// <summary>
/// Implémentation d'un service récupérant des données sur les états d'usures.
/// La connexion à la base de donnée est ouverte et fermée à chaque requête
/// </summary>
public class EtatUsureService : IEtatUsureService
{
    private static NpgsqlConnection _connection = new();

    /// <summary>
    /// Constructeur du service
    /// La connection string de la base de données est transmise par injection de dépendances
    /// </summary>
    public EtatUsureService(IOptions<DbConnection> options)
    {
        _connection = new NpgsqlConnection(options.Value.ConnectionString);
    }

    /// <summary>
    /// Crée un record EtatUsure champs par champs depuis un reader obtenu d'une commande à la base de données
    /// </summary>
    public static EtatUsure PopulateEtatUsureRecord(NpgsqlDataReader reader, string key = "nom")
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var nom = reader.GetString(reader.GetOrdinal(key));

        return new EtatUsure(nom);
    }

    public IList<EtatUsure> GetEtatsUsure()
    {
        var list = new List<EtatUsure>();
        var query = "SELECT * FROM EtatUsure";

        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = query;

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(PopulateEtatUsureRecord(reader));
                }
            }
        }

        _connection.Close();

        return list;
    }

    public EtatUsure? GetEtatUsureByNom(string nom)
    {
        EtatUsure? etat = null;
        var query = "SELECT * FROM EtatUsure WHERE nom = @nom";

        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = query;
            command.Parameters.AddWithValue("@nom", nom);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    etat = PopulateEtatUsureRecord(reader);
                }
            }
        }

        _connection.Close();

        return etat;
    }

    public string? Insert(string nomId)
    {
        string? nom = null;

        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO EtatUsure (nom) VALUES (@nom) returning nom";
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
            command.CommandText = "UPDATE EtatUsure SET nom = @newNom WHERE nom = @nom";
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
            command.CommandText = "DELETE FROM EtatUsure WHERE nom = @nom";
            command.Parameters.AddWithValue("@nom", nom);
            affectedRows = command.ExecuteNonQuery();
        }

        _connection.Close();

        return affectedRows;
    }
}