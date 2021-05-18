using System;
using System.Collections.Generic;

namespace Practice17may
{
    public class FileProvider
    {
        private readonly Dictionary<string, Command> commands;

        private string currentPath = @"C:\";

        public FileProvider()
        {
            commands = new Dictionary<string, Command>();
            commands.Add("cd", new CdCommand());
            commands.Add("dir", new ShowCommand());
            commands.Add("open", new OpenCommand());
            commands.Add("exit", new ExitCommand());
        }

        public void Run()
        {
            commands["dir"].Execute(currentPath, ref currentPath);

            while (true)
            {
                ReceiveCommand();
            }
        }

        //Получаем ввод пользователя, отделяем команду ии аргумент и выполняем
        private void ReceiveCommand()
        {
            Console.Write(currentPath + ">");

            var input = Console.ReadLine();

            var splitted = input?.Split(" ", 2, StringSplitOptions.None);

            if (splitted.Length != 1)
            {
                var command = splitted?[0];
                var argument = splitted?[1];

                if (commands.ContainsKey(command))
                {
                    commands[command].Execute(argument, ref currentPath);
                }
                else
                {
                    Console.WriteLine($@"There is no method {command}");
                    Console.WriteLine();
                }
            }
            else
            {
                var command = splitted[0];

                if (commands.ContainsKey(command))
                {
                    commands[command].Execute("", ref currentPath);
                }
                else
                {
                    Console.WriteLine($@"There is no method {command}");
                    Console.WriteLine();
                }
            }
        }
    }
}