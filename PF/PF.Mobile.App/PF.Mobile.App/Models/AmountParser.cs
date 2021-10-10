using System;
using System.Collections.Generic;
using System.Text;

namespace PF.Mobile.App.Models
{
    public static class AmountParser
    {
        public static string ParseToText(string input)
        {
            if (input == null)
            {
                return "";
            }

            string output = "";
            bool dotted = false;
            int digitsAfterDot = 0;

            for (int i = 0; i< input.Length; i++)
            {
                char c = input[i];

                if (digitsAfterDot >= 2)
                {
                    break;
                }

                if (char.IsNumber(c))
                {
                    output += c;

                    if (dotted)
                    {
                        digitsAfterDot++;
                    }
                }
                else if (!dotted && (c == ',' || c == '.'))
                {
                    output += ',';
                    dotted = true;
                }
            }

            if (output.Contains(','))
            {
                int zerosToAdd = 2 - output.Split(',')[1].Length;

                for (int i = 0; i < zerosToAdd; i++)
                {
                    output += '0';
                }
            }

            return output;
        }
    }
}
