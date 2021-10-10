using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.Common.Extensions
{
    public static class Extensions
    {
        public static List<long> ToLongList(this string value, bool skipInvalid = false, char separator = ',')
        {
            List<long> numberIds = new List<long>();

            if (value == null)
            {
                return numberIds;
            }

            string[] strIds = value.Split(separator);

            foreach (string id in strIds)
            {
                long res;
                if (long.TryParse(id, out res))
                {
                    numberIds.Add(res);
                }
                else
                {
                    if (!skipInvalid)
                    {
                        throw new ApplicationException($"Cannot parse {id} into long number (Extensions > ToLongList)");
                    }
                }
            }

            return numberIds;
        }
    }
}
