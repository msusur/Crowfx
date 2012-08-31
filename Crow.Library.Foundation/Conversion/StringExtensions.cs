using Crow.Library.Foundation.Conversion;

namespace System
{
    /// <summary>
    /// Stringi bir tipi parse etmede kullanılacak extension methodlar.
    /// </summary>
    public static class StringConversionExtensions
    {
        /// <summary>
        /// Stringi Nullable Double a çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string</param>
        /// <returns>Çevrilmiş double.</returns>
        public static double? ToNullableDouble(this string text)
        {
            return ConversionHelper.Parse<double?>(text);
        }

        /// <summary>
        /// Stringi Double a çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <returns>Çevrilmiş double.</returns>
        public static double ToDouble(this string text)
        {
            return ConversionHelper.Parse<double>(text);
        }

        /// <summary>
        /// Stringi Double a çevirir. Çevirememesi halinde
        /// default valueyu döner.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <param name="defaultValue">
        ///  Çevrilme işleminin başarısız olması helinde dönücek değer.
        /// </param>
        /// <returns>Çevrilmiş double. </returns>
        public static double ToDouble(this string text, double defaultValue)
        {
            return ConversionHelper.Parse(text, defaultValue);
        }

        /// <summary>
        /// Stringi Nullable Float a çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string</param>
        /// <returns>Çevrilmiş float.</returns>
        public static float? ToNullableFloat(this string text)
        {
            return ConversionHelper.Parse<float?>(text);
        }

        /// <summary>
        /// Stringi Float a çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <returns>Çevrilmiş float.</returns>
        public static float ToFloat(this string text)
        {
            return ConversionHelper.Parse<float>(text);
        }

        /// <summary>
        /// Stringi Float a çevirir. Çevirememesi halinde
        /// default valueyu döner.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <param name="defaultValue">
        ///  Çevrilme işleminin başarısız olması helinde dönücek değer.
        /// </param>
        /// <returns>Çevrilmiş float.</returns>
        public static float ToFloat(this string text, float defaultValue)
        {
            return ConversionHelper.Parse(text, defaultValue);
        }

        /// <summary>
        /// Stringi Nullable Decimal ye çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <returns>Çevrilmiş decimal.</returns>
        public static decimal? ToNullableDecimal(this string text)
        {
            return ConversionHelper.Parse<decimal?>(text);
        }

        /// <summary>
        /// Stringi Decimal e çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <returns>Çevrilmiş decimal.</returns>
        public static decimal ToDecimal(this string text)
        {
            return ConversionHelper.Parse<decimal>(text);
        }

        /// <summary>
        /// Stringi Decimal e çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <param name="defaultValue">
        ///  Çevrilme işleminin başarısız olması helinde dönücek değer.
        /// </param>
        /// <returns>Çevrilmiş decimal.</returns>
        public static decimal ToDecimal(this string text, decimal defaultValue)
        {
            return ConversionHelper.Parse(text, defaultValue);
        }

        /// <summary>
        /// Stringi Nullable Int64 e çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <returns>Çevrilmiş long.</returns>
        public static long? ToNullableInt64(this string text)
        {
            return ConversionHelper.Parse<long?>(text);
        }

        /// <summary>
        /// Stringi Int64 e çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <returns>Çevrilmiş long.</returns>
        public static long ToInt64(this string text)
        {
            return ConversionHelper.Parse<long>(text);
        }

        /// <summary>
        /// Stringi Int64 e çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <param name="defaultValue">
        ///  Çevrilme işleminin başarısız olması helinde dönücek değer.
        /// </param>
        /// <returns>Çevrilmiş long.</returns>
        public static long ToInt64(this string text, long defaultValue)
        {
            return ConversionHelper.Parse(text, defaultValue);
        }

        /// <summary>
        /// Stringi Nullable Int32 ye çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <returns>Çevrilmiş int.</returns>
        public static int? ToNullableInt32(this string text)
        {
            return ConversionHelper.Parse<int?>(text);
        }

        /// <summary>
        /// Stringi Int32 ye çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <returns>Çevrilmiş int.</returns>
        public static int ToInt32(this string text)
        {
            return ConversionHelper.Parse<int>(text);
        }

        /// <summary>
        /// Stringi Int32 e çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <param name="defaultValue">
        ///  Çevrilme işleminin başarısız olması helinde dönücek değer.
        /// </param>
        /// <returns>Çevrilmiş int.</returns>
        public static int ToInt32(this string text, int defaultValue)
        {
            return ConversionHelper.Parse(text, defaultValue);
        }

        /// <summary>
        /// Stringi Nullable Int16 ye çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <returns>Çevrilmiş short.</returns>
        public static short? ToNullableInt16(this string text)
        {
            return ConversionHelper.Parse<short?>(text);
        }

        /// <summary>
        /// Stringi Int16 ye çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <returns>Çevrilmiş short.</returns>
        public static short ToInt16(this string text)
        {
            return ConversionHelper.Parse<short>(text);
        }

        /// <summary>
        /// Stringi Int16 e çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <param name="defaultValue">
        ///  Çevrilme işleminin başarısız olması helinde dönücek değer.
        /// </param>
        /// <returns>Çevrilmiş short.</returns>
        public static short ToInt16(this string text, short defaultValue)
        {
            return ConversionHelper.Parse(text, defaultValue);
        }

        /// <summary>
        /// Stringi Nullable Byte a çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <returns>Çevrilmiş byte.</returns>
        public static byte? ToNullableByte(this string text)
        {
            return ConversionHelper.Parse<byte?>(text);
        }

        /// <summary>
        /// Stringi Byte a çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <returns>Çevrilmiş short.</returns>
        public static byte ToByte(this string text)
        {
            return ConversionHelper.Parse<byte>(text);
        }

        /// <summary>
        /// Stringi Byte a çevirir.
        /// </summary>
        /// <param name="text">Çevirilecek string.</param>
        /// <param name="defaultValue">
        ///  Çevrilme işleminin başarısız olması helinde dönücek değer.
        /// </param>
        /// <returns>Çevrilmiş byte.</returns>
        public static byte ToByte(this string text, byte defaultValue)
        {
            return ConversionHelper.Parse(text, defaultValue);
        }

        /// <summary>
        /// Stringi Nullable DateTime a çevirir.
        /// </summary>
        /// <param name="text">String İfade.</param>
        /// <returns>Çevirilen DateTime.</returns>
        public static DateTime? ToNullableDateTime(this string text)
        {
            return ConversionHelper.Parse<DateTime?>(text);
        }

        /// <summary>
        /// Stringi DateTime a çevirir.
        /// </summary>
        /// <param name="text">String İfade.</param>
        /// <returns>Çevirilen DateTime.</returns>
        public static DateTime ToDateTime(this string text)
        {
            return ConversionHelper.Parse<DateTime>(text);
        }

        /// <summary>
        /// Stringi DateTime a çevirir.
        /// </summary>
        /// <param name="text">String İfade.</param>
        /// <param name="defaultValue">Çevirilime halinde geri dönüş değeri.</param>
        /// <returns>Çevirilen DateTime.</returns>
        public static DateTime ToDateTime(this string text, DateTime defaultValue)
        {
            return ConversionHelper.Parse(text, defaultValue);
        }

        /// <summary>
        /// Stringi T tipine çevirir.
        /// </summary>
        /// <typeparam name="T">Çevirilmek istenilen tip.</typeparam>
        /// <param name="text">Çevirilecek string.</param>
        /// <returns>Çevrilmiş string.</returns>
        public static T Parse<T>(this string text)
        {
            return ConversionHelper.Parse<T>(text);
        }

        /// <summary>Stringi T tipine çevirir.</summary>
        /// <typeparam name="T">Çevirilmek istenilen tip.</typeparam>
        /// <param name="text">Çevirilecek string.</param>
        /// <param name="defaultValue">Çevirilime halinde geri dönüş değeri.</param>
        /// <returns>Çevrilmiş string.</returns>
        public static T Parse<T>(this string text, T defaultValue)
        {
            return ConversionHelper.Parse(text, defaultValue);
        }
    }
}
