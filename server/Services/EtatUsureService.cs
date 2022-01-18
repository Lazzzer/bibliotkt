using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Utils;

namespace server.Services;

public class EtatUsureService : IEtatUsureService
{
    private static NpgsqlConnection _connection = new();

    public EtatUsureService(IOptions<DbConnection> options)
    {
        _connection =
            new NpgsqlConnection(options.Value.ConnectionString);
    }

    public static EtatUsure PopulateEtatUsureRecord(NpgsqlDataReader reader, string key = "nom")
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));
        
        var nom = reader.GetString(reader.GetOrdinal(key));

        return new EtatUsure(nom);
    }
    
    public IList<EtatUsure> GetCategories()
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

    public EtatUsure? GetCategorieByNom(string nom)
    {
        EtatUsure? etat = null;
        var query = "SELECT * FROM EtatUsure WHERE nom = @nom";
        
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = query;
            command.Parameters.AddWithValue("nom", nom);

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

    public string? Insert(EtatUsure etat)
    {
        string? nom = null;
        
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Catégorie (nom) VALUES (@nom) returning nom";
            command.Parameters.AddWithValue("@nom", etat.Nom);
            nom = (string?)(command.ExecuteScalar() ?? null);
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

    public int Delete(EtatUsure etat)
    {
        int affectedRows;
    
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM EtatUsure WHERE nom = @nom";
            command.Parameters.AddWithValue("@nom", etat.Nom);
            affectedRows = command.ExecuteNonQuery();
        }
        _connection.Close();
    
        return affectedRows;
    }
}