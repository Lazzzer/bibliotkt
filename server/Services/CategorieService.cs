﻿using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Utils;

namespace server.Services;

public class CategorieService : ICategorieService
{
    private static NpgsqlConnection _connection = new();

    public CategorieService(IOptions<DbConnection> options)
    {
        _connection =
            new NpgsqlConnection(options.Value.ConnectionString);
    }

    public static Categorie PopulateCategorieRecord(NpgsqlDataReader reader, string key = "nom")
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));
        
        var nom = reader.GetString(reader.GetOrdinal(key));

        return new Categorie(nom, new List<Livre>());
    }

    public IList<Categorie> GetCategories()
    {
        var list = new List<Categorie>();
        var query = "SELECT * FROM Categorie";
        
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = query;

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
        var query = "SELECT * FROM Categorie WHERE nom = @nom";
        
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = query;
            command.Parameters.AddWithValue("nom", nom);

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
    
    public string? Insert(Categorie categorie)
    {
        string? nom = null;
        
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Catégorie (nom) VALUES (@nom) returning nom";
            command.Parameters.AddWithValue("@nom", categorie.Nom);
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
            command.CommandText = "UPDATE Catégorie SET nom = @newNom WHERE nom = @nom";
            command.Parameters.AddWithValue("@newNom", newNom);
            command.Parameters.AddWithValue("@nom", nom);
            affectedRows = command.ExecuteNonQuery();
        }
        _connection.Close();
    
        return affectedRows;
    }
    
    public int Delete(Categorie cat)
    {
        int affectedRows;
    
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM Categorie WHERE nom = @nom";
            command.Parameters.AddWithValue("@nom", cat.Nom);
            affectedRows = command.ExecuteNonQuery();
        }
        _connection.Close();
    
        return affectedRows;
    }
}