using System;
using System.Collections.Generic;

namespace Homework1
{
    public class MovieParsingModule : IParsingModule
    {
        public List<Item> Execute(string str)
        {
            List<Item> list = new List<Item>();

            foreach (var row in str.Split("\r\n"))
            {
                if (row.Contains(".mkv"))
                {
                    string name = row.Substring(row.IndexOf(":") + 1, (row.LastIndexOf("(") - (row.IndexOf(":") + 1)));
                    string extension = name.Substring(name.LastIndexOf(".") + 1);
                    string size = row.Substring(row.LastIndexOf("(") + 1, row.LastIndexOf(")") - (row.LastIndexOf("(") + 1));
                    string resolution = row.Split(";")[1];
                    string length = row.Split(";")[2];

                    var item = new MovieItem(name, extension, size, resolution, length);
                    item.Category = "Movie";

                    list.Add(item);
                }
            }

            return list;
        }
    }
}