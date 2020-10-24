using System;

namespace Hasso.Tests
{
    public static class StringExtensions
    {
        public static string AsOneLiner(this string that) => that.Replace(Environment.NewLine, "").Replace(" ", "").Trim();
    }
}
