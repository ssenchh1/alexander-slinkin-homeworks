using System;
using System.Collections.Generic;
using System.Linq;

namespace Practice31may
{
    public class StringCalculator
    {
        public int called = 0;
        public event Action AddOccured;

        public int Add(string numbers)
        {
            AddOccured?.Invoke();

            var nums = new List<string>();

            if (numbers.StartsWith("//"))
            {
                var delimetersList = new List<string>();

                var delimiter = numbers.Split("\n", 2)[0].Replace("//", "");

                if (delimiter.Contains("["))
                {
                    var delimiters = delimiter.Split("][")
                        .Select(s => s.Replace("[","").Replace("]", ""));

                    delimetersList.AddRange(delimiters);
                }
                else
                {
                    delimetersList.Add(delimiter);
                }

                numbers = numbers.Split("\n", 2)[1];

                CountWithDelimiters(delimetersList.ToArray());
            }
            else
            {
                CountWithDelimiters(",", "\n");
            }

            //splits input string into array with delimiters
            void CountWithDelimiters(params string[] delimiters)
            {
                if (delimiters.Any(s => numbers.Contains(s)))
                {
                    var tempstrings = new List<string>();

                    tempstrings.Add(numbers);

                    foreach (var delimiter in delimiters)
                    {
                        for (int i = 0; i < tempstrings.Count; i++)
                        {
                            var oldValue = tempstrings[i];
                            var newCollection = tempstrings[i].Split(delimiter).Select(s => s.Trim());
                            tempstrings.InsertRange(i, newCollection);
                            tempstrings.Remove(oldValue);
                        }
                    }

                    nums.AddRange(tempstrings);
                }
                else
                {
                    nums.Add(numbers);
                }

                var negatives = new List<string>();
                for (int i = 0; i < nums.Count; i++)
                {
                    if (nums[i] == "")
                    {
                        nums[i] = "0";
                    }

                    if (nums[i].StartsWith("-"))
                    {
                        negatives.Add(nums[i]);
                    }

                    if (nums[i].Length > 3)
                    {
                        nums[i] = "0";
                    }
                }

                if (negatives.Count != 0)
                {
                    throw new ArgumentException($"Negatives not allowed: {string.Join(", " , negatives)}");
                }
            }

            return nums.Select(int.Parse).Sum();
        }

        public int GetCalledCount()
        {
            return called;
        }

        public void AddCalled()
        {
            called++;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var calc = new StringCalculator();

            calc.AddOccured += calc.AddCalled;
            calc.Add("//[;][***]\n1;2***3");
            calc.GetCalledCount();
        }
    }
}
