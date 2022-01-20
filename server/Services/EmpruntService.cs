using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
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

    public static Emprunt PopulateEmpruntRecord(NpgsqlDataReader reader, string key = "nom")
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var id = reader.GetInt32(reader.GetOrdinal(key));
        var dateDébut = reader.GetDateTime(reader.GetOrdinal("dateDébut"));
        var dateRetour = reader.GetDateTime(reader.GetOrdinal("dateRetourPlanifié"));
        var dateRendu = reader.GetDateTime(reader.GetOrdinal("dateRendu"));
        var état = reader.GetString(reader.GetOrdinal("nomEtatUsure"));
        var idExemplaire = reader.GetInt32(reader.GetOrdinal("idExemplaire"));
        var idMembre = reader.GetInt32(reader.GetOrdinal("idMembre"));

        return new Emprunt(id, new DateOnly(dateDébut.Year, dateDébut.Month, dateDébut.Day), new DateOnly(dateRetour.Year, dateRetour.Month, dateRetour.Day), new DateOnly(dateRendu.Year, dateRendu.Month, dateRendu.Day), état, idExemplaire, idMembre);
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

    public Emprunt? GetEmpruntsById(int id)
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

    public IList<Emprunt> GetEmpruntByIdMembre(int id)
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

    public IList<Emprunt> GetEmpruntByIdExemplaire(int id)
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

    public IList<Emprunt> GetEmpruntActuel()
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

    public IList<Emprunt> GetEmpruntEnRetardByIdMembre(int id)
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

    public int Insert(Emprunt emprunt)
    {
        int id;

        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"INSERT INTO Emprunt (dateDébut, dateRetourPlanifié, dateRendu, nomEtatUsure, 
            idExemplaire, idMembre) VALUES (@début, @planifié, @rendu, @etat, @idExemplaire, @idMembre) returning id";
            command.Parameters.AddWithValue("@début", emprunt.DateDebut);
            command.Parameters.AddWithValue("@planifié", emprunt.DateRetourPlanifie);
            command.Parameters.AddWithValue("@rendu", emprunt.DateRendu);
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
            command.CommandText = @"UPDATE Emprunt 
            SET dateDébut = @début, dateRetourPlanifié = @retour, dateRendu = @rendu, nomEtatUsure = @etat, idExemplaire = @idExemplaire, idMembre = @idMembre
            WHERE id = @id";
            
            command.Parameters.AddWithValue("@id", emprunt.Id);
            command.Parameters.AddWithValue("@début", emprunt.DateDebut);
            command.Parameters.AddWithValue("@planifié", emprunt.DateRetourPlanifie);
            command.Parameters.AddWithValue("@rendu", emprunt.DateRendu);
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