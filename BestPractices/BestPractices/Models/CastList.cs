using System;
using System.Collections.Generic;
using System.Text;

namespace BestPractices.Models
{
    public class CastList
    {
        public int Id { get; }
        public string Name { get; }
        public string Character { get; }
        public string ImageUrl { get; }
        public int Order { get; }

        public CastList(int id, string name, string character, string imageUrl, int order)
        {
            Id = id;
            Name = name;
            Character = character;
            ImageUrl = imageUrl;
            Order = order;
        }
    }
}
