using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorCore.Extensions
{
    public static class NumberExtensions
    {
        public static bool IsGreaterOrEqualThan(this int value, int comparationValue)
        {
            return value >= comparationValue;
        }
        public static bool IsGreaterThan(this int value, int comparationValue)
        {
            return value > comparationValue;
        }
        public static bool IsLowerOrEqualThan(this int value, int comparationValue)
        {
            return value <= comparationValue;
        }
        public static bool IsLowerThan(this int value, int comparationValue)
        {
            return value < comparationValue;
        }
        public static bool IsInBetween(this int value, int comparationValue1, int comparationValue2)
        {
            if (comparationValue1 > comparationValue2)
                return value >= comparationValue1 && value <= comparationValue2;
            else
                return value >= comparationValue2 && value <= comparationValue1;
        }

    }
}
