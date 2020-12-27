using System;
using System.Collections.Generic;
using System.Text;

namespace BestPractices.Models
{
    public class MovieList
    {
        public string Title { get; }
        public string Intro { get; }
        public string ImagePath { get; }

        public MovieList(string title, string intro, string imagePath)
        {
            Title = title;
            Intro = intro;
            ImagePath = imagePath;
        }
    }
}
