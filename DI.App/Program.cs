using DI.App.Services.PL;

namespace DI.App
{
    internal class Program
    {
        private static void Main()
        {
            // Inversion of Control
            var manager = new CommandManager(new CommandProcessor());

            manager.Start();
        }
    }
}
