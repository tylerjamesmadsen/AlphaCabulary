using System.Linq;

namespace AlphaCabulary.ApplicationCore.Catalog.Extensions
{
    public static class StringExtensions
    {
        public static string Sanitize(this string source)
        {
            return new string(source.Where(char.IsLetter).ToArray()).Trim().ToUpper();
        }
    }
}