/* Author : 
 * Olivier Dahan
 * 
 * Link : 
 * http://www.e-naxos.com/Blog/post/C-creer-des-descendants-du-type-String.aspx
 */

using System;

namespace SimpleHelpers.Types
{
    public class MyString : IEquatable<MyString>, IConvertible
    {
        public MyString()
        {
        }

        public MyString(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public bool Equals(MyString other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || Equals(other.Value, Value);
        }

        public override string ToString()
            => Value;

        public static implicit operator MyString(string str) 
            => new MyString(str);
        
        public static implicit operator string(MyString dictionary)
            => dictionary.Value;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(MyString) &&
                   Equals((MyString)obj);
        }

        public override int GetHashCode()
            => Value?.GetHashCode() ?? 0;

        public static bool operator ==(MyString left, MyString right)
            => Equals(left, right);

        public static bool operator !=(MyString left, MyString right)
            => !Equals(left, right);

        #region IConvertible Members

        public TypeCode GetTypeCode()
            => TypeCode.String;

        public bool ToBoolean(IFormatProvider provider)
            => Convert.ToBoolean(Value, provider);

        public byte ToByte(IFormatProvider provider)
            => Convert.ToByte(Value, provider);

        public char ToChar(IFormatProvider provider)
            => Convert.ToChar(Value, provider);

        public DateTime ToDateTime(IFormatProvider provider)
            => Convert.ToDateTime(Value, provider);

        public decimal ToDecimal(IFormatProvider provider)
            => Convert.ToDecimal(Value, provider);

        public double ToDouble(IFormatProvider provider)
            => Convert.ToDouble(Value, provider);

        public short ToInt16(IFormatProvider provider)
            => Convert.ToInt16(Value, provider);

        public int ToInt32(IFormatProvider provider)
            => Convert.ToInt32(Value, provider);

        public long ToInt64(IFormatProvider provider)
            => Convert.ToInt64(Value, provider);

        public sbyte ToSByte(IFormatProvider provider)
            => Convert.ToSByte(Value, provider);

        public float ToSingle(IFormatProvider provider)
            => Convert.ToSingle(Value, provider);

        public string ToString(IFormatProvider provider)
            => Value;

        public object ToType(Type conversionType, IFormatProvider provider)
            => Convert.ChangeType(Value, conversionType, provider);

        public ushort ToUInt16(IFormatProvider provider)
            => Convert.ToUInt16(Value, provider);

        public uint ToUInt32(IFormatProvider provider)
            => Convert.ToUInt32(Value, provider);

        public ulong ToUInt64(IFormatProvider provider)
            => Convert.ToUInt64(Value, provider);

        #endregion
    }
}