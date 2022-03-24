using System.Globalization;

namespace Example.Cloudon.API.Helpers.Extentions
{
    public static class DecimalsExtentions
    {

        public static decimal? ConvertToDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;

            // Because of OS Culture in some cases decimal.TryParse works with 0,0 and in some cases with 0.0 types
            decimal.TryParse(value, out var coord);

            // check if coordString has comma or dot
            var hasDotOrComma = value.Contains(",") || value.Contains(".");

            // if it has not comma or dot then we can return it without comma or dot
            if (!hasDotOrComma) return coord;

            // else we have to validate that we return it with dot
            if (coord.ToString(CultureInfo.InvariantCulture).Contains(".")) return coord;

            // replacing . with , and TryParse again
            value = value.Replace(".", ",");
            decimal.TryParse(value, out coord);
            if (coord.ToString(CultureInfo.InvariantCulture).Contains(".")) return coord;

            // replacing , with . and TryParse again
            value = value.Replace(",", ".");
            decimal.TryParse(value, out coord);
            if (coord.ToString(CultureInfo.InvariantCulture).Contains(".")) return coord;

            return null;
        }
    }
}