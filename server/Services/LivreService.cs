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

    public static Livre PopulateLivreRecord(NpgsqlDataReader reader)
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var issn = reader.GetInt32(reader.GetOrdinal("issn"));
        var titre = reader.GetString(reader.GetOrdinal("titre"));
        var synospis = reader.GetString(reader.GetOrdinal("synopsis"));
        var dateParution = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("dateParution"));
        var dateAcquisition = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("dateAcquisition"));
        var prixAchat = reader.GetInt32(reader.GetOrdinal("prixAchat"));
        var prixEmprunt = reader.GetInt32(reader.GetOrdinal("prixEmprunt"));
        
        return new Livre(issn, titre, synospis, dateParution, dateAcquisition, prixAchat, prixEmprunt, new List<Auteur>(), new List<Categorie>());
    }
}