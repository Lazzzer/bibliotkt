namespace server.Models;

public record Emprunt(int Id, DateOnly DateDebut, DateOnly DateRetourPlanifie, DateOnly DateRendu, EtatUsure EtatUsure, Exemplaire Exemplaire, Membre Membre);