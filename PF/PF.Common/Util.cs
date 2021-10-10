using System;
using System.Collections.Generic;
using System.Text;

namespace PF.Common
{
    public static class Util
    {
        public static string GetInitials(string name, string lastName)
        {
            string initials = "";

            if (!string.IsNullOrEmpty(name))
            {
                initials += name[0];
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                initials += lastName[0];
            }

            return initials;
        }
    }
}
