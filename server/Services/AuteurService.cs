/**
 * implémente les différentes reliées à la table auteur
 *
 * La connexion est ouverte et fermée à chaque requête
 *
 * Chacune des méthodes de récupération de données va peupler les records correspondants
 */
using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Services.Interfaces;
using server.Utils;

namespace server.Services;

public class AuteurService : IAuteurService
{
  private static NpgsqlConnection _connection = new();
  
  public AuteurService(IOptions<DbConnection> options)
  {
    _connection =
        new NpgsqlConnection(options.Value.ConnectionString);
  }

  /**
   * Crée un record du type auteur
   */
  public static Auteur PopulateAuteurRecord(NpgsqlDataReader reader, string key = "id")
  {
    if (reader == null) throw new ArgumentNullException(nameof(reader));

    var auteurId = reader.GetInt32(reader.GetOrdinal(key));
      var nom = reader.GetString(reader.GetOrdinal("nom"));
    var prenom = reader.GetString(reader.GetOrdinal("prénom"));

    return new Auteur(auteurId, nom, prenom, new List<Livre>());
  }

  public IList<Auteur> GetAuteurs()
  {
    var list = new List<Auteur>();
    _connection.Open();
    using (var command = _connection.CreateCommand())
    {
      command.CommandText = "SELECT * FROM Auteur";
      using (var reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          list.Add(PopulateAuteurRecord(reader));
        }
      }
    }
    _connection.Close();
    return list;
  }

  public Auteur? GetAuteurById(int id)
  {
    Auteur? auteur = null;
    _connection.Open();
    using (var command = _connection.CreateCommand())
    {
      command.CommandText = "SELECT * FROM Auteur WHERE id = @id";
      command.Parameters.AddWithValue("@id", id);

      using (var reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          auteur = PopulateAuteurRecord(reader);
        }
      }
    }
    _connection.Close();
    return auteur;
  }

  public Auteur? GetAuteurByIdWithLivres(int id)
  {
    Auteur? auteur = null;
    var livres = new List<Livre>();
    _connection.Open();
    using (var command = _connection.CreateCommand())
    {
      command.CommandText = "SELECT * FROM Auteur LEFT JOIN Livre_Auteur ON Auteur.id = Livre_Auteur.idAuteur LEFT JOIN Livre ON Livre_Auteur.ISSNLivre = Livre.ISSN WHERE Auteur.id = @id";
      command.Parameters.AddWithValue("id", id);

      using (var reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          auteur = PopulateAuteurRecord(reader);
          if (!reader.IsDBNull(reader.GetOrdinal("issn")))
          {
            livres.Add(LivreService.PopulateLivreRecord(reader));
          }
        }
      }
    }
    _connection.Close();
    if (auteur != null && livres.Count > 0)
    {
      return auteur with {Livres = livres};
    }
    return auteur;
  }


  public int Insert(Auteur auteur)
  {
    int id;

    _connection.Open();
    using (var command = _connection.CreateCommand())
    {
      command.CommandText = "INSERT INTO Auteur (nom, prénom) VALUES (@nom, @prenom) returning id";
      command.Parameters.AddWithValue("@nom", auteur.Nom);
      command.Parameters.AddWithValue("@prenom", auteur.Prenom);
      id = (int)(command.ExecuteScalar() ?? -1);
    }
    _connection.Close();
    return id;
  }

  public int Update(Auteur auteur)
  {
    int affectedRows;
    
    _connection.Open();
    using (var command = _connection.CreateCommand())
    {
      command.CommandText = "UPDATE Auteur SET nom = @nom, prénom = @prenom WHERE id = @id";
      command.Parameters.AddWithValue("@id", auteur.Id);
      command.Parameters.AddWithValue("@nom", auteur.Nom);
      command.Parameters.AddWithValue("@prenom", auteur.Prenom);
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
      command.CommandText = "DELETE FROM Auteur WHERE id = @id";
      command.Parameters.AddWithValue("@id", id);
      affectedRows = command.ExecuteNonQuery();
    }
    _connection.Close();
    
    return affectedRows;
  }
}