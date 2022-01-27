namespace server.Models;

/// <summary>
/// Record représentant une maison d'édition
/// </summary>
public record MaisonEdition(int Id, string Nom, string Email, string Rue, int NoRue, int Npa, string Localite,
    string Pays, List<Edition>? Editions = null);