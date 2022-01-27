using NpgsqlTypes;

namespace server.Models;

/// <summary>
/// Enum mappant le type énuméré "TypeEdition" dans la base de données
/// </summary>
public enum TypeEdition
{
    [PgName("Article")] Article,
    [PgName("Poche")] Poche,
    [PgName("Standard")] Standard,
    [PgName("Résumé")] Resume
}

/// <summary>
/// Enum mappant le type énuméré "Langue" dans la base de données
/// </summary>
public enum Langue
{
    [PgName("Espagnol")] Espagnol,
    [PgName("Italien")] Italien,
    [PgName("Allemand")] Allemand,
    [PgName("Anglais")] Anglais,
    [PgName("Français")] Francais
}

/// <summary>
/// Record représentant une édition
/// </summary>
public record Edition(int Id, int issn, int idMaison, TypeEdition Type, Langue Langue,
    List<Exemplaire>? Exemplaires = null);