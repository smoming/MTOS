using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
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

        static public string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings.AllKeys.Contains(key) ?
                ConfigurationManager.AppSettings.Get(key) : string.Empty;
        }

        public static string GetDescription(this Enum source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
             typeof(DescriptionAttribute), false);
            if (attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        public static T ToEnum<T>(this string val)
            where T : struct, IConvertible
        {
            if (typeof(T).IsEnum.Not())
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return (T)Enum.Parse(typeof(T), val);
        }

        public static List<T> GetEnumList<T>()
            where T : struct, IConvertible
        {
            if (typeof(T).IsEnum.Not())
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        public static string GetFileExtension(this HttpPostedFileBase file)
        {
            if (file.IsNull())
                return string.Empty;

            return Path.GetExtension(file.FileName).ToLower();
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}