using System;
using System.Text.RegularExpressions;

namespace Homework1
{
    public abstract class Item
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Size { get; set; }
        public int iSize { get; set; }
        public Item(string name, string ext, string size)
        {
            Name = name;
            Extension = ext;
            Size = size;

            var numbersonly = Regex.Replace(size, "[^0-9]", "");

            switch (Size[Size.Length-2])
            {
                case 'M': iSize = 1000000 * int.Parse(numbersonly); 
                    break;
                case 'G': iSize = 1000000000 * int.Parse(numbersonly);
                    break;
                case 'K': iSize = 1000 * int.Parse(numbersonly);
                    break;
                default: iSize = 1 * int.Parse(numbersonly);
                    break;
            }
        }

        public virtual void Show()
        {
            Console.WriteLine($"\t{Name}");
            Console.WriteLine($"\t\tExtension:{Extension}");
            Console.WriteLine($"\t\tSize:{Size}");
        }
    }
}