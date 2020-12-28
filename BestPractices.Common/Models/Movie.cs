using System;
using System.Collections.Generic;
using System.Text;

namespace BestPractices.Common.Models
{
    public class Movie
    {
        public string Tagline { get; }
        public string Title { get; }
        public string Image_path { get; }

        public Movie(string title, string tagline, string image)
        {
            Tagline = tagline;
            Title = title;
            Image_path = image;
        }
    }
}
