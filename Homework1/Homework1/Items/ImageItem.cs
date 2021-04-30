using System;

namespace Homework1
{
    class ImageItem : Item
    {
        public string Resolution { get; set; }

        public ImageItem(string name, string ext, string size, string resolution) 
            : base(name, ext, size)
        {
            Resolution = resolution;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"\t\tResolution:{Resolution}");
        }

        
    }
}