/* Author : 
 * Philippe Matray
 *
 * Date :
 * 2014-01-23, 2017-01-25
 */

using System;
using System.ComponentModel;
using System.Reflection;
using SimpleHelpers.Tools;

namespace SimpleHelpers.Extensions
{
    public static partial class EnumExtension
    {
        /// <summary>
        ///     Gets the attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The enum value.</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this Enum value)
            where T : Attribute
        {
            var retval = value
                .GetType()
                .GetTypeInfo()
                .GetDeclaredField(value.ToString())
                .GetCustomAttribute<T>();

            return retval;
        }

        /// <summary>
        ///     Gets a random value of enumeration.
        /// </summary>
        /// <returns></returns>
        public static T GetRandomEnumValue<T>()
            where T : struct
        {
            if (typeof(T).GetTypeInfo().IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            Array values = Enum.GetValues(typeof(T));
            var retval = (T)values.GetValue(StaticRandom.Instance.Next(values.Length));

            return retval;
        }

        /// <summary>
        ///     Returns the value of an enums <see cref="DescriptionAttribute"/>
        ///     if it exists, otherwise the enums value.
        /// </summary>
        /// <param name="value">The enum instance that this method extends.</param>
        /// <returns></returns>
        public static string ToDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])field
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            var retval = attributes.Length > 0
                ? attributes[0].Description
                : value.ToString();

            return retval;
        }
    }
}
