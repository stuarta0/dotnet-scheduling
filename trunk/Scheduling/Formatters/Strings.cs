using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public static class Strings
    {
        public static string Plural(int number, string singular, string plural)
        {
            if (number == 1)
                return singular;

            return plural;
        }

        public static string Join(string seperator, string lastSeperator, IEnumerable<string> values)
        {
            int count = 0;
            string cur = null;
            StringBuilder sb = new StringBuilder();
            foreach (string val in values)
            {
                if (count == 1)
                    sb.Append(cur);
                else if (count > 1)
                    sb.Append(String.Concat(seperator, cur));

                cur = val;
                count++;
            }

            if (count > 1)
                sb.Append(lastSeperator);
            sb.Append(cur);

            return sb.ToString();
        }
    }
}
