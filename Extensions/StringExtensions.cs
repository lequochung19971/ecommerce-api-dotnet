using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ecommerce.Extensions
{
    public static class StringExtensions
    {
        public static string toKebabCase(this string text)
        {
            // Replace all non-alphanumeric characters with a dash
            text = Regex.Replace(text, @"[^0-9a-zA-Z]", "-");

            // Replace all subsequent dashes with a single dash
            text = Regex.Replace(text, @"[-]{2,}", "-");

            // Remove any trailing dashes
            text = Regex.Replace(text, @"-+$", string.Empty);

            // Remove any dashes in position zero
            if (text.StartsWith("-")) text = text.Substring(1);

            // Lowercase and return
            return text.ToLower();
        }
    }
}