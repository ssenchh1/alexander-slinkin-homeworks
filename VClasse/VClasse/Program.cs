﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Channels;

namespace VClasse
{

    public class Customer
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime RegistrationDate { get; set; }

        public double Balance { get; set; }

        public Customer(long id, string name, DateTime registrationDate, double balance)
        {
            this.Id = id;
            this.Name = name;
            this.RegistrationDate = registrationDate;
            this.Balance = balance;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var customers = new List<Customer>
            {
                new Customer(1, "Tawana Shope", new DateTime(2017, 7, 15), 15.8),
                new Customer(2, "Danny Wemple", new DateTime(2016, 2, 3), 88.54),
                new Customer(3, "Margert Journey", new DateTime(2017, 11, 19), 0.5),
                new Customer(4, "Tyler Gonzalez", new DateTime(2017, 5, 14), 184.65),
                new Customer(5, "Melissa Demaio", new DateTime(2016, 9, 24), 241.33),
                new Customer(6, "Cornelius Clemens", new DateTime(2016, 4, 2), 99.4),
                new Customer(7, "Silvia Stefano", new DateTime(2017, 7, 15), 40),
                new Customer(8, "Margrett Yocum", new DateTime(2017, 12, 7), 62.2),
                new Customer(9, "Clifford Schauer", new DateTime(2017, 6, 29), 89.47),
                new Customer(10, "Norris Ringdahl", new DateTime(2017, 1, 30), 13.22),
                new Customer(11, "Delora Brownfield", new DateTime(2011, 10, 11), 0),
                new Customer(12, "Sparkle Vanzile", new DateTime(2017, 7, 15), 12.76),
                new Customer(13, "Lucina Engh", new DateTime(2017, 3, 8), 19.7),
                new Customer(14, "Myrna Suther", new DateTime(2017, 8, 31), 13.9),
                new Customer(15, "Fidel Querry", new DateTime(2016, 5, 17), 77.88),
                new Customer(16, "Adelle Elfrink", new DateTime(2017, 11, 6), 183.16),
                new Customer(17, "Valentine Liverman", new DateTime(2017, 1, 18), 13.6),
                new Customer(18, "Ivory Castile", new DateTime(2016, 4, 21), 36.8),
                new Customer(19, "Florencio Messenger", new DateTime(2017, 10, 2), 36.8),
                new Customer(20, "Anna Ledesma", new DateTime(2017, 12, 29), 0.8)
            };


            var first = customers.FirstOrDefault(c => c.RegistrationDate == customers.Min(c => c.RegistrationDate));

            var second = customers.Average(c => c.Balance);
            Console.WriteLine(second);

            var res = Third(customers, DateTime.MinValue, DateTime.MaxValue);
            if (!res.Any())
            {
                Console.WriteLine("No results");
            }

            var fourth = customers.OrderBy(c => c.Id);

            Fifth(customers, "Ama");
            
            Six(customers);

            Seven(customers, "Name", "descending");

            Eight(customers);
        }

        public static IEnumerable<Customer> Third(IEnumerable<Customer> list, DateTime min, DateTime max)
        {
            return list.Where(c => c.RegistrationDate <= max && c.RegistrationDate >= min)
                .OrderBy(c => c.RegistrationDate);
        }

        public static IEnumerable<Customer> Fifth(IEnumerable<Customer> list, string str)
        {
            return list.Where(c => c.Name.Contains(str, StringComparison.OrdinalIgnoreCase));
        }

        public static void Six(List<Customer> list)
        {
            var a = list
                .GroupBy(c => c.RegistrationDate.Month)
                .OrderBy(c => c.Key)
                .ThenBy(c => c.Select(c=> c.Name));

            foreach (var group in a)
            {
                foreach (var customer in group)
                {
                    Console.WriteLine(customer.Name + " " + customer.RegistrationDate);
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public static IEnumerable<Customer> Seven(List<Customer> list, string field, string direction)
        {
            var sortedList = list
                .OrderBy(c => c.GetType().GetProperty(field)?.GetValue(c));
            
            return direction == "descending" ? sortedList.Reverse() : sortedList;
        }

        public static void Eight(List<Customer> list)
        {
            list.Select(c => c.Name).ToList().ForEach(Console.WriteLine);
        }
    }
}
