using System;

namespace Homework1
{
    class MovieItem : Item
    {
        public string Resolution { get; set; }

        public string Length { get; set; }

        public MovieItem(string name, string ext, string size, string resolution, string length)
            : base(name, ext, size)
        {
            Resolution = resolution;
            Length = length;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"\t\tResolution:{Resolution}");
            Console.WriteLine($"\t\tLength:{Length}");
        }
    }
}