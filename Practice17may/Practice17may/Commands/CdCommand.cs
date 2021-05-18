using System;
using System.IO;

namespace Practice17may
{
    public class CdCommand : Command
    {
        public override void Execute(string argument, ref string currentpath)
        {
            try
            {
                if (argument != "..")
                {
                    currentpath = currentpath + argument;

                    Console.WriteLine();
                    Console.WriteLine($"Directories and files in {currentpath}");

                    var directories = Directory.GetDirectories(currentpath);

                    foreach (var dir in directories)
                    {
                        Console.WriteLine(dir);
                    }

                    var files = Directory.GetFiles(currentpath);

                    foreach (var file in files)
                    {
                        Console.WriteLine(file);
                    }

                    currentpath += "\\";

                    Console.WriteLine();
                }
                else
                {
                    var dir = Directory.GetParent(currentpath).Parent;
                    currentpath = dir.FullName;
                    Execute("", ref currentpath);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Не удалось получить доступ к папке. Причина - " + e.Message);
                Console.WriteLine();
                Execute("..", ref currentpath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Что-то пошло не так." + ex.Message);
            }
        }
    }
}