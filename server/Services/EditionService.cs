﻿using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
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
    
    public static Edition PopulateEditionRecord(NpgsqlDataReader reader, string key = "nom")
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var id = reader.GetInt32(reader.GetOrdinal(key));
        var issn = reader.GetInt32(reader.GetOrdinal("ISSNLivre"));
        var idMaison = reader.GetInt32(reader.GetOrdinal("idMaisonEdition"));
        var type = (TypeEdition)reader.GetValue(reader.GetOrdinal("type"));
        var langue = (Langue)reader.GetValue(reader.GetOrdinal("langue"));

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
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Edition WHERE id = @id";
            command.Parameters.AddWithValue("id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    edition = PopulateEditionRecord(reader);
                }
            }
        }
        
        _connection.Close();
        return edition;
    }

    public IList<Edition>? GetEditionByIssn(int issn)
    {
        var list = new List<Edition>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Edition WHERE ISSNLivre = @issn";
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

    public IList<Edition>? GetEditionByIdMaison(int idMaison)
    {
        var list = new List<Edition>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Edition WHERE idMaisonEdition = @idMaison";
            command.Parameters.AddWithValue("idMaison", idMaison);

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

    public IList<Edition>? GetEditionByType(TypeEdition type)
    {
        var list = new List<Edition>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Edition WHERE type = @type";
            command.Parameters.AddWithValue("type", type);

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

    public IList<Edition>? GetEditionByLangue(Langue langue)
    {
        var list = new List<Edition>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Edition WHERE Langue = @langue";
            command.Parameters.AddWithValue("langue", langue);

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
            id = (int)(command.ExecuteScalar());
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
            command.CommandText = "UPDATE Edition SET ISSNLivre = @issn, idMaison = @idMaison, type = @type, langue = @langue WHERE id = @id";
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