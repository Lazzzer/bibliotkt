namespace server.Models;

public record Livre(int Issn, string Titre, string synopsis, DateOnly DateParution, DateOnly DateAcquisition, int PrixAchat, int PrixEmprunt);