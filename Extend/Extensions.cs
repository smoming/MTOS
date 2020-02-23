using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MTOS
{
    static public class Extensions
    {
        static public bool Not(this bool b)
        {
            return b == false;
        }
        static public bool IsNull(this object obj)
        {
            return obj == null;
        }
        static public bool IsNotNull(this object obj)
        {
            return obj.IsNull().Not();
        }
        static public bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        static public bool IsNotNullOrEmpty(this string str)
        {
            return str.IsNullOrEmpty().Not();
        }

        static public IEnumerable<T> ToEnumerable<T>(this T obj)
        {
            yield return obj;
        }

        static public bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            return collection.IsNull() || collection.Any().Not();
        }

        static public bool IsNotEmpty<T>(this IEnumerable<T> collection)
        {
            return collection.IsEmpty().Not();
        }

        static public IEnumerable<DateTime> ToDates(this DateTime s, DateTime e)
        {
            for (var d = s; d <= s; d = d.AddDays(1))
                yield return d;
        }
    }
}