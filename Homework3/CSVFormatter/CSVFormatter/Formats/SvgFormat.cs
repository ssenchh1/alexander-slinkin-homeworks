using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSVFormatter
{
    class SvgFormat : IFormat
    {
        private char separator;

        public void GetNeededInfo()
        {
            Console.Write("Enter separator: ");
            separator = Console.ReadKey().KeyChar;
        }

        public string Execute<T>(IEnumerable<T> list, List<string> fields)
        {
            //Adding fields that we will write
            var properties = GetRequiredProperties(list.FirstOrDefault(), fields);

            //writing a new line for each element of collection
            var sb = new StringBuilder();

            foreach (var prop in properties)
            {
                sb.Append(prop.Name + separator);
            }

            sb.Append("\r\n");

            foreach (var person in list)
            {
                var str = "";
                foreach (var field in properties)
                {
                    str += field.GetValue(person);
                    str += separator;
                }
                
                sb.AppendLine(str);
            }

            return sb.ToString();
        }

        public IEnumerable<PropertyInfo> GetRequiredProperties<T>(T instance, List<string> fields)
        {
            var properties = new List<PropertyInfo>();

            foreach (var propertyInfo in instance.GetType().GetProperties())
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
    }
}