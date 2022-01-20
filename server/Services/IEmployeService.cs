using server.Models;

namespace server.Services;

public interface IEmployeService
{
    Employe? GetEmployeByLogin(string login);
    Employe? GetEmployeById(int id);
}