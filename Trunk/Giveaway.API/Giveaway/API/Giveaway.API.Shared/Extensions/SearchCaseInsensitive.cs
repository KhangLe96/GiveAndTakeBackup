using System;
using System.Collections.Generic;
using System.Text;

namespace Giveaway.API.Shared.Extensions
{
    public static class SearchCaseInsensitive
    {
        public static bool Contains(this string target, string value, StringComparison comparison)
        {
            return target.IndexOf(value, comparison) >= 0;
        }
    }
}
