using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseCore.Extensions
{
    public static class CommonExtensions
    {
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        public static bool IsNotNull(this object value)
        {
            return value != null;
        }

        public static List<string> RetrieveProperties(this object value)
        {
            var list = new List<string>();

            PropertyInfo[] properties = value.GetType().GetProperties();
            foreach(PropertyInfo property in properties)
            {
                list.Add(property.Name);
            }

            return list;
        }

        public static T UpdateFromObject<T>(this T value, T source)
        {
            if (value.IsNull() || source.IsNull()) return value;

            PropertyInfo[] properties = value.GetType().GetProperties();
            foreach(PropertyInfo property in properties)
            {
                if (property.SetMethod.IsNotNull())
                {
                    property.SetValue(value, property.GetValue(source));
                }
            }

            return value;

        } 
    }
}
