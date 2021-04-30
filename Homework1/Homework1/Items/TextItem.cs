using System;

namespace Homework1
{
    class TextItem : Item
    {
        public string Content { get; set; }

        public TextItem(string name, string ext, string size, string content) 
            : base(name, ext, size)
        {
            Content = content;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"\t\tContent:{Content}");
        }
    }
}