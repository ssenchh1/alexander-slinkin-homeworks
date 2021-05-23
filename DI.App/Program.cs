using System.Collections.Generic;
using DI.App.Services;
using DI.App.Services.PL;
using DI.App.Services.PL.Commands;

namespace DI.App
{
    internal class Program
    {
        private static void Main()
        {
            // Inversion of Control
            var db = new InMemoryDatabaseService();
            var store = new UserStore(db);
            var processor = new CommandProcessor(store);
            
            processor.AddCommand(new AddUserCommand(store));
            processor.AddCommand(new ListUsersCommand(store));
            processor.AddCommand(new ListUsersCommand(store));

            var manager = new CommandManager(processor);

            manager.Start();
        }
    }
}
