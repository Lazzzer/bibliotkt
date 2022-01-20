using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Utils;

namespace server.Services;

public class LivreService : ILivreService
{
    private static NpgsqlConnection _connection = new();

    public LivreService(IOptions<DbConnection> options)
    {
        _connection =
            new NpgsqlConnection(options.Value.ConnectionString);
    }

    public static Livre PopulateLivreRecord(NpgsqlDataReader reader, string key = "issn")
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var issn = reader.GetInt32(reader.GetOrdinal(key));
        var titre = reader.GetString(reader.GetOrdinal("titre"));
        var synospis = reader.GetString(reader.GetOrdinal("synopsis"));
        var dateParution = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("dateParution"));
        var dateAcquisition = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("dateAcquisition"));
        var prixAchat = reader.GetInt32(reader.GetOrdinal("prixAchat"));
        var prixEmprunt = reader.GetInt32(reader.GetOrdinal("prixEmprunt"));

        return new Livre(issn, titre, synospis, dateParution, dateAcquisition, prixAchat, prixEmprunt,
            new List<Auteur>(), new List<Categorie>());
    }

    public IList<Livre> GetLivres()
    {
        var list = new List<Livre>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Livre";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(PopulateLivreRecord(reader));
                }
            }

            _connection.Close();
            return list;
        }
    }

    public Livre? GetLivreByIssn(int issn)
    {
        Livre? livre = null;
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Livre WHERE issn = @issn";
            command.Parameters.AddWithValue("@issn", issn);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    livre = PopulateLivreRecord(reader);
                }
            }
        }

        _connection.Close();
        return livre;
    }

    public Livre? GetLivreByIssnWithAuteursEtCategories(int issn)
    {
        Livre? livre = null;
        var auteurs = new List<Auteur>();
        var categories = new List<Categorie>();

        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText =
                "SELECT*FROM Livre LEFT JOIN Livre_Auteur ON Livre.issn = Livre_Auteur.issnlivre LEFT JOIN Auteur ON Livre_Auteur.idAuteur = Auteur.id LEFT JOIN Livre_Catégorie ON Livre.issn = Livre_Catégorie.issnLivre LEFT JOIN Catégorie ON Livre_Catégorie.nomCatégorie = Catégorie.nom WHERE Livre.issn = @issn";
            command.Parameters.AddWithValue("issn", issn);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    livre = PopulateLivreRecord(reader);
                    if (!reader.IsDBNull(reader.GetOrdinal("idauteur")) && !auteurs.Exists(a => a.Id == reader.GetInt32(reader.GetOrdinal("idauteur"))))
                    {
                        auteurs.Add(AuteurService.PopulateAuteurRecord(reader, "idauteur"));
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("nomcatégorie")) && !categories.Exists(c => c.Nom == reader.GetString(reader.GetOrdinal("nomcatégorie"))))
                    {
                        categories.Add(CategorieService.PopulateCategorieRecord(reader, "nomcatégorie"));
                    }
                }
            }

            if (livre != null && auteurs.Count > 0)
            {
                return livre with {Auteurs = auteurs, Categories = categories};
            }

            if (livre != null) 
                return livre with {Categories = categories};

            return livre;
        }
    }

    public IList<Livre> GetLivresByTitle(string title)
    {
        var list = new List<Livre>();
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM LIVRE WHERE titre ILIKE @text";
            command.Parameters.AddWithValue("@text", "%" + title.Trim() + "%");

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(PopulateLivreRecord(reader));
                }
            }

            _connection.Close();
            return list;
        }
    }

    public IList<Livre> GetLivresByFilters(string? nomAuteur, string? langue, string[]? nomCategories)
    {
        throw new NotImplementedException();
    }

    public int Insert(Livre auteur)
    {
        throw new NotImplementedException();
    }

    public void Update(Livre auteur)
    {
        throw new NotImplementedException();
    }

    public void Delete(Livre auteur)
    {
        throw new NotImplementedException();
    }
}