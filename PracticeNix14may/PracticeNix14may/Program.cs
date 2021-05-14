using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;

namespace PracticeNix14may
{
    public class Seven
    {
        public string[] A(string str)
        {
            var rgx = new Regex("\\w{5}\\d{3}");

            var matches = rgx.Matches(str);

            var list = new List<string>();

            foreach (Match match in matches)
            {
                list.Add(match.Value);
            }

            return list.ToArray();
        }

        public bool B(string password)
        {
            var regex = new Regex( "^(?=.*[a - z])(?=.*[A - Z])(?=.*\\d)[a - zA - Z\\d]{6,}$");

            return regex.IsMatch(password);
        }

        public bool C(string pass)
        {
            var regex = new Regex("[\\d]{3}-[\\d]{3}");

            return regex.IsMatch(pass);
        }

        public bool D(string phone)
        {
            var regex = new Regex("[+380]-[\\d]{2}-[\\d]{3}-[\\d]{2}-[\\d]{2}");

            return regex.IsMatch(phone);
        }

        public void E(string text)
        {
            var regex = new Regex("[+380]-[\\d]{2}-[\\d]{3}-[\\d]{2}-[\\d]{2}");

            var matches = regex.Matches(text);

            foreach (Match match in matches)
            {
                text = text.Replace(match.Value, "+XXX-XX-XXX-XX-XX");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var sorting = new QuickSort<int>();

            var array = new int[] {12, 56, 87, 45, 65, 56};

            var sorted = sorting.Sort(array, 0, array.Length-1);

            var sv = new Seven();

            var a = sv.D("+380-98-219-72-70");

            Console.WriteLine(First("a2g4hr7389wdjfk"));
            
            
            Console.WriteLine(Nine(
                "0JXRgdC70Lgg0YLRiyDRh9C40YLQsNC10YjRjCDRjdGC0L7RgiDRgtC10LrRgdGCLCDQt9C90LDRh9C40YIg0LfQsNC00LDQvdC40LUg0LLRi9C/0L7Qu9C90LXQvdC+INCy0LXRgNC90L4gOik="));
        }
            

        public static string First(string str)
        {
            return Regex.Replace(str, "\\D", "");
        }

        public static void Second(double firstDigit, double secondDigit)
        {
            Console.WriteLine($"{(firstDigit / secondDigit):0.00}");
        }

        public static void Third()
        {
            var input = Console.ReadLine();

            var parsed = double.Parse(input, NumberStyles.AllowExponent);

            Console.WriteLine(parsed);
        }

        public static string Fourth(DateTime date)
        {
            return date.ToString("O");
        }

        public static DateTime Fifth(string str)
        {
            var splitted = str.Split(new char[] {'-', ' '});

            return new DateTime(int.Parse(splitted[0]), int.Parse(splitted[1]), int.Parse(splitted[2]));
        }

        public static int Six(string str)
        {
            var splitted = str.Split(",");
            var sum = 0;

            foreach (var item in splitted)
            {
                sum += int.Parse(item);
            }

            return sum;
        }

        public static string[] Eight(string[] names)
        {
            var filalNames = new List<string>();

            foreach (var name in names)
            {
                var separators = Regex.Replace(name, "\\w", "").ToCharArray();
                var splitted = name.Split(separators);

                var finalname = "";

                for (var index = 0; index < splitted.Length; index++)
                {
                    var piece = splitted[index];
                    var chars = piece.ToCharArray();
                    chars[0] = char.ToUpper(chars[0]);

                    finalname += string.Join("",chars);
                    if (index != splitted.Length - 1)
                    {
                        finalname += separators[index];
                    }
                }

                filalNames.Add(finalname);
            }

            return filalNames.ToArray();
        }

        public static string Nine(string str)
        {
            var data = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(data);
        }
    }

    public class QuickSort<T> where T : IComparable
    {
        public void Swap(T[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        
        public int Partition(T[] arr, int low, int high)
        {
            var pivot = arr[high];
            
            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {
                if (arr[j].CompareTo(pivot) < 0)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, high);
            return (i + 1);
        }

        public T[] Sort(T[] arr, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high);
                
                Sort(arr, low, pi - 1);
                Sort(arr, pi + 1, high);
            }

            return arr;
        }
    }
}
