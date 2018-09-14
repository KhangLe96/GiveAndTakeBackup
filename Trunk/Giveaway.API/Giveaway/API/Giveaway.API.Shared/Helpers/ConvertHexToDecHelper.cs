using System;
using System.Collections.Generic;
using System.Text;

namespace Giveaway.API.Shared.Helpers
{
    public class ConvertHexToDecHelper
    {
        public static int Convert(string hex)
        {
            string color = hex.TrimStart('#');
            string R = color.Substring(0, 2);
            string G = color.Substring(2, 2);
            string B = color.Substring(4, 2);

            int decValue = int.Parse(B + G + R, System.Globalization.NumberStyles.HexNumber);
            return decValue;
        }
    }
}
