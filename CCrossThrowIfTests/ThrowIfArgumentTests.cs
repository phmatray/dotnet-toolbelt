using System.Linq.Expressions;
using CCrossThrowIf;

namespace CCrossThrowIfTests;

public class ThrowIfArgumentTests
{
    #region String Tests

    [Fact]
    public void IsNullOrWhiteSpace_WhenStringIsNull_ThrowsArgumentNullException()
    {
        string? testValue = null;


        Expression<Func<string?>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentNullException>(() =>
            ThrowIf.Argument.IsNullOrWhiteSpace(expression));

        Assert.Equal("testValue", exception.ParamName);
    }

    [Fact]
    public void IsNullOrWhiteSpace_WhenStringIsEmpty_ThrowsArgumentNullException()
    {
        const string testValue = "";
        Expression<Func<string?>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentNullException>(() =>
            ThrowIf.Argument.IsNullOrWhiteSpace(expression));

        Assert.Equal("testValue", exception.ParamName);
    }

    [Fact]
    public void IsNullOrWhiteSpace_WhenStringIsWhiteSpace_ThrowsArgumentNullException()
    {
        const string testValue = "   ";
        Expression<Func<string?>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentNullException>(() =>
            ThrowIf.Argument.IsNullOrWhiteSpace(expression));

        Assert.Equal("testValue", exception.ParamName);
    }

    [Fact]
    public void IsNullOrWhiteSpace_WhenStringIsValid_DoesNotThrow()
    {
        const string testValue = "valid";
        Expression<Func<string?>> expression = () => testValue;

        ThrowIf.Argument.IsNullOrWhiteSpace(expression);
    }

    [Fact]
    public void IsNullOrWhiteSpace_WithCustomMessage_ThrowsWithCustomMessage()
    {
        string? testValue = null;


        Expression<Func<string?>> expression = () => testValue;
        const string customMessage = "Custom error message";

        var exception = Assert.Throws<ArgumentNullException>(() =>
            ThrowIf.Argument.IsNullOrWhiteSpace(expression, customMessage));

        Assert.Contains(customMessage, exception.Message);
    }

    [Fact]
    public void IsNullOrEmpty_WhenStringIsNull_ThrowsArgumentNullException()
    {
        string? testValue = null;


        Expression<Func<string?>> expression = () => testValue;

        Assert.Throws<ArgumentNullException>(() =>
            ThrowIf.Argument.IsNullOrEmpty(expression));
    }

    [Fact]
    public void IsNullOrEmpty_WhenStringIsEmpty_ThrowsArgumentNullException()
    {
        const string testValue = "";
        Expression<Func<string?>> expression = () => testValue;

        Assert.Throws<ArgumentNullException>(() =>
            ThrowIf.Argument.IsNullOrEmpty(expression));
    }

    [Fact]
    public void IsNullOrEmpty_WhenStringIsValid_DoesNotThrow()
    {
        const string testValue = "valid";
        Expression<Func<string?>> expression = () => testValue;

        ThrowIf.Argument.IsNullOrEmpty(expression);
    }

    #endregion

    #region TimeSpan Tests

    [Fact]
    public void IsNegativeOrZero_WhenTimeSpanIsNegative_ThrowsArgumentOutOfRangeException()
    {
        TimeSpan testValue = TimeSpan.FromSeconds(-1);
        Expression<Func<TimeSpan>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsNegativeOrZero(expression));

        Assert.Contains("lower or equal to zero", exception.Message);
    }

    [Fact]
    public void IsNegativeOrZero_WhenTimeSpanIsZero_ThrowsArgumentOutOfRangeException()
    {
        TimeSpan testValue = TimeSpan.Zero;
        Expression<Func<TimeSpan>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsNegativeOrZero(expression));
    }

    [Fact]
    public void IsNegativeOrZero_WhenTimeSpanIsPositive_DoesNotThrow()
    {
        TimeSpan testValue = TimeSpan.FromSeconds(1);
        Expression<Func<TimeSpan>> expression = () => testValue;

        ThrowIf.Argument.IsNegativeOrZero(expression);
    }

    [Fact]
    public void IsNegativeOrZero_WithCustomMessage_ThrowsWithCustomMessage()
    {
        TimeSpan testValue = TimeSpan.Zero;
        Expression<Func<TimeSpan>> expression = () => testValue;
        const string customMessage = "Custom timespan error";

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsNegativeOrZero(expression, customMessage));

        Assert.Contains(customMessage, exception.Message);
    }

    #endregion

    #region DateTime Tests

    [Fact]
    public void IsInThePast_WhenDateTimeIsInPast_ThrowsArgumentException()
    {
        DateTime testValue = DateTime.Now.AddDays(-1);
        Expression<Func<DateTime>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentException>(() =>
            ThrowIf.Argument.IsInThePast(expression));

        Assert.Contains("is in the past", exception.Message);
    }

    [Fact]
    public void IsInThePast_WhenDateTimeIsInFuture_DoesNotThrow()
    {
        DateTime testValue = DateTime.Now.AddDays(1);
        Expression<Func<DateTime>> expression = () => testValue;

        ThrowIf.Argument.IsInThePast(expression);
    }

    [Fact]
    public void IsInThePast_WithCustomMessage_ThrowsWithCustomMessage()
    {
        DateTime testValue = DateTime.Now.AddDays(-1);
        Expression<Func<DateTime>> expression = () => testValue;
        const string customMessage = "Date cannot be in the past";

        var exception = Assert.Throws<ArgumentException>(() =>
            ThrowIf.Argument.IsInThePast(expression, customMessage));

        Assert.Equal(customMessage, exception.Message);
    }

    [Fact]
    public void IsInTheFuture_WhenDateTimeIsInFuture_ThrowsArgumentException()
    {
        DateTime testValue = DateTime.Now.AddDays(1);
        Expression<Func<DateTime>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentException>(() =>
            ThrowIf.Argument.IsInTheFuture(expression));

        Assert.Contains("is in the future", exception.Message);
    }

    [Fact]
    public void IsInTheFuture_WhenDateTimeIsInPast_DoesNotThrow()
    {
        DateTime testValue = DateTime.Now.AddDays(-1);
        Expression<Func<DateTime>> expression = () => testValue;

        ThrowIf.Argument.IsInTheFuture(expression);
    }

    #endregion

    #region Bool Tests

    [Fact]
    public void IsTrue_WhenBoolIsTrue_ThrowsArgumentException()
    {
        const bool testValue = true;
        Expression<Func<bool>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentException>(() =>
            ThrowIf.Argument.IsTrue(expression));

        Assert.Contains("is true", exception.Message);
    }

    [Fact]
    public void IsTrue_WhenBoolIsFalse_DoesNotThrow()
    {
        const bool testValue = false;
        Expression<Func<bool>> expression = () => testValue;

        ThrowIf.Argument.IsTrue(expression);
    }

    [Fact]
    public void IsFalse_WhenBoolIsFalse_ThrowsArgumentException()
    {
        const bool testValue = false;
        Expression<Func<bool>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentException>(() =>
            ThrowIf.Argument.IsFalse(expression));

        Assert.Contains("is false", exception.Message);
    }

    [Fact]
    public void IsFalse_WhenBoolIsTrue_DoesNotThrow()
    {
        const bool testValue = true;
        Expression<Func<bool>> expression = () => testValue;

        ThrowIf.Argument.IsFalse(expression);
    }

    #endregion

    #region Int Tests

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    public void IsPositive_WhenIntIsPositive_ThrowsArgumentOutOfRangeException(int value)
    {
        int testValue = value;
        Expression<Func<int>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsPositive(expression));

        Assert.Equal("testValue", exception.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void IsPositive_WhenIntIsNotPositive_DoesNotThrow(int value)
    {
        int testValue = value;
        Expression<Func<int>> expression = () => testValue;

        ThrowIf.Argument.IsPositive(expression);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void IsNegative_WhenIntIsNegative_ThrowsArgumentOutOfRangeException(int value)
    {
        int testValue = value;
        Expression<Func<int>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsNegative(expression));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void IsNegative_WhenIntIsNotNegative_DoesNotThrow(int value)
    {
        int testValue = value;
        Expression<Func<int>> expression = () => testValue;

        ThrowIf.Argument.IsNegative(expression);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void IsPositiveOrZero_WhenIntIsPositiveOrZero_ThrowsArgumentOutOfRangeException(int value)
    {
        int testValue = value;
        Expression<Func<int>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsPositiveOrZero(expression));
    }

    [Fact]
    public void IsPositiveOrZero_WhenIntIsNegative_DoesNotThrow()
    {
        const int testValue = -1;
        Expression<Func<int>> expression = () => testValue;

        ThrowIf.Argument.IsPositiveOrZero(expression);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void IsNegativeOrZero_WhenIntIsNegativeOrZero_ThrowsArgumentOutOfRangeException(int value)
    {
        int testValue = value;
        Expression<Func<int>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsNegativeOrZero(expression));
    }

    [Fact]
    public void IsNegativeOrZero_WhenIntIsPositive_DoesNotThrow()
    {
        const int testValue = 1;
        Expression<Func<int>> expression = () => testValue;

        ThrowIf.Argument.IsNegativeOrZero(expression);
    }

    [Fact]
    public void IsGreaterThan_WhenIntIsGreaterThanLimit_ThrowsArgumentOutOfRangeException()
    {
        const int testValue = 10;
        Expression<Func<int>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsGreaterThan(expression, 5));

        Assert.Contains("is greater than 5", exception.Message);
    }

    [Fact]
    public void IsGreaterThan_WhenIntIsNotGreaterThanLimit_DoesNotThrow()
    {
        const int testValue = 3;
        Expression<Func<int>> expression = () => testValue;

        ThrowIf.Argument.IsGreaterThan(expression, 5);
    }

    [Fact]
    public void IsLowerThan_WhenIntIsLowerThanLimit_ThrowsArgumentOutOfRangeException()
    {
        const int testValue = 3;
        Expression<Func<int>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsLowerThan(expression, 5));

        Assert.Contains("is lower than 5", exception.Message);
    }

    [Fact]
    public void IsLowerThan_WhenIntIsNotLowerThanLimit_DoesNotThrow()
    {
        const int testValue = 10;
        Expression<Func<int>> expression = () => testValue;

        ThrowIf.Argument.IsLowerThan(expression, 5);
    }

    [Fact]
    public void IntBase_WithCustomMessage_ThrowsWithCustomMessage()
    {
        const int testValue = 1;
        Expression<Func<int>> expression = () => testValue;
        const string customMessage = "Custom int error";

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsPositive(expression, customMessage));

        Assert.Contains(customMessage, exception.Message);
    }

    #endregion

    #region Long Tests

    [Theory]
    [InlineData(1L)]
    [InlineData(100L)]
    public void IsPositive_WhenLongIsPositive_ThrowsArgumentOutOfRangeException(long value)
    {
        long testValue = value;
        Expression<Func<long>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsPositive(expression));
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-1L)]
    public void IsPositive_WhenLongIsNotPositive_DoesNotThrow(long value)
    {
        long testValue = value;
        Expression<Func<long>> expression = () => testValue;

        ThrowIf.Argument.IsPositive(expression);
    }

    [Theory]
    [InlineData(-1L)]
    [InlineData(-100L)]
    public void IsNegative_WhenLongIsNegative_ThrowsArgumentOutOfRangeException(long value)
    {
        long testValue = value;
        Expression<Func<long>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsNegative(expression));
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(1L)]
    public void IsNegative_WhenLongIsNotNegative_DoesNotThrow(long value)
    {
        long testValue = value;
        Expression<Func<long>> expression = () => testValue;

        ThrowIf.Argument.IsNegative(expression);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(1L)]
    public void IsPositiveOrZero_WhenLongIsPositiveOrZero_ThrowsArgumentOutOfRangeException(long value)
    {
        long testValue = value;
        Expression<Func<long>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsPositiveOrZero(expression));
    }

    [Fact]
    public void IsPositiveOrZero_WhenLongIsNegative_DoesNotThrow()
    {
        const long testValue = -1L;
        Expression<Func<long>> expression = () => testValue;

        ThrowIf.Argument.IsPositiveOrZero(expression);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-1L)]
    public void IsNegativeOrZero_WhenLongIsNegativeOrZero_ThrowsArgumentOutOfRangeException(long value)
    {
        long testValue = value;
        Expression<Func<long>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsNegativeOrZero(expression));
    }

    [Fact]
    public void IsNegativeOrZero_WhenLongIsPositive_DoesNotThrow()
    {
        const long testValue = 1L;
        Expression<Func<long>> expression = () => testValue;

        ThrowIf.Argument.IsNegativeOrZero(expression);
    }

    [Fact]
    public void IsGreaterThan_WhenLongIsGreaterThanLimit_ThrowsArgumentOutOfRangeException()
    {
        const long testValue = 10L;
        Expression<Func<long>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsGreaterThan(expression, 5L));
    }

    [Fact]
    public void IsGreaterThan_WhenLongIsNotGreaterThanLimit_DoesNotThrow()
    {
        const long testValue = 3L;
        Expression<Func<long>> expression = () => testValue;

        ThrowIf.Argument.IsGreaterThan(expression, 5L);
    }

    [Fact]
    public void IsLowerThan_WhenLongIsLowerThanLimit_ThrowsArgumentOutOfRangeException()
    {
        const long testValue = 3L;
        Expression<Func<long>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsLowerThan(expression, 5L));
    }

    [Fact]
    public void IsLowerThan_WhenLongIsNotLowerThanLimit_DoesNotThrow()
    {
        const long testValue = 10L;
        Expression<Func<long>> expression = () => testValue;

        ThrowIf.Argument.IsLowerThan(expression, 5L);
    }

    #endregion

    #region Float Tests

    [Theory]
    [InlineData(1.0F)]
    [InlineData(100.5F)]
    public void IsPositive_WhenFloatIsPositive_ThrowsArgumentOutOfRangeException(float value)
    {
        float testValue = value;
        Expression<Func<float>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsPositive(expression));
    }

    [Theory]
    [InlineData(0.0F)]
    [InlineData(-1.0F)]
    public void IsPositive_WhenFloatIsNotPositive_DoesNotThrow(float value)
    {
        float testValue = value;
        Expression<Func<float>> expression = () => testValue;

        ThrowIf.Argument.IsPositive(expression);
    }

    [Theory]
    [InlineData(-1.0F)]
    [InlineData(-100.5F)]
    public void IsNegative_WhenFloatIsNegative_ThrowsArgumentOutOfRangeException(float value)
    {
        float testValue = value;
        Expression<Func<float>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsNegative(expression));
    }

    [Theory]
    [InlineData(0.0F)]
    [InlineData(1.0F)]
    public void IsNegative_WhenFloatIsNotNegative_DoesNotThrow(float value)
    {
        float testValue = value;
        Expression<Func<float>> expression = () => testValue;

        ThrowIf.Argument.IsNegative(expression);
    }

    [Theory]
    [InlineData(0.0F)]
    [InlineData(1.0F)]
    public void IsPositiveOrZero_WhenFloatIsPositiveOrZero_ThrowsArgumentOutOfRangeException(float value)
    {
        float testValue = value;
        Expression<Func<float>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsPositiveOrZero(expression));
    }

    [Fact]
    public void IsPositiveOrZero_WhenFloatIsNegative_DoesNotThrow()
    {
        const float testValue = -1.0F;
        Expression<Func<float>> expression = () => testValue;

        ThrowIf.Argument.IsPositiveOrZero(expression);
    }

    [Theory]
    [InlineData(0.0F)]
    [InlineData(-1.0F)]
    public void IsNegativeOrZero_WhenFloatIsNegativeOrZero_ThrowsArgumentOutOfRangeException(float value)
    {
        float testValue = value;
        Expression<Func<float>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsNegativeOrZero(expression));
    }

    [Fact]
    public void IsNegativeOrZero_WhenFloatIsPositive_DoesNotThrow()
    {
        const float testValue = 1.0F;
        Expression<Func<float>> expression = () => testValue;

        ThrowIf.Argument.IsNegativeOrZero(expression);
    }

    [Fact]
    public void IsGreaterThan_WhenFloatIsGreaterThanLimit_ThrowsArgumentOutOfRangeException()
    {
        const float testValue = 10.5F;
        Expression<Func<float>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsGreaterThan(expression, 5.0F));
    }

    [Fact]
    public void IsGreaterThan_WhenFloatIsNotGreaterThanLimit_DoesNotThrow()
    {
        const float testValue = 3.0F;
        Expression<Func<float>> expression = () => testValue;

        ThrowIf.Argument.IsGreaterThan(expression, 5.0F);
    }

    [Fact]
    public void IsLowerThan_WhenFloatIsLowerThanLimit_ThrowsArgumentOutOfRangeException()
    {
        const float testValue = 3.0F;
        Expression<Func<float>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsLowerThan(expression, 5.0F));
    }

    [Fact]
    public void IsLowerThan_WhenFloatIsNotLowerThanLimit_DoesNotThrow()
    {
        const float testValue = 10.0F;
        Expression<Func<float>> expression = () => testValue;

        ThrowIf.Argument.IsLowerThan(expression, 5.0F);
    }

    #endregion

    #region Double Tests

    [Theory]
    [InlineData(1.0)]
    [InlineData(100.5)]
    public void IsPositive_WhenDoubleIsPositive_ThrowsArgumentOutOfRangeException(double value)
    {
        double testValue = value;
        Expression<Func<double>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsPositive(expression));
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    public void IsPositive_WhenDoubleIsNotPositive_DoesNotThrow(double value)
    {
        double testValue = value;
        Expression<Func<double>> expression = () => testValue;

        ThrowIf.Argument.IsPositive(expression);
    }

    [Theory]
    [InlineData(-1.0)]
    [InlineData(-100.5)]
    public void IsNegative_WhenDoubleIsNegative_ThrowsArgumentOutOfRangeException(double value)
    {
        double testValue = value;
        Expression<Func<double>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsNegative(expression));
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(1.0)]
    public void IsNegative_WhenDoubleIsNotNegative_DoesNotThrow(double value)
    {
        double testValue = value;
        Expression<Func<double>> expression = () => testValue;

        ThrowIf.Argument.IsNegative(expression);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(1.0)]
    public void IsPositiveOrZero_WhenDoubleIsPositiveOrZero_ThrowsArgumentOutOfRangeException(double value)
    {
        double testValue = value;
        Expression<Func<double>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsPositiveOrZero(expression));
    }

    [Fact]
    public void IsPositiveOrZero_WhenDoubleIsNegative_DoesNotThrow()
    {
        const double testValue = -1.0;
        Expression<Func<double>> expression = () => testValue;

        ThrowIf.Argument.IsPositiveOrZero(expression);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    public void IsNegativeOrZero_WhenDoubleIsNegativeOrZero_ThrowsArgumentOutOfRangeException(double value)
    {
        double testValue = value;
        Expression<Func<double>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsNegativeOrZero(expression));
    }

    [Fact]
    public void IsNegativeOrZero_WhenDoubleIsPositive_DoesNotThrow()
    {
        const double testValue = 1.0;
        Expression<Func<double>> expression = () => testValue;

        ThrowIf.Argument.IsNegativeOrZero(expression);
    }

    [Fact]
    public void IsGreaterThan_WhenDoubleIsGreaterThanLimit_ThrowsArgumentOutOfRangeException()
    {
        const double testValue = 10.5;
        Expression<Func<double>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsGreaterThan(expression, 5.0));
    }

    [Fact]
    public void IsGreaterThan_WhenDoubleIsNotGreaterThanLimit_DoesNotThrow()
    {
        const double testValue = 3.0;
        Expression<Func<double>> expression = () => testValue;

        ThrowIf.Argument.IsGreaterThan(expression, 5.0);
    }

    [Fact]
    public void IsLowerThan_WhenDoubleIsLowerThanLimit_ThrowsArgumentOutOfRangeException()
    {
        const double testValue = 3.0;
        Expression<Func<double>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsLowerThan(expression, 5.0));
    }

    [Fact]
    public void IsLowerThan_WhenDoubleIsNotLowerThanLimit_DoesNotThrow()
    {
        const double testValue = 10.0;
        Expression<Func<double>> expression = () => testValue;

        ThrowIf.Argument.IsLowerThan(expression, 5.0);
    }

    #endregion

    #region Decimal Tests

    [Theory]
    [InlineData(1.0)]
    [InlineData(100.5)]
    public void IsPositive_WhenDecimalIsPositive_ThrowsArgumentOutOfRangeException(double val)
    {
        decimal testValue = (decimal)val;
        Expression<Func<decimal>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsPositive(expression));
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    public void IsPositive_WhenDecimalIsNotPositive_DoesNotThrow(double val)
    {
        decimal testValue = (decimal)val;
        Expression<Func<decimal>> expression = () => testValue;

        ThrowIf.Argument.IsPositive(expression);
    }

    [Theory]
    [InlineData(-1.0)]
    [InlineData(-100.5)]
    public void IsNegative_WhenDecimalIsNegative_ThrowsArgumentOutOfRangeException(double val)
    {
        decimal testValue = (decimal)val;
        Expression<Func<decimal>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsNegative(expression));
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(1.0)]
    public void IsNegative_WhenDecimalIsNotNegative_DoesNotThrow(double val)
    {
        decimal testValue = (decimal)val;
        Expression<Func<decimal>> expression = () => testValue;

        ThrowIf.Argument.IsNegative(expression);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(1.0)]
    public void IsPositiveOrZero_WhenDecimalIsPositiveOrZero_ThrowsArgumentOutOfRangeException(double val)
    {
        decimal testValue = (decimal)val;
        Expression<Func<decimal>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsPositiveOrZero(expression));
    }

    [Fact]
    public void IsPositiveOrZero_WhenDecimalIsNegative_DoesNotThrow()
    {
        const decimal testValue = -1.0m;
        Expression<Func<decimal>> expression = () => testValue;

        ThrowIf.Argument.IsPositiveOrZero(expression);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    public void IsNegativeOrZero_WhenDecimalIsNegativeOrZero_ThrowsArgumentOutOfRangeException(double val)
    {
        decimal testValue = (decimal)val;
        Expression<Func<decimal>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsNegativeOrZero(expression));
    }

    [Fact]
    public void IsNegativeOrZero_WhenDecimalIsPositive_DoesNotThrow()
    {
        const decimal testValue = 1.0m;
        Expression<Func<decimal>> expression = () => testValue;

        ThrowIf.Argument.IsNegativeOrZero(expression);
    }

    [Fact]
    public void IsGreaterThan_WhenDecimalIsGreaterThanLimit_ThrowsArgumentOutOfRangeException()
    {
        const decimal testValue = 10.5m;
        Expression<Func<decimal>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsGreaterThan(expression, 5.0m));
    }

    [Fact]
    public void IsGreaterThan_WhenDecimalIsNotGreaterThanLimit_DoesNotThrow()
    {
        const decimal testValue = 3.0m;
        Expression<Func<decimal>> expression = () => testValue;

        ThrowIf.Argument.IsGreaterThan(expression, 5.0m);
    }

    [Fact]
    public void IsLowerThan_WhenDecimalIsLowerThanLimit_ThrowsArgumentOutOfRangeException()
    {
        const decimal testValue = 3.0m;
        Expression<Func<decimal>> expression = () => testValue;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsLowerThan(expression, 5.0m));
    }

    [Fact]
    public void IsLowerThan_WhenDecimalIsNotLowerThanLimit_DoesNotThrow()
    {
        const decimal testValue = 10.0m;
        Expression<Func<decimal>> expression = () => testValue;

        ThrowIf.Argument.IsLowerThan(expression, 5.0m);
    }

    #endregion

    #region Generic Tests

    [Fact]
    public void IsDefault_WhenValueIsNull_ThrowsArgumentException()
    {
        string? testValue = null;


        Expression<Func<string?>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentException>(() =>
            ThrowIf.Argument.IsDefault(expression));

        Assert.Contains("is equal to its default value", exception.Message);
    }

    [Fact]
    public void IsDefault_WhenValueIsNotNull_DoesNotThrow()
    {
        const string testValue = "not null";
        Expression<Func<string?>> expression = () => testValue;

        ThrowIf.Argument.IsDefault(expression);
    }

    [Fact]
    public void IsDefault_WithCustomMessage_ThrowsWithCustomMessage()
    {
        string? testValue = null;


        Expression<Func<string?>> expression = () => testValue;
        const string customMessage = "Value is default";

        var exception = Assert.Throws<ArgumentException>(() =>
            ThrowIf.Argument.IsDefault(expression, customMessage));

        Assert.Equal(customMessage, exception.Message);
    }

    [Fact]
    public void IsNull_WhenValueIsNull_ThrowsArgumentException()
    {
        string? testValue = null;


        Expression<Func<string?>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentException>(() =>
            ThrowIf.Argument.IsNull(expression));

        Assert.Contains("is null", exception.Message);
    }

    [Fact]
    public void IsNull_WhenValueIsNotNull_DoesNotThrow()
    {
        const string testValue = "not null";
        Expression<Func<string?>> expression = () => testValue;

        ThrowIf.Argument.IsNull(expression);
    }

    [Fact]
    public void IsEqualTo_WhenValueIsEqualToTestValue_ThrowsArgumentOutOfRangeException()
    {
        const int testValue = 5;
        Expression<Func<int>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf.Argument.IsEqualTo(expression, 5));

        Assert.Contains("is not equal to 5", exception.Message);
    }

    [Fact]
    public void IsEqualTo_WhenValueIsNotEqualToTestValue_DoesNotThrow()
    {
        const int testValue = 5;
        Expression<Func<int>> expression = () => testValue;

        ThrowIf.Argument.IsEqualTo(expression, 10);
    }

    [Fact]
    public void IsEqualTo_WithDefaultValue_WhenValueIsNotDefault_DoesNotThrow()
    {
        const int testValue = 5;
        Expression<Func<int>> expression = () => testValue;

        ThrowIf.Argument.IsEqualTo(expression);
    }

    #endregion
}