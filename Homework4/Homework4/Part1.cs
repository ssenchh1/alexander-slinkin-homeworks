using System;
using System.Linq;

namespace Homework4
{
    static class Part1
    {
        public static void First()
        {
            Enumerable.Range(10, 40).ToList()
                .ForEach(arg => Console.Write(arg + ", "));
        }

        public static void Second()
        {
            Enumerable.Range(10, 40)
                .Where(i => i % 3 == 0).ToList()
                .ForEach(i => Console.Write(i + ", "));
        }

        public static void Third()
        {
            Enumerable.Repeat("LINQ", 10).ToList()
                .ForEach(Console.WriteLine);
        }

        public static void Fourth(string str)
        {
            str.Split(";")
                .Where(s => s.Contains("a")).ToList()
                .ForEach(Console.WriteLine);
        }

        public static void Fifth(string str)
        {
            str.Split(";").ToList()
                .ForEach(a => Console.Write(a.Count(c => c == 'a') + ", "));
        }

        public static void Six(string str)
        {
            str.Split(";").ToList()
                .ForEach(s => Console.Write(s.Contains("abb") + ", "));
        }

        public static void Seven(string str)
        {
            str.Split(";")
                .Select(s => s.Trim())
                .Where(s => s.Length == str.Split(";").Select(s => s.Trim())
                    .Max(c => c.Length)).ToList()
                .ForEach(Console.WriteLine);
        }

        public static double Eight(string str)
        {
            return str.Split(";")
                .Select(s => s.Trim())
                .Average(s => s.Length);
        }

        public static string Nine(string str)
        {
            return string.Join("", str
                .Split(";")
                .FirstOrDefault(s => s.Trim().Length == str.Split(";").Min(s => s.Trim().Length))
                .Reverse());
        }

        public static bool Ten(string str)
        {
            return str.Split(";")
                .Select(s => s.Trim())
                .FirstOrDefault(s => s.StartsWith("aa"))
                .Replace("aa", "bb")
                .All(s => s == 'b');
        }

        public static string Eleven(string str)
        {
            return str.Split(";")
                .Select(s => s.Trim())
                .Where(s => s.TakeLast(2).All(c => c == 'b'))
                .Skip(2)
                .DefaultIfEmpty("there are no matching words")
                .Last();
        }
    }
}