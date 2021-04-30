using System.Collections.Generic;

namespace Homework1
{
    public interface IParsingModule
    {
        public List<Item> Execute(string str);
    }
}