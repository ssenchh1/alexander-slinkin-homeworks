using System;
using DI.App.Abstractions;
using DI.App.Abstractions.BLL;
using DI.App.Models;

namespace DI.App.Services.PL.Commands
{
    public class AddUserCommand : ICommand
    {
        //Теперь в конструктор добавляется хранилище, с которым будет работать данная команда
        private readonly IUserStore userStore /*= new UserStore(new InMemoryDatabaseService())*/;

        public AddUserCommand(IUserStore strorage)
        {
            userStore = strorage;
        }

        public int Number { get; } = 1;
        public string DisplayName { get; } = "Add user";

        public void Execute()
        {
            var rnd = new Random();
            var id = rnd.Next(1, 101);

            this.userStore.AddUser(new User
            {
                Id = id,
                Name = $"User {id}"
            });
        }
    }
}