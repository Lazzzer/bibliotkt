using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;

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

        var issn = reader.GetInt32(reader.GetOrdinal("issnlivre"));
        var titre = reader.GetString(reader.GetOrdinal("titre"));
        string? synospis = null;
        if (!reader.IsDBNull(reader.GetOrdinal("synopsis")))
        { 
            synospis = reader.GetString(reader.GetOrdinal("synopsis"));
        }
        var dateParution = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("dateParution"));
        var dateAcquisition = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("dateAcquisition"));
        var prixAchat = reader.GetInt32(reader.GetOrdinal("prixAchat"));
        var prixEmprunt = reader.GetInt32(reader.GetOrdinal("prixEmprunt"));
        
        return new Livre(issn, titre, synospis, dateParution, dateAcquisition, prixAchat, prixEmprunt);
    }
}