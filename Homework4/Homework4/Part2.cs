using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework4
{
    static class Part2
    {
        public static void First(List<object> list)
        {
            list.OfType<Film>().SelectMany(f => f.Actors).ToList().ForEach(i => Console.WriteLine(i.Name));
        }

        public static void Second(List<object> list)
        {
            list.OfType<Film>().SelectMany(f => f.Actors.Where(a => a.Birthdate.Month == 8)).ToList().ForEach(i => Console.WriteLine(i.Name));
        }

        public static void Third(List<object> list)
        {
            list.OfType<Film>().SelectMany(f => f.Actors)
                .OrderBy(a => a.Birthdate).Take(2).ToList()
                .ForEach(i => Console.WriteLine(i.Name));
        }

        public static void Fourth(List<object> list)
        {
            list.OfType<Article>().GroupBy(a => a.Author).ToList()
                .ForEach(g => Console.WriteLine(g.Key + ": " + g.Count()));
        }

        public static void Fifth(List<object> list)
        {
            list.OfType<ArtObject>().GroupBy(i => i.Author).ToList()
                .ForEach(g => Console.WriteLine(g.Key + ": " + g.Count()));
        }

        public static void Six(List<object> list)
        {
            int i = 0;

            list.OfType<Film>().SelectMany(f => f.Actors)
                .SelectMany(s => s.Name.Replace(" ", string.Empty).ToLower())
                .Distinct().Select(a => 
                {   
                    i++;
                    return a;
                }).ToList()
                .ForEach(Console.WriteLine);

            Console.WriteLine(i);
        }

        public static void Seven(List<object> list)
        {
            list.OfType<Article>().OrderBy(a => a.Author).ThenBy(a => a.Pages).ToList().
                ForEach(a => Console.WriteLine(a.Name));
        }

        public static void Eight(List<object> list)
        {
            list.OfType<Film>()
                .SelectMany(f => f.Actors,
                    (f, a) => new {Film = f, Actor = a})
                .GroupBy(i => i.Actor).ToList().
                ForEach(g => g.ToList()
                    .ForEach(a => Console.WriteLine(g.Key.Name + ": " + a.Film.Name)));
        }

        public static void Nine(List<object> list)
        {
            var a = list.OfType<Article>()
                .Select(i => i.GetType().GetProperties().Where(p => p.PropertyType == typeof(int))
                    .Select(p => (int)p.GetValue(i)))
                .Select(i => i.ToList()).
                Union(list.OfType<Film>()
                    .Select(i => i.GetType().GetProperties().Where(p => p.PropertyType == typeof(int))
                        .Select(p => (int)p.GetValue(i)))
                    .Select(i => i.ToList()))
                .Union(list.OfType<List<int>>().Select(i => i))
                .Select(a => a.Sum()).Sum();

            Console.WriteLine();
        }

        public static void Ten(List<object> list)
        {
            list
                .OfType<Article>()
                .GroupBy(i => i.Author)
                .ToList()
                .ForEach(g => g.ToList().ForEach(a => Console.WriteLine(g.Key + ": " + a.Name[..20])));
            Console.WriteLine();
        }
    }
}