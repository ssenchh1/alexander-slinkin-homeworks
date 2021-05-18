using System;
using System.Collections.Generic;
using System.Text;

namespace CSVFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            var persons = new List<Person>()
            {
                new Person() {Name = "Alex", Surname = "Sen", Age = 20, IsStudent = true},
                new Person() {Name = "Antony", Surname = "Menk", Age = 20, IsStudent = true},
                new Person() {Name = "Mark", Surname = "Aurelius", Age = 25, IsStudent = false},
                new Person() {Name = "Brad", Surname = "Pitt", Age = 50, IsStudent = false},
                new Person() {Name = "Billy", Surname = "Harrington", Age = 45, IsStudent = false}
            };

            string pathToFile = @$"C:\Users\{Environment.UserName}\Desktop\persons.csv";

            var formatter = new Formatter(new CsvFormat());
            formatter.Run(persons, pathToFile);
        }
    }
}
