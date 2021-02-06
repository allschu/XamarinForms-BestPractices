using System;
using Xamarin.Forms;

namespace BestPractices.Globals
{
    public static class SharedFunctions
    {
        public static Color Determine_Vote_Color(double vote)
        {
            if (vote >= 7)
            {
                return Color.Green;
            }
            if (vote > 5 && vote < 6.9)
            {
                return Color.Orange;
            }

            return Color.Red;
        }


        public static string GetCultureDate(DateTime? date)
        {
            return date.HasValue ? date.Value.ToShortDateString() : string.Empty;
        }
    }
}
