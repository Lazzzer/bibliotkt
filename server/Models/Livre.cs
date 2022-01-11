namespace server.Models;

public record Livre(int Issn, string Titre, string? Synopsis, DateOnly DateParution, DateOnly DateAcquisition, int PrixAchat, int PrixEmprunt);