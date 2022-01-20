using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Utils;

namespace server.Services;

public class EmployeService : IEmployeService
{
    private static NpgsqlConnection _connection = new();

    public EmployeService(IOptions<DbConnection> options)
    {
        _connection =
            new NpgsqlConnection(options.Value.ConnectionString);
    }
    
    public static Employe PopulateEmployeRecord(NpgsqlDataReader reader, string key = "nom")
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var id = reader.GetInt32(reader.GetOrdinal(key));
        var nom = reader.GetString(reader.GetOrdinal("nom"));
        var prénom = reader.GetString(reader.GetOrdinal("prénom"));
        var login = reader.GetString(reader.GetOrdinal("login"));
        var mdp = reader.GetString(reader.GetOrdinal("password"));
        var rue = reader.GetString(reader.GetOrdinal("rue"));
        var noRue = reader.GetInt32(reader.GetOrdinal("noRue"));
        var npa = reader.GetInt32(reader.GetOrdinal("npa"));
        var localité = reader.GetString(reader.GetOrdinal("localité"));
        var dateCreation = reader.GetDateTime(reader.GetOrdinal("dateCreationCompte"));

        return new Employe(id,nom, prénom, rue, noRue, npa, localité, new DateOnly(dateCreation.Year, dateCreation.Month, dateCreation.Day), login, mdp);
    }
    
    public Employe? GetEmployeByLogin(string login)
    {
        Employe? employe = null;
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Edition WHERE login = @login";
            command.Parameters.AddWithValue("login", login);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    employe = PopulateEmployeRecord(reader);
                }
            }
        }
        
        _connection.Close();
        return employe;
    }

    public Employe? GetEmployeById(int id)
    {
        Employe? employe = null;
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Edition WHERE id = @id";
            command.Parameters.AddWithValue("id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    employe = PopulateEmployeRecord(reader);
                }
            }
        }
        
        _connection.Close();
        return employe;
    }
}