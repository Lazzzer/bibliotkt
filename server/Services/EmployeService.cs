using Microsoft.Extensions.Options;
using Npgsql;
using server.Models;
using server.Services.Interfaces;
using server.Utils;

namespace server.Services;

/// <summary>
/// Implémentation d'un service récupérant des données sur les éditions.
/// La connexion à la base de donnée est ouverte et fermée à chaque requête
/// </summary>
public class EmployeService : IEmployeService
{
    private static NpgsqlConnection _connection = new();

    /// <summary>
    /// Constructeur du service
    /// La connection string de la base de données est transmise par injection de dépendances
    /// </summary>
    public EmployeService(IOptions<DbConnection> options)
    {
        _connection = new NpgsqlConnection(options.Value.ConnectionString);
    }
    
    /// <summary>
    /// Crée un record Employe champs par champs depuis un reader obtenu d'une commande à la base de données
    /// </summary>
    public static Employe PopulateEmployeRecord(NpgsqlDataReader reader, string key = "id")
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
        var localite = reader.GetString(reader.GetOrdinal("localité"));
        var dateCreation = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("dateCreationCompte"));

        return new Employe(id,nom, prénom, rue, noRue, npa, localite, dateCreation, login, mdp);
    }
    
    public Employe? GetEmployeByLogin(string login)
    {
        Employe? employe = null;
        _connection.Open();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Employé INNER JOIN Personne ON Employé.idPersonne = Personne.id WHERE login = @login";
            command.Parameters.AddWithValue("@login", login);

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