using Crow.Library.Foundation.Exceptions;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System;

namespace Crow.Library.Foundation.Conversion
{
    public static class ConversionHelper
    {
        public static T CastDbValue<T>(object o, T defaultValue)
        {
            try
            {
                if (o == DBNull.Value)
                {
                    return defaultValue;
                }

                return (T)o;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T CastDbValue<T>(object o)
        {
            try
            {
                object val = (o == DBNull.Value) ? null : o;
                return (T)val;
            }
            catch (Exception ex)
            {
                Type t = typeof(T);
                ConversionException cEx = new ConversionException("Conversion Hatası", ex);
                cEx.Data.Add("Value", o);
                cEx.Data.Add("Type", t.ToString());
                throw cEx;
            }
        }

        public static T ConvertTo<T>(object value, T defaultValue)
        {
            object obj;
            try
            {
                obj = ConvertTo(typeof(T), value);
            }
            catch
            {
                return defaultValue;
            }

            if (obj == null)
            {
                return defaultValue;
            }

            return (T)obj;
        }

        public static T ConvertTo<T>(object value)
        {
            return (T)ConvertTo(typeof(T), value, Thread.CurrentThread.CurrentCulture);
        }

        public static T ConvertTo<T>(object value, CultureInfo culture)
        {
            return (T)ConvertTo(typeof(T), value, culture);
        }

        public static object ConvertTo(Type t, object value)
        {
            return ConvertTo(t, value, Thread.CurrentThread.CurrentCulture);
        }

        public static object ConvertTo(Type t, object value, CultureInfo culture)
        {
            return CommonConvertOperation(t, value, culture, false);
        }

        public static T ChangeType<T>(object value, T defaultValue)
        {
            object obj;
            try
            {
                obj = ChangeType(typeof(T), value);
            }
            catch
            {
                return defaultValue;
            }

            if (obj == null)
            {
                return defaultValue;
            }

            return (T)obj;
        }

        public static T ChangeType<T>(object value)
        {
            return (T)ChangeType(typeof(T), value, Thread.CurrentThread.CurrentCulture);
        }

        public static T ChangeType<T>(object value, CultureInfo culture)
        {
            return (T)ChangeType(typeof(T), value, culture);
        }

        public static object ChangeType(Type t, object value)
        {
            return ChangeType(t, value, Thread.CurrentThread.CurrentCulture);
        }

        public static object ChangeType(Type t, object value, CultureInfo culture)
        {
            return CommonConvertOperation(t, value, culture, true);
        }

        private static object CommonConvertOperation(Type t, object value, CultureInfo culture, bool safeConvert)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t", "Type null olamaz");
            }

            try
            {
                if (t == typeof(string))
                {
                    return Convert.ToString(value);
                }

                if (value == null)
                {
                    return null;
                }

                if (t == value.GetType() || t == typeof(object))
                {
                    return value;
                }

                TypeConverter tc = TypeDescriptor.GetConverter(t);

                if (safeConvert)
                {
                    return tc.ConvertFrom(null, culture, value);
                }

                return tc.ConvertTo(null, culture, value, t);
            }
            catch (Exception ex)
            {
                ConversionException cEx = new ConversionException("Conversion Hatası", ex);
                cEx.Data.Add("Value", value);
                cEx.Data.Add("Type", t.ToString());
                cEx.Data.Add("Culture", culture.Name);
                throw cEx;
            }
        }

        public static T Parse<T>(string value, T defualtValue)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFromString(null, Thread.CurrentThread.CurrentCulture, value);
            }
            catch
            {
                return defualtValue;
            }
        }

        public static T Parse<T>(string value)
        {
            return (T)Parse(typeof(T), value);
        }

        public static T Parse<T>(string value, CultureInfo cultureInfo)
        {
            return (T)Parse(typeof(T), value, cultureInfo);
        }

        public static object Parse(Type t, string value)
        {
            return Parse(t, value, Thread.CurrentThread.CurrentCulture);
        }

        public static object Parse(Type t, string value, CultureInfo culture)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t", "Type null olamaz");
            }

            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(t);
                return converter.ConvertFromString(null, culture, value);
            }
            catch (Exception ex)
            {
                ConversionException cEx = new ConversionException("Parse Hatası", ex);
                cEx.Data.Add("Value", value);
                cEx.Data.Add("Type", t.ToString());
                cEx.Data.Add("Culture", culture.Name);
                throw cEx;
            }
        }

        public static void RegisterTypeConverter<T, TC>() where TC : TypeConverter
        {
            TypeDescriptor.AddAttributes(typeof(T), new TypeConverterAttribute(typeof(TC)));
        }
    }
}
