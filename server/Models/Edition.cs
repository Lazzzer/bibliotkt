using NpgsqlTypes;

namespace server.Models;

public enum TypeEdition
{
    [PgName("Article")]
    Article,
    [PgName("Poche")]
    Poche,
    [PgName("Standard")]
    Standard,
    [PgName("Résumé")]
    Resume
}

public enum Langue
{
    [PgName("Espagnol")]
    Espagnol,
    [PgName("Italien")]
    Italien,
    [PgName("Allemand")]
    Allemand,
    [PgName("Anglais")]
    Anglais,
    [PgName("Français")]
    Francais
}

public record Edition(int Id, int issn, int idMaison, TypeEdition Type, Langue Langue, List<Exemplaire> Exemplaires);