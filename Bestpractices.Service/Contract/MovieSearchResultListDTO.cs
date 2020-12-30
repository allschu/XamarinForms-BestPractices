using System;
using System.Collections.Generic;
using System.Text;

namespace Bestpractices.Service.Contract
{
    public class MovieSearchResultListDTO
    {
        public int Page { get; set; }
        public int Total_Results { get; set; }
        public int Total_Pages { get; }
        public ICollection<MovieSearchResultDTO> results { get; set; }
    }
}
