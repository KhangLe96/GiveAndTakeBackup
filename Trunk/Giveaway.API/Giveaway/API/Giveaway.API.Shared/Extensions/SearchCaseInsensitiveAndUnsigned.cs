using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Giveaway.API.Shared.Extensions
{
    public static class SearchCaseInsensitiveAndUnsigned
    {
        public static bool Contains(this string target, string value, StringComparison comparison)
        {
            return ConvertToUnSign(target).IndexOf(ConvertToUnSign(value), comparison) >= 0;
        }

        public static string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }
    }
}
