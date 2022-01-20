﻿using server.Models;

namespace server.Services;

public interface IMembreService
{
    IList<Membre> GetMembres();
    Membre? GetMembreById(int id);
    Membre? GetMembreByIdWithEmprunt(int id);
    int Insert(Membre auteur);
    int Update(Membre auteur);
    int Delete(int id);
}