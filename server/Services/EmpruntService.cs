using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Services.Interfaces;
using server.Utils;

namespace server.Services;
public class EmpruntService : IEmpruntService
{
    private static NpgsqlConnection _connection = new();

    public EmpruntService(IOptions<DbConnection> options)
    {
        _connection =
            new NpgsqlConnection(options.Value.ConnectionString);
    }

    public static Emprunt PopulateEmpruntRecord(NpgsqlDataReader reader, string key = "id")
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var id = reader.GetInt32(reader.GetOrdinal(key));
        var dateDebut = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("dateDébut"));
        var dateRetour = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("dateRetourPlanifié"));

        DateOnly? dateRendu = null;
        if (!reader.IsDBNull(reader.GetOrdinal("dateRendu")))
            dateRendu = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("dateRendu"));
        var etat = reader.GetString(reader.GetOrdinal("nomEtatUsure"));
        var idExemplaire = reader.GetInt32(reader.GetOrdinal("idExemplaire"));
        var idMembre = reader.GetInt32(reader.GetOrdinal("idMembre"));

        return new Emprunt(id, dateDebut, dateRetour, dateRendu, etat, idExemplaire, idMembre);
    }

    public IList<Emprunt> GetEmprunts()
    {
        var list = new List<Emprunt>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Emprunt";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(PopulateEmpruntRecord(reader));
                }
            }
        }
        
        _connection.Close();
        return list;
    }

    public Emprunt? GetEmpruntById(int id)
    {
        Emprunt? emprunt = null;
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Emprunt WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    emprunt = PopulateEmpruntRecord(reader);
                }
            }
        }
        
        _connection.Close();
        return emprunt;
    }

    public IList<Emprunt> GetEmpruntsByIdMembre(int id)
    {
        IList<Emprunt> emprunt = new List<Emprunt>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Emprunt WHERE idMembre = @id";
            command.Parameters.AddWithValue("@id", id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    emprunt.Add(PopulateEmpruntRecord(reader));
                }
            }
        }
        
        _connection.Close();
        return emprunt;
    }

    public IList<Emprunt> GetEmpruntsByIdExemplaire(int id)
    {
        IList<Emprunt> emprunt = new List<Emprunt>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Emprunt WHERE idExemplaire = @id";
            command.Parameters.AddWithValue("@id", id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    emprunt.Add(PopulateEmpruntRecord(reader));
                }
            }
        }
        
        _connection.Close();
        return emprunt;
    }

    public IList<Emprunt> GetEmpruntsActuels()
    {
        IList<Emprunt> emprunt = new List<Emprunt>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Emprunt WHERE dateRendu IS NULL";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    emprunt.Add(PopulateEmpruntRecord(reader));
                }
            }
        }
        
        _connection.Close();
        return emprunt;
    }
    
    public IList<Emprunt> GetEmpruntsActuelsEnRetard()
    {
        IList<Emprunt> emprunt = new List<Emprunt>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Emprunt WHERE dateRendu IS NULL AND CURRENT_DATE > dateRetourPlanifié";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    emprunt.Add(PopulateEmpruntRecord(reader));
                }
            }
        }
        
        _connection.Close();
        return emprunt;
    }

    public IList<Emprunt> GetEmpruntsEnRetard()
    {
        IList<Emprunt> emprunt = new List<Emprunt>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"SELECT * FROM Emprunt WHERE ((dateRendu IS NULL 
            AND CURRENT_DATE > dateRetourPlanifié) OR dateRendu > dateRetourPlanifié)";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    emprunt.Add(PopulateEmpruntRecord(reader));
                }
            }
        }
        
        _connection.Close();
        return emprunt;
    }
    public IList<Emprunt> GetEmpruntsEnRetardByIdMembre(int id)
    {
        IList<Emprunt> emprunt = new List<Emprunt>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"SELECT * FROM Emprunt WHERE idMembre = @id AND ((dateRendu IS NULL 
            AND CURRENT_DATE > dateRetourPlanifié) OR dateRendu > dateRetourPlanifié)";
            command.Parameters.AddWithValue("@id", id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    emprunt.Add(PopulateEmpruntRecord(reader));
                }
            }
        }
        
        _connection.Close();
        return emprunt;
    }
    
    public IList<Emprunt> GetEmpruntsEnRetardByIdExemplaire(int id)
    {
        IList<Emprunt> emprunt = new List<Emprunt>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"SELECT * FROM Emprunt WHERE idExemplaire = @id AND ((dateRendu IS NULL 
            AND CURRENT_DATE > dateRetourPlanifié) OR dateRendu > dateRetourPlanifié)";
            command.Parameters.AddWithValue("@id", id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    emprunt.Add(PopulateEmpruntRecord(reader));
                }
            }
        }
        
        _connection.Close();
        return emprunt;
    }

    public int Insert(Emprunt emprunt)
    {
        int id;

        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"INSERT INTO Emprunt (dateDébut, dateRetourPlanifié, dateRendu, nomEtatUsure, 
            idExemplaire, idMembre) VALUES (@début, @planifie, NULL, @etat, @idExemplaire, @idMembre) returning id";
            command.Parameters.AddWithValue("@début", emprunt.DateDebut);
            command.Parameters.AddWithValue("@planifie", emprunt.DateRetourPlanifie);
            command.Parameters.AddWithValue("@etat", emprunt.EtatUsure);
            command.Parameters.AddWithValue("@idExemplaire", emprunt.idExemplaire);
            command.Parameters.AddWithValue("@idMembre", emprunt.idMembre);
            id = (int)(command.ExecuteScalar() ?? -1);
        }
        _connection.Close();
        return id;
    }

    public int Update(Emprunt emprunt)
    {
        int affectedRows;
    
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            var renduValeur = "";
            if (emprunt.DateRendu != null)
                renduValeur = "dateRendu = @rendu,";
            command.CommandText = @"UPDATE Emprunt 
            SET dateDébut = @début, dateRetourPlanifié = @planifie," + renduValeur + @" nomEtatUsure = @etat, idExemplaire = @idExemplaire, idMembre = @idMembre
            WHERE id = @id";
            
            command.Parameters.AddWithValue("@id", emprunt.Id);
            command.Parameters.AddWithValue("@début", emprunt.DateDebut);
            command.Parameters.AddWithValue("@planifie", emprunt.DateRetourPlanifie);
            if (emprunt.DateRendu != null) command.Parameters.AddWithValue("@rendu", emprunt.DateRendu);
            command.Parameters.AddWithValue("@etat", emprunt.EtatUsure);
            command.Parameters.AddWithValue("@idExemplaire", emprunt.idExemplaire);
            command.Parameters.AddWithValue("@idMembre", emprunt.idMembre);
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
            command.CommandText = "DELETE FROM Emprunt WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            affectedRows = command.ExecuteNonQuery();
        }
        _connection.Close();
    
        return affectedRows;
    }
}