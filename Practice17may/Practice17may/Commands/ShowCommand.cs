using System;
using System.IO;

namespace Practice17may
{
    public class ShowCommand : Command
    {
        public override void Execute(string argument, ref string currentpath)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine($"Directories and files in {currentpath}");

                var a = Directory.GetDirectories(currentpath);

                foreach (var dir in a)
                {
                    Console.WriteLine(dir);
                }

                var b = Directory.GetFiles(currentpath);

                foreach (var file in b)
                {
                    Console.WriteLine(file);
                }

                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}