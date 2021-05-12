using System;
using System.Collections.Generic;
using System.IO;

namespace CSVFormatter
{
    class Formatter
    {
        private readonly IFormat format;

        private List<string> fields;

        public Formatter(IFormat format)
        {
            this.format = format;
            fields = new List<string>();
        }

        public void Run<T>(IEnumerable<T> collection, string path)
        {
            AskFields();
            var formatted = Format(collection);
            WriteToFile(formatted, path);
        }

        private string Format<T>(IEnumerable<T> list)
        {
            format.GetNeededInfo();
            var content = format.Execute(list, fields);
            return content;
        }

        private void WriteToFile(string content, string path)
        {
            File.WriteAllText(path, content);
        }

        private void AskFields()
        {
            Console.Write("Enter fields using comma: ");
            var input = Console.ReadLine();
            fields.AddRange(input?.Split(","));
        }
    }
}