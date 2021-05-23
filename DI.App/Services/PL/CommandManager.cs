using System;
using System.Text;
using DI.App.Abstractions;

namespace DI.App.Services.PL
{
    public class CommandManager
    {
        //избавились от инверсии зависимостей, добавив выбор процессора с помощью конструктора. Также исправили нейминг
        private readonly ICommandProcessor _processor/*= new CommandProcessor()*/;
        private string _info;
        
        public CommandManager(ICommandProcessor processor)
        {
            _processor = processor;
        }

        public void Start()
        {
            this.SetupInfo();

            while (true)
            {
                Console.Clear();
                Console.WriteLine(this._info);

                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)
                    ||
                    !int.TryParse(input, out var command))
                {
                    continue;
                }

                this._processor.Process(command);

                Console.WriteLine("RETURN to continue...");
                Console.ReadLine();
            }
        }

        private void SetupInfo()
        {
            var sb = new StringBuilder();
            var commands = this._processor.Commands;

            sb.AppendLine("Select operation:");

            foreach (var command in commands)
            {
                sb.AppendLine($"{command.Number}. {command.DisplayName}");
            }

            this._info = sb.ToString();
        }
    }
}