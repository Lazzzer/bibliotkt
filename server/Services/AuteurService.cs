using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;

namespace server.Services;

public class AuteurService : IAuteurService
{
  private static NpgsqlConnection _connection = new();
  public AuteurService(IOptions<DbConnection> options)
  {
    _connection =
        new NpgsqlConnection(options.Value.ConnectionString);
  }

  private static Auteur PopulateAuteurRecord(NpgsqlDataReader reader)
  {
    if (reader == null) throw new ArgumentNullException(nameof(reader));

    var auteurId = reader.GetInt32(reader.GetOrdinal("id"));
    var nom = reader.GetString(reader.GetOrdinal("nom"));
    var prenom = reader.GetString(reader.GetOrdinal("prénom"));

    return new Auteur(auteurId, nom, prenom);
  }

  public IList<Auteur> GetAuteurs(int? limit, int? offset)
  {
    var list = new List<Auteur>();
    var query = "SELECT * FROM Auteur LIMIT @limit OFFSET @offset";
    var hasParameters = true;

    if (limit < 0 || offset < 0)
    {
      throw new ArgumentException("Offset or limit should be equal or higher than 0.");
    }

    if (limit == null && offset == null)
    {
      query = "SELECT * FROM Auteur";
      hasParameters = false;
    }
    _connection.Open();
    using (var command = _connection.CreateCommand())
    {
      command.CommandText = query;
      if (hasParameters)
      {
        command.Parameters.AddWithValue("@limit", limit);
        command.Parameters.AddWithValue("@offset", offset);
      }

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

  public IList<Auteur> GetAuteursByNames(string? nom, string? prenom)
  {
    throw new NotImplementedException();
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

  public void Update(Auteur auteur)
  {
    throw new NotImplementedException();
  }

  public void Delete(Auteur auteur)
  {
    throw new NotImplementedException();
  }
}