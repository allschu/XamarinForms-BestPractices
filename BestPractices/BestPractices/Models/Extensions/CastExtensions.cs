using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BestPractices.Common.Models;

namespace BestPractices.Models.Extensions
{
    internal static class CastExtensions
    {
        public static CastList ToViewModel(this Cast cast)
        {
            return new CastList(cast.Id, cast.Name, cast.Character, cast.ImageUrl, cast.Order);
        }

        public static IEnumerable<CastList> ToViewModel(this IEnumerable<Cast> cast)
        {
            return cast.Select(x => x.ToViewModel()).OrderBy(x => x.Order);
        }
    }
}
