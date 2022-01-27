using server.Models;

namespace server.Services.Interfaces;

/// <summary>
/// Interface des méthodes du service pour les employés
/// </summary>
public interface IEmployeService
{
    Employe? GetEmployeByLogin(string login);
}