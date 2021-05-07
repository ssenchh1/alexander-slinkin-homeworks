using System.Collections.Generic;
using System.Linq;
using DI.App.Abstractions;
using DI.App.Abstractions.BLL;
using DI.App.Services.PL.Commands;

namespace DI.App.Services.PL
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly Dictionary<int, ICommand> commands = new Dictionary<int, ICommand>();
        private IUserStore store;

        public CommandProcessor(IUserStore userStore)
        {
            //инициализируем хранилище, с которым будут работать команды.
            store = userStore;
        }

        public void AddCommand(ICommand command)
        {
            commands.TryAdd(command.Number, command);
        }

        public void Process(int number)
        {
            if (!this.commands.Distinct().ToDictionary(d=>d.Key).TryGetValue(number, out var command)) return;

            command.Value.Execute();
        }

        public IEnumerable<ICommand> Commands => this.commands.Values.AsEnumerable();
    }
}