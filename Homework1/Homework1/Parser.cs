using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework1
{
    public class Parser
    {
        private List<IParsingModule> modules;
        private List<Item> items;

        public Parser()
        {
            modules = new List<IParsingModule>();
            items = new List<Item>();
        }

        public void AddModule(IParsingModule module)
        {
            modules.Add(module);
        }

        public void Execute(string str)
        {
            foreach (var module in modules.Distinct())
            {
                items.AddRange(module.Execute(str));
            }

            ShowItems();
        }

        private void ShowItems()
        {
            var groupItems = items.GroupBy(i => i.Category).Select(g => new
            {
                Name = g.Key,
                Items = g.Select(i => i).OrderBy(i => i.iSize)
            });

            foreach (var group in groupItems)
            {
                Console.WriteLine(group.Name);
                foreach (var item in group.Items)
                {
                    item.Show();
                }
            }
        }
    }
}