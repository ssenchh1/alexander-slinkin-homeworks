using System;

namespace Practice17may
{
    public class ExitCommand : Command
    {
        public override void Execute(string argument, ref string currentpath)
        {
            Environment.Exit(0);
        }
    }
}