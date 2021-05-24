using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ThreadingTaskHomework
{
    class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            var processors = Environment.ProcessorCount;

            var array = FirstTask(processors);

            var average = FourthTask(array, processors);

            Console.ReadKey();
        }

        private static int[] FirstTask(int processors)
        {
            var array = new int[4000];
            var threads = new Thread[processors];

            for (int i = 0; i < processors; i++)
            {
                var startIndex = i * (array.Length / processors);

                var endIndex = startIndex + (array.Length / processors);

                threads[i] = new Thread((() => InitializeList(array, startIndex, endIndex)));
                threads[i].Start();
                threads[i].Join();
            }

            return array;
        }

        private static int[] SecondTask(int[] array, int start, int end, int processors)
        {
            var length = end - start;

            var newarray = new int[length];

            var threads = new Thread[processors];

            var pieces = new List<int[]>();

            for (int i = 0; i < processors; i++)
            {
                var startIndex = start + i * (length / processors);

                var endIndex = startIndex + (length / processors);

                var piece = new int[endIndex - startIndex];
                threads[i] = new Thread((() => CopyArray(array, startIndex, endIndex, out piece)));
                threads[i].Start();
                threads[i].Join();
                pieces.Add(piece);
            }

            newarray = pieces.SelectMany(p => p).ToArray();

            return newarray;
        }

        private static int ThirdTask(int[] array, int processors)
        {
            var threads = new Thread[processors];

            var pieces = DivideArray(array, processors);

            var minimals = new int[pieces.Count];

            for (int i = 0; i < processors; i++)
            {
                threads[i] = new Thread((() => FindMinimal(pieces[i], out minimals[i])));
                threads[i].Start();
                threads[i].Join();
            }

            return minimals.Min();
        }

        private static double FourthTask(int[] array, int processors)
        {
            var threads = new Thread[processors];

            var pieces = DivideArray(array, processors);

            var averages = new double[pieces.Count];

            for (int i = 0; i < processors; i++)
            {
                threads[i] = new Thread((() => FindAverage(pieces[i], out averages[i])));
                threads[i].Start();
                threads[i].Join();
            }

            return averages.Average();
        }

        private static void FindAverage(int[] array, out double average)
        {
            average = array.Average();
        }

        private static List<int[]> DivideArray(int[] array, int processors)
        {
            var pieces = new List<int[]>();

            for (int i = 0; i < processors; i++)
            {
                if (i != processors - 1)
                {
                    var toSkip = i * (array.Length / processors);

                    pieces.Add(array.Skip(toSkip).Take(array.Length / processors).ToArray());
                }
                else
                {
                    var toSkip = (array.Length / processors) * (processors - 1);

                    pieces.Add(array.Skip(toSkip).Take(array.Length - toSkip).ToArray());
                }
            }

            return pieces;
        }

        private static void FindMinimal(int[] array, out int minimum)
        {
            minimum = array.Min();
        }

        private static void CopyArray(int[] oldcollection, int start, int end, out int[] newcollection)
        {
            var length = end - start;

            newcollection = new int[length];

            for (int i = 0; i < length; i++)
            {
                newcollection[i] = oldcollection[start + i];
            }
        }

        private static void InitializeList(int[] collection, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                collection[i] = rnd.Next(100);
            }
        }
    }
}
