namespace server.Models;

public record MaisonEdition(int Id, string Nom, string Email, string Rue, int NoRue, int Npa, string Localite, string Pays, List<Edition> Editions);