using System;

namespace EduPortal.Application.ViewModels
{
    public class ArticleViewModel : MaterialViewModel
    {
        public DateTime Date { get; set; }

        public string Text { get; set; }

        public string Source { get; set; }
    }
}
