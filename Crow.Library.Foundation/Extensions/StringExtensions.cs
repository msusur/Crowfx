﻿using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System
{
    //This class was copied from ServiceStack library. (Servicestack.net)
    public static class StringExtensions
    {
        static readonly Regex RegexSplitCamelCase = new Regex("([A-Z]|[0-9]+)",
#if !SILVERLIGHT && !MONOTOUCH && !XBOX
 RegexOptions.Compiled
#else
            RegexOptions.None
#endif
);
        /// <summary>
        /// Converts the string to a enum.
        /// </summary>
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        /// <summary>
        /// Converts the string to a enum and returns a default value if failed.
        /// </summary>
        public static T ToEnumOrDefault<T>(this string value, T defaultValue)
        {
            if (String.IsNullOrEmpty(value)) return defaultValue;
            return (T)Enum.Parse(typeof(T), value, true);
        }
        /// <summary>
        /// Splits the string into camelcase
        /// </summary>
        public static string SplitCamelCase(this string value)
        {
            return RegexSplitCamelCase.Replace(value, " $1").TrimStart();
        }
        /// <summary>
        /// Converts the string to english.
        /// </summary>
        public static string ToEnglish(this string camelCase)
        {
            var ucWords = camelCase.SplitCamelCase().ToLower();
            return ucWords[0].ToString(CultureInfo.InvariantCulture).ToUpper() + ucWords.Substring(1);
        }

        public static bool IsEmpty(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        public static bool EqualsIgnoreCase(this string value, string other)
        {
            return String.Equals(value, other, StringComparison.CurrentCultureIgnoreCase);
        }

        public static string ReplaceFirst(this string haystack, string needle, string replacement)
        {
            var pos = haystack.IndexOf(needle);
            if (pos < 0) return haystack;

            return haystack.Substring(0, pos) + replacement + haystack.Substring(pos + needle.Length);
        }

        public static string ReplaceAll(this string haystack, string needle, string replacement)
        {
            int pos;
            // Avoid a possible infinite loop
            if (needle == replacement) return haystack;
            while ((pos = haystack.IndexOf(needle)) > 0)
            {
                haystack = haystack.Substring(0, pos)
                    + replacement
                    + haystack.Substring(pos + needle.Length);
            }
            return haystack;
        }

        public static bool ContainsAny(this string text, params string[] testMatches)
        {
            foreach (var testMatch in testMatches)
            {
                if (text.Contains(testMatch)) return true;
            }
            return false;
        }

        private static readonly Regex InvalidVarCharsRegEx = new Regex(@"[^A-Za-z0-9]",
#if !SILVERLIGHT && !MONOTOUCH && !XBOX
 RegexOptions.Compiled
#else
 RegexOptions.None
#endif
);

        public static string SafeVarName(this string text)
        {
            if (String.IsNullOrEmpty(text)) return null;
            return InvalidVarCharsRegEx.Replace(text, "_");
        }


        public static string Join(this List<string> items, string delimeter)
        {
            return String.Join(delimeter, items.ToArray());
        }

        public static string ToParentPath(this string path)
        {
            var pos = path.LastIndexOf('/');
            if (pos == -1) return "/";

            var parentPath = path.Substring(0, pos);
            return parentPath;
        }

        public static string RemoveCharFlags(this string text, bool[] charFlags)
        {
            if (text == null) return null;

            var copy = text.ToCharArray();
            var nonWsPos = 0;

            for (var i = 0; i < text.Length; i++)
            {
                var @char = text[i];
                if (@char < charFlags.Length && charFlags[@char]) continue;
                copy[nonWsPos++] = @char;
            }

            return new String(copy, 0, nonWsPos);
        }

        public static string ToNullIfEmpty(this string text)
        {
            return String.IsNullOrEmpty(text) ? null : text;
        }


        private static char[] SystemTypeChars = new[] { '<', '>', '+' };

        public static bool IsUserType(this Type type)
        {
            return type.IsClass
                && type.Namespace != null
                && !type.Namespace.StartsWith("System.")
                && type.Name.IndexOfAny(SystemTypeChars) == -1;
        }

        public static string FormatText(this string text, params object[] args)
        {
            return string.Format(text, args);
        }
    }
}
