using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PF.Common.Currencies
{
    public static class Currencies
    {
        public enum CurrencyCodes
        {
            EUR = 1,
            USD = 2,
            PLN = 3,
            HRK = 4,
            BGN = 5
        }

        private static Dictionary<string, string> CurrenciesWithUnits = null;
        public static string[] CurrenciesList;
       
        public static void InitCurrencies()
        {
            CurrenciesWithUnits = new Dictionary<string, string>();
            CurrenciesWithUnits.Add("PLN", "zł");
            CurrenciesWithUnits.Add("EUR", "€");
            CurrenciesWithUnits.Add("USD", "$");

            CurrenciesList = CurrenciesWithUnits.Select(p => p.Key).ToArray();
        }


        public static string GetUnit(string currencyCode)
        {
            string unit = null;

            if (!CurrenciesWithUnits.TryGetValue(currencyCode.Trim(), out unit))
            {
                return null;
                //TODO loguj to
            }

            return unit;
        }
    }
}
