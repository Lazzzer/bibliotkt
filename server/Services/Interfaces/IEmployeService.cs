/**
 * Mets à disposition les méthodes permettant de faire des requêtes sql à la base concernant la table employé
 */
using server.Models;

namespace server.Services.Interfaces;

public interface IEmployeService
{
    Employe? GetEmployeByLogin(string login);
    Employe? GetEmployeById(int id);
}