using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CSVFormatter
{
    class Formatter
    {
        private char separator;

        private List<string> fields;

        public Formatter()
        {
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
            GetNeededInfo();
            var content = ExecuteFormatting(list, fields);
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

        private string ExecuteFormatting<T>(IEnumerable<T> list, List<string> fields)
        {
            var properties = GetRequiredProperties<T>(typeof(T), fields);

            //writing a new line for each element of collection
            var sb = new StringBuilder();

            foreach (var prop in properties)
            {
                sb.Append(prop.Name + separator);
            }

            sb.Append("\r\n");

            foreach (var person in list)
            {
                var str = string.Empty;

                foreach (var field in properties)
                {
                    str += field.GetValue(person);
                    str += separator;
                }

                sb.AppendLine(str);
            }

            return sb.ToString();
        }

        private IEnumerable<PropertyInfo> GetRequiredProperties<T>(Type type, List<string> fields)
        {
            var properties = new List<PropertyInfo>();

            foreach (var propertyInfo in type.GetProperties())
            {
                foreach (var field in fields)
                {
                    if (string.Equals(field.ToLower().Trim(), propertyInfo.Name.ToLower()))
                    {
                        properties.Add(propertyInfo);
                    }
                }
            }

            return properties;
        }

        private void GetNeededInfo()
        {
            Console.Write("Enter separator: ");
            separator = Console.ReadKey().KeyChar;
        }
    }
}