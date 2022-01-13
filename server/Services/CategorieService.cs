using Microsoft.Extensions.Options;
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
}