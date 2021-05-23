using System;
using DI.App.Abstractions;
using DI.App.Abstractions.BLL;

namespace DI.App.Services.PL.Commands
{
    public class ListUsersCommand : ICommand
    {
        //Теперь в конструктор добавляется хранилище, с которым будет работать данная команда
        private readonly IUserStore userStore /*= new UserStore(new InMemoryDatabaseService())*/;

        public ListUsersCommand(IUserStore storage)
        {
            userStore = storage;
        }

        public int Number { get; } = 2;

        public string DisplayName { get; } = "List users";

        public void Execute()
        {
            var users = this.userStore.Users;

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id}. {user.Name}");
            }
        }
    }
}