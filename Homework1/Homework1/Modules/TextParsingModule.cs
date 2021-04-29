using System;
using System.Collections.Generic;

namespace Homework1
{
    public class TextParsingModule : IParsingModule
    {
        public List<Item> Execute(string str)
        {
            List<Item> list = new List<Item>();

            foreach (var row in str.Split("\r\n"))
            {
                if (row.Contains(".txt"))
                {
                    string name = row.Substring(row.IndexOf(":")+1, (row.LastIndexOf("(")-(row.IndexOf(":")+1)));
                    string extension = name.Substring(name.LastIndexOf(".") + 1);
                    string size = row.Substring(row.LastIndexOf("(")+1, row.LastIndexOf(")") - (row.LastIndexOf("(")+1));
                    string content = row.Split(";")[1];

                    TextItem item = new TextItem(name, extension, size, content);
                    item.Category = "Text";

                    list.Add(item);
                }
            }

            return list;
        }
    }
}