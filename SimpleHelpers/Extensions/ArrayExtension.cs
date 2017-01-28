/* Author : 
 * Philippe Matray
 * 
 * Date : 
 * 2014-07-23
 */

using System;
using System.Linq;
using System.Text;

namespace SimpleHelpers.Extensions
{
    public static partial class ArrayExtension
    {
        #region Public Methods

        public static string ToString<T>(this T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            var sb = new StringBuilder(string.Empty);
            foreach (var obj in array)
                sb.AppendLine(obj.ToString());

            return sb.ToString();
        }

        public static T[] Trim<T>(this T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            var retval = array
                .SkipWhile(arg => arg.Equals(default(T)))
                .Reverse()
                .SkipWhile(arg => arg.Equals(default(T)))
                .Reverse()
                .ToArray();

            return retval;
        }

        public static T[] Remove<T>(this T[] array, T toRemove = default(T))
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            var retval = array
                .Where(arg => !arg.Equals(toRemove))
                .ToArray();

            return retval;
        }

        public static T[] Remove<T>(this T[] array, params T[] toRemoves)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (toRemoves == null)
                throw new ArgumentNullException(nameof(toRemoves));

            var retval = array
                .Where(arg => !toRemoves.Contains(arg))
                .ToArray();

            return retval;
        }

        public static T[] Replace<T>(this T[] array, T oldValue, T newValue)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            var retval = array
                .Select(arg => arg.Equals(oldValue) ? newValue : arg)
                .ToArray();

            return retval;
        }

        public static T[] ToArray<T>(this T[][] jaggedArray)
        {
            if (jaggedArray == null)
                throw new ArgumentNullException(nameof(jaggedArray));

            var elementsCount = jaggedArray
                .Where(arg => arg != null)
                .Sum(arg => arg.Length);
            var retval = new T[elementsCount];

            int index = 0;
            foreach (var t in jaggedArray)
                foreach (var t1 in t)
                    retval[index++] = t1;

            return retval;
        }

        public static T[] ToArray<T>(this T[,] multiArray)
        {
            if (multiArray == null)
                throw new ArgumentNullException(nameof(multiArray));

            var lines = multiArray.CountLines();
            var columns = multiArray.CountColumns();
            var retval = new T[lines * columns];

            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var multiArrayI = multiArray.GetLowerBound(0);
                    var multiArrayJ = multiArray.GetLowerBound(1);
                    retval[i * columns + j] = multiArray[multiArrayI + i, multiArrayJ + j];
                }
            }

            return retval;
        }

        public static T[][] ToJaggedArray<T>(this T[,] multiArray)
        {
            var lines = multiArray.CountLines();
            var columns = multiArray.CountColumns();
            var retval = new T[lines][];

            for (long i = 0; i < lines; i++)
            {
                retval[i] = new T[columns];
                for (long j = 0; j < columns; j++)
                {
                    var multiArrayI = multiArray.GetLowerBound(0);
                    var multiArrayJ = multiArray.GetLowerBound(1);
                    retval[i][j] = multiArray[multiArrayI + i, multiArrayJ + j];
                }
            }

            return retval;
        }

        public static T[,] ToMultiArray<T>(this T[][] jaggedArray)
        {
            if (jaggedArray == null)
                throw new ArgumentNullException(nameof(jaggedArray));

            int lines = jaggedArray.CountLines();
            int columns = jaggedArray.CountColumns();
            var retval = new T[lines, columns];

            for (int i = 0; i < jaggedArray.Length; i++)
            {
                var array = jaggedArray[i];
                if (array != null)
                {
                    for (int j = 0; j < array.Length; j++)
                        retval[i, j] = array[j];
                }
            }

            return retval;
        }

        public static int CountLines<T>(this T[,] multiArray)
        {
            if (multiArray == null)
                throw new ArgumentNullException(nameof(multiArray));

            return multiArray.GetLength(0);
        }

        public static int CountColumns<T>(this T[,] multiArray)
        {
            if (multiArray == null)
                throw new ArgumentNullException(nameof(multiArray));

            return multiArray.GetLength(1);
        }

        public static int CountLines<T>(this T[][] jaggedArray)
        {
            if (jaggedArray == null)
                throw new ArgumentNullException(nameof(jaggedArray));

            return jaggedArray.Length;
        }

        public static int CountColumns<T>(this T[][] jaggedArray)
        {
            if (jaggedArray == null)
                throw new ArgumentNullException(nameof(jaggedArray));

            if (jaggedArray.Count(arg => arg != null) > 0)
            {
                return jaggedArray
                    .Where(arg => arg != null)
                    .Max(arg => arg.Length);
            }

            return 0;
        }

        #endregion
    }
}