using System;
using System.Collections.Generic;
using System.Linq;

namespace BullsAndCows
{
    public class Game
    {
        private int bulls;

        private int cows;

        private int iterations;

        private List<int[]> possibleVariants;

        private int[] currentVariant;

        public Game()
        {
            possibleVariants = new List<int[]>();
            Initialize();
        }

        public void Start()
        {
            for (var index = 0; index < possibleVariants.Count; index++)
            {

                while (0 != possibleVariants.Count())
                {
                    var variant = possibleVariants[0];
                    currentVariant = variant;
                    Console.WriteLine(string.Join("", variant));
                    var input = Console.ReadLine();
                    Check(input);

                    for (var j = 1; j < possibleVariants.Count();)
                    {
                        if (!Matches(currentVariant, possibleVariants[j], bulls, cows))
                            possibleVariants.Remove(possibleVariants[j]);
                        else
                            j++;
                    }

                    possibleVariants.Remove(possibleVariants[0]);

                    iterations++;
                }
            }
        }

        private void Check(string input)
        {
            var splitted = input.Split(",");
            bulls = int.Parse(splitted[0]);
            cows = int.Parse(splitted[1]);

            if (bulls == 4)
            {
                Stop();
            }

            if (bulls == 0 && cows == 0)
            {
                foreach (var i in currentVariant)
                {
                    possibleVariants.RemoveAll(x => x.Contains(i));
                }
            }
        }

        private static bool Matches(int[] num1, int[] num2, int bulls, int cows)
        {
            return
                bulls == (
                    Convert.ToInt32(num1[0] == num2[0])
                    + Convert.ToInt32(num1[1] == num2[1])
                    + Convert.ToInt32(num1[2] == num2[2])
                    + Convert.ToInt32(num1[3] == num2[3])
                ) &&
                cows == (
                    Convert.ToInt32(num1[0] == num2[1])
                    + Convert.ToInt32(num1[0] == num2[2])
                    + Convert.ToInt32(num1[0] == num2[3])
                    + Convert.ToInt32(num1[1] == num2[0])
                    + Convert.ToInt32(num1[1] == num2[2])
                    + Convert.ToInt32(num1[1] == num2[3])
                    + Convert.ToInt32(num1[2] == num2[0])
                    + Convert.ToInt32(num1[2] == num2[1])
                    + Convert.ToInt32(num1[2] == num2[3])
                    + Convert.ToInt32(num1[3] == num2[0])
                    + Convert.ToInt32(num1[3] == num2[1])
                    + Convert.ToInt32(num1[3] == num2[2])
                );
        }

        private void Stop()
        {
            Console.WriteLine($"Thats it! Hurray! I guessed numbers in {iterations} iterations");
            AskPlayAgain();
        }

        private void AskPlayAgain()
        {
            Console.WriteLine("One more time? Y/N");
            var answer = Console.ReadKey();
            Console.WriteLine();

            switch (answer.KeyChar.ToString().ToUpper())
            {
                case "N":
                    Environment.Exit(0);
                    break;
                case "Y":
                    Initialize(); Start();
                    break;
                default: AskPlayAgain(); 
                    break;
            }
        }

        private void Initialize()
        {
            bulls = 0;
            cows = 0;
            iterations = 0;

            int[] arr = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var variants = arr.SelectMany(x => arr, (i, i1) => new { i, i1 })
                .SelectMany(x => arr, (x, x1) => new { x.i, x.i1, x1 })
                .SelectMany(x => arr, (c, c1) => new int[] { c.i, c.i1, c.x1, c1 });

            possibleVariants = variants.Where(i => i[0] != 0).Where(i => i.Distinct().Count() == 4).ToList();
        }
    }
}