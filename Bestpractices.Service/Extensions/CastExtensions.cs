using Bestpractices.Service.Contract;
using BestPractices.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bestpractices.Service.Extensions
{
    internal static class CastExtensions
    {
        public static Cast ToModel(this CastDTO cast)
        {
            return new Cast(cast.Id, cast.Name, cast.Character, cast.Profile_Path, cast.Order);
        }

        public static IEnumerable<Cast> ToModel(this CastResult castList)
        {
            return castList.Cast.Select(x => x.ToModel());
        }

    }
}
