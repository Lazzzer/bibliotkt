namespace server.Models;

public record Emprunt(int Id, DateOnly DateDebut, DateOnly DateRetourPlanifie, DateOnly? DateRendu, string EtatUsure, int idExemplaire, int idMembre);