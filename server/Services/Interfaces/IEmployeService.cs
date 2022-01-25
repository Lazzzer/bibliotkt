﻿using server.Models;

namespace server.Services.Interfaces;

public interface IEmployeService
{
    Employe? GetEmployeByLogin(string login);
    Employe? GetEmployeById(int id);
}