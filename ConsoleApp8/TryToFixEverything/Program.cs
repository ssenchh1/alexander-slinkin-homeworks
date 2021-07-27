using System;
using System.Linq;
using BLL.Services;
using DAL.Services;

namespace ConsoleApp8
{
    // Task:
    // Program should return "Matching Record, got name=Fred, lastname=Smith, age=40"
    // Могут быть ошибки любого вида:
    // - отсутствие ссылок на проекты, классы, интерфейсы или же ссылки на несуществующие классы, методы, интерфейсы
    // - ошибки в нейминге
    // - ошибки в логике работы программы
    // - Ошибки в версии целевого фреймворка
    // - очепятки
    // - ошибки в DI контейнере
    // - и т.д.
    // Необходимо исправить все проблемы и ошибки системы и сделать так, чтобы программа заработала

    class Program
    {
        static void Main(string[] args)
        {
            var userService = new UserService(new UserRepository());

            var users = userService.LoadRecords();

            users.OrderBy(u => u.LastName).ToList()
                .ForEach(i => Console.WriteLine($"Matching Record, got name={i.FirstName}, lastName={i.LastName}, age={i.Age}"));
            
            Console.ReadKey();
        }
    }
}