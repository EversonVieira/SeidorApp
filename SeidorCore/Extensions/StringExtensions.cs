using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorCore.Extensions
{
    public static class StringExtensions
    {
        public static string TargetAsParameter(this string value)
        {
            while(value.Contains('.'))
            {
                value = value.Replace(".", "");
            }

            return value;
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static List<long> RetrieveIdsFromString(this string value, string separator = ",")
        {
            List<long> ids = new List<long>();
            if (value.IsNullOrEmpty()) return ids;

            foreach (string s in value.Split(separator))
            {
                ids.Add(Convert.ToInt64(s));
            }

            return ids;
        }
    }
}
