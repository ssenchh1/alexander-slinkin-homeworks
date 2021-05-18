namespace Practice17may
{
    public abstract class Command
    {
        public string callName;

        public abstract void Execute(string argument, ref string currentpath);
    }
}