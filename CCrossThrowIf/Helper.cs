using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CCrossThrowIf
{
    internal static class Helper
    {
        internal static Metadata<T> GetMetadata<T>(this Expression<Func<T>> expression)
        {
            return new Metadata<T>(expression);
        }

        public static TException CreateException<TException>(string? message)
            where TException : Exception, new()
        {
            var args = CreateArgsException<TException>(message);
            return (TException)Activator.CreateInstance(typeof(TException), args);
        }

        public static TException CreateException<TException>(string? message, string name, object? checkedValue = null)
            where TException : Exception, new()
        {
            var args = CreateArgsException<TException>(message, name, checkedValue);
            return (TException)Activator.CreateInstance(typeof(TException), args);
        }

        private static object[] CreateArgsException<TException>(string? message, string? paramName = null, object? actualValue = null)
        {
            var exceptionType = typeof(TException);
            
            if (exceptionType == typeof(ArgumentNullException))
            {
                if (paramName != null && message != null)
                    return new object[] { paramName, message };
                else if (paramName != null)
                    return new object[] { paramName };
                else if (message != null)
                    return new object[] { message };
                else
                    return new object[] { };
            }
            else if (exceptionType == typeof(ArgumentOutOfRangeException))
            {
                if (paramName != null && actualValue != null && message != null)
                    return new object[] { paramName, actualValue, message };
                else if (paramName != null && message != null)
                    return new object[] { paramName, message };
                else if (paramName != null)
                    return new object[] { paramName };
                else if (message != null)
                    return new object[] { null, null, message };
                else
                    return new object[] { };
            }
            else if (exceptionType == typeof(ArgumentException))
            {
                if (message != null && paramName != null)
                    return new object[] { message, paramName };
                else if (message != null)
                    return new object[] { message };
                else
                    return new object[] { };
            }
            else if (exceptionType == typeof(InvalidOperationException))
            {
                if (message != null)
                    return new object[] { message };
                else
                    return new object[] { };
            }
            else if (exceptionType == typeof(Exception) || exceptionType.BaseType == typeof(Exception))
            {
                if (message != null)
                    return new object[] { message };
                else
                    return new object[] { };
            }
            else
            {
                return new object[] { };
            }
        }
    }
}