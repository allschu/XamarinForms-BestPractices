using System;
using System.Collections.Generic;
using System.Text;

namespace BestPractices.Common.Models
{
    public class Cast
    {
        public int Id { get; }
        public string Name { get; }
        public string Character { get; }
        public string ImageUrl { get; }
        public int Order { get; }

        public Cast(int id, string name, string character, string imageUrl, int order)
        {
            Id = id;
            Name = name;
            Character = character;
            ImageUrl = imageUrl;
            Order = order;

        }
    }
}

