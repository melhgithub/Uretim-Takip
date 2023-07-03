using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Extensions
{
    public static class CultureExtensions
    {
        private static readonly CultureInfo _defaultCulture = new("en-US");

        public static decimal ToDecimal(this string @decimal) => decimal.Parse(@decimal, _defaultCulture);
        public static double ToDouble(this string @double) => double.Parse(@double, _defaultCulture);
    }
}
