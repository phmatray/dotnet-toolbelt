/* Author : 
 * Philippe Matray
 * 
 * Date : 
 * 2014-09-22, 2017-01-25
 */

using System;

namespace SimpleHelpers.Extensions
{
    public static partial class DateTimeExtension
    {
        /// <summary>
        ///     Determines whether the specified date is monday.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsMonday(this DateTime date)
            => date.DayOfWeek == DayOfWeek.Monday;

        /// <summary>
        ///     Determines whether the specified date is tuesday.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsTuesday(this DateTime date)
            => date.DayOfWeek == DayOfWeek.Tuesday;

        /// <summary>
        ///     Determines whether the specified date is wednesday.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsWednesday(this DateTime date)
            => date.DayOfWeek == DayOfWeek.Wednesday;

        /// <summary>
        ///     Determines whether the specified date is thursday.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsThursday(this DateTime date)
            => date.DayOfWeek == DayOfWeek.Thursday;

        /// <summary>
        ///     Determines whether the specified date is friday.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsFriday(this DateTime date)
            => date.DayOfWeek == DayOfWeek.Friday;

        /// <summary>
        ///     Determines whether the specified date is saturday.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsSaturday(this DateTime date)
            => date.DayOfWeek == DayOfWeek.Saturday;

        /// <summary>
        ///     Determines whether the specified date is sunday.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsSunday(this DateTime date)
            => date.DayOfWeek == DayOfWeek.Sunday;

        /// <summary>
        ///     Determines whether the specified date is in the weekend.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime date)
            => date.IsSaturday() || date.IsSunday();

        /// <summary>
        ///     Determines whether the specified date is a working day of the week.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsWorkingWeek(this DateTime date)
            => !date.IsWeekend();

        /// <summary>
        ///     Gets the first day of the month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime FirstOfMonth(this DateTime date)
            => new DateTime(date.Year, date.Month, 1);

        /// <summary>
        ///     Gets the last day of the month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime LastOfMonth(this DateTime date)
            => new DateTime(date.Year, date.Month + 1, 1).AddDays(-1);

        /// <summary>
        ///     Gets the last day of the year.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime LastOfYear(this DateTime date)
            => new DateTime(date.Year, 12, 31);

        /// <summary>
        ///     Gets the first day ot the working week (Monday).
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime FirstOfWorkingWeek(this DateTime date)
            => date.AddDays(DayOfWeek.Monday - date.DayOfWeek);

        /// <summary>
        ///     Gets the last day ot the working week (Friday).
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime LastOfWorkingWeek(this DateTime date)
            => date.AddDays(DayOfWeek.Friday - date.DayOfWeek);
    }
}