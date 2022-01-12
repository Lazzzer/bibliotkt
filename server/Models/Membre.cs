﻿namespace server.Models;

public record Membre(int Id, string Nom, string Prenom, string Rue, int NoRue, int Npa, int Localite, DateOnly DateCreationCompte, bool DroitEmprunt, List<Emprunt> Emprunts) 
    : Personne(Id, Nom, Prenom, Rue, NoRue, Npa, Localite, DateCreationCompte);