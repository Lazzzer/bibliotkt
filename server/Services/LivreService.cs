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
            command.CommandText = "SELECT * FROM Livre WHERE titre ILIKE @text";
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

    public IList<Livre> GetLivresByFilters(string? nomAuteur, Langue? langue, string[] nomCategories)
    {
        var list = new List<Livre>();
        var joins = "";
        var where = "";
        
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            if (nomAuteur != null)
            {
                // Exemple avec seulement l'auteur
                // SELECT * FROM Livre INNER JOIN Livre_Auteur ON Livre_Auteur.issnlivre = Livre.issn
                // INNER JOIN Auteur ON livre_auteur.idauteur = auteur.id
                // WHERE auteur.nom ILIKE '%Hugo%' OR auteur.prénom ILIKE '%Hugo%';

                joins += " INNER JOIN Livre_Auteur ON Livre_Auteur.issnLivre = Livre.issn INNER JOIN Auteur ON Livre_auteur.idauteur = auteur.id";
                
                where += where == "" ? "WHERE " : " AND ";
                where += "(auteur.nom ILIKE @nomAuteur OR auteur.prénom ILIKE @nomAuteur)";
                command.Parameters.AddWithValue("@nomAuteur", "%" + nomAuteur.Trim() + "%");
            }

            if (langue != null)
            {
                joins += " INNER JOIN edition ON edition.issnlivre = livre.issn";
                
                where += where == "" ? "WHERE " : " AND ";
                where += "edition.langue = @langue";
                command.Parameters.AddWithValue("@langue", langue.Value);
            }

            if (nomCategories.Length > 0)
            {
                joins +=
                    " INNER JOIN livre_catégorie ON livre_catégorie.issnlivre = Livre.issn INNER JOIN catégorie ON livre_catégorie.nomcatégorie = catégorie.nom";
                
                where += where == "" ? "WHERE " : " AND ";
                where += "(";
                for (var i = 0; i < nomCategories.Length; ++i)
                {
                    where += "catégorie.nom = @nom" + i;
                    command.Parameters.AddWithValue("@nom" + i, nomCategories[i].Trim());
                    if (i != nomCategories.Length - 1)
                    {
                        where += " OR ";
                    }
                }

                where += ")";
            }

            var query = "SELECT DISTINCT issn, titre, synopsis, dateparution, dateacquisition, prixachat, prixemprunt FROM Livre " + joins + " " + where;

            command.CommandText = query;

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

    public int Insert(Livre livre)
    {
        int id;
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"INSERT INTO Livre (issn, titre, synopsis, dateParution, dateAcquisition, prixAchat, prixEmprunt)
            VALUES (ISSN = @issn, titre = @titre, synopsis = @synopsis, dateParution = @parution, dateAcquisition = @acquisition, 
                prixAchat = @achat, prixEmprunt = @emprunt) returning  issn";
            
            command.Parameters.AddWithValue("@issn", livre.Issn);
            command.Parameters.AddWithValue("@titre", livre.Titre);
            command.Parameters.AddWithValue("@synopsis", livre.Synopsis);
            command.Parameters.AddWithValue("@parution", livre.DateParution);
            command.Parameters.AddWithValue("@acquisition", livre.DateAcquisition);
            command.Parameters.AddWithValue("@achat", livre.PrixAchat);
            command.Parameters.AddWithValue("@emprunt", livre.PrixEmprunt);
            id = (int)(command.ExecuteScalar() ?? -1);

            foreach (Auteur a in livre.Auteurs)
            {
                InsertLivreAuteur(a.Id, id);
            }
            foreach (Categorie c in livre.Categories)
            {
                InsertLivreCat(c.Nom, id);
            }
        }

        _connection.Close();
        return id;
    }

    private void InsertLivreCat(string nomCat, int issnLivre)
    {
        
    }

    private void InsertLivreAuteur(int idAuteur, int issnLivre)
    {
        
    }

    public void Update(Livre livre)
    {
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"UPDATE Livre 
            SET titre = @titre, synopsis = @synopsis, dateParution = @parution, dateAcquisition = @acquisition, 
                prixAchat = @achat, prixEmprunt = @emprunt WHERE issn = @issn";
            
            command.Parameters.AddWithValue("@issn", livre.Issn);
            command.Parameters.AddWithValue("@titre", livre.Titre);
            command.Parameters.AddWithValue("@synopsis", livre.Synopsis);
            command.Parameters.AddWithValue("@parution", livre.DateParution);
            command.Parameters.AddWithValue("@acquisition", livre.DateAcquisition);
            command.Parameters.AddWithValue("@achat", livre.PrixAchat);
            command.Parameters.AddWithValue("@emprunt", livre.PrixEmprunt);
        }
        _connection.Close();
    }

    public void Delete(int id)
    {
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM Livre WHERE ISSN = @id";
            command.Parameters.AddWithValue("@id", id);
        }
        _connection.Close();
    }
}