using System;
using System.Linq.Expressions;
using CCrossThrowIf;
using Xunit;

namespace CCrossThrowIfTests;

public class ThrowIfGenericTests
{
    #region String Tests

    [Fact]
    public void IsNullOrWhiteSpace_WhenStringIsNull_ThrowsSpecifiedException()
    {
string? testValue = null;
        

        Expression<Func<string?>> expression = () => testValue;

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsNullOrWhiteSpace(expression));
        
        Assert.NotNull(exception);
    }

    [Fact]
    public void IsNullOrWhiteSpace_WhenStringIsEmpty_ThrowsSpecifiedException()
    {
        string testValue = "";
        Expression<Func<string>> expression = () => testValue;

        Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsNullOrWhiteSpace(expression));
    }

    [Fact]
    public void IsNullOrWhiteSpace_WhenStringIsWhiteSpace_ThrowsSpecifiedException()
    {
        string testValue = "   ";
        Expression<Func<string>> expression = () => testValue;

        Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsNullOrWhiteSpace(expression));
    }

    [Fact]
    public void IsNullOrWhiteSpace_WhenStringIsValid_DoesNotThrow()
    {
        string testValue = "valid";
        Expression<Func<string>> expression = () => testValue;

        ThrowIf<InvalidOperationException>.IsNullOrWhiteSpace(expression);
    }

    [Fact]
    public void IsNullOrWhiteSpace_WithCustomMessage_ThrowsWithCustomMessage()
    {
string? testValue = null;
        

        Expression<Func<string?>> expression = () => testValue;
        string customMessage = "Custom error message";

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsNullOrWhiteSpace(expression, customMessage));
        
        Assert.Equal(customMessage, exception.Message);
    }

    [Fact]
    public void IsNullOrEmpty_WhenStringIsNull_ThrowsSpecifiedException()
    {
string? testValue = null;
        

        Expression<Func<string?>> expression = () => testValue;

        Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsNullOrEmpty(expression));
    }

    [Fact]
    public void IsNullOrEmpty_WhenStringIsEmpty_ThrowsSpecifiedException()
    {
        string testValue = "";
        Expression<Func<string>> expression = () => testValue;

        Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsNullOrEmpty(expression));
    }

    [Fact]
    public void IsNullOrEmpty_WhenStringIsValid_DoesNotThrow()
    {
        string testValue = "valid";
        Expression<Func<string>> expression = () => testValue;

        ThrowIf<InvalidOperationException>.IsNullOrEmpty(expression);
    }

    [Fact]
    public void IsNullOrEmpty_WithCustomMessage_ThrowsWithCustomMessage()
    {
        string testValue = "";
        Expression<Func<string>> expression = () => testValue;
        string customMessage = "String cannot be empty";

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsNullOrEmpty(expression, customMessage));
        
        Assert.Equal(customMessage, exception.Message);
    }

    #endregion

    #region TimeSpan Tests

    [Fact]
    public void IsNegativeOrZero_WhenTimeSpanIsNegative_ThrowsSpecifiedException()
    {
        TimeSpan testValue = TimeSpan.FromSeconds(-1);
        Expression<Func<TimeSpan>> expression = () => testValue;

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsNegativeOrZero(expression));
        
        Assert.Contains("lower or equal to zero", exception.Message);
    }

    [Fact]
    public void IsNegativeOrZero_WhenTimeSpanIsZero_ThrowsSpecifiedException()
    {
        TimeSpan testValue = TimeSpan.Zero;
        Expression<Func<TimeSpan>> expression = () => testValue;

        Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsNegativeOrZero(expression));
    }

    [Fact]
    public void IsNegativeOrZero_WhenTimeSpanIsPositive_DoesNotThrow()
    {
        TimeSpan testValue = TimeSpan.FromSeconds(1);
        Expression<Func<TimeSpan>> expression = () => testValue;

        ThrowIf<InvalidOperationException>.IsNegativeOrZero(expression);
    }

    [Fact]
    public void IsNegativeOrZero_WithCustomMessage_ThrowsWithCustomMessage()
    {
        TimeSpan testValue = TimeSpan.Zero;
        Expression<Func<TimeSpan>> expression = () => testValue;
        string customMessage = "TimeSpan must be positive";

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsNegativeOrZero(expression, customMessage));
        
        Assert.Equal(customMessage, exception.Message);
    }

    #endregion

    #region Generic Tests

    [Fact]
    public void IsDefault_WhenValueIsNull_ThrowsSpecifiedException()
    {
string? testValue = null;
        

        Expression<Func<string?>> expression = () => testValue;

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsDefault(expression));
        
        Assert.Contains("is equal to its default value", exception.Message);
    }

    [Fact]
    public void IsDefault_WhenValueIsNotNull_DoesNotThrow()
    {
        string testValue = "not null";
        Expression<Func<string>> expression = () => testValue;

        ThrowIf<InvalidOperationException>.IsDefault(expression);
    }

    [Fact]
    public void IsDefault_WithCustomMessage_ThrowsWithCustomMessage()
    {
string? testValue = null;
        

        Expression<Func<string?>> expression = () => testValue;
        string customMessage = "Value cannot be default";

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsDefault(expression, customMessage));
        
        Assert.Equal(customMessage, exception.Message);
    }

    [Fact]
    public void IsNull_WhenValueIsNull_ThrowsSpecifiedException()
    {
string? testValue = null;
        

        Expression<Func<string?>> expression = () => testValue;

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsNull(expression));
        
        Assert.Contains("is null", exception.Message);
    }

    [Fact]
    public void IsNull_WhenValueIsNotNull_DoesNotThrow()
    {
        string testValue = "not null";
        Expression<Func<string>> expression = () => testValue;

        ThrowIf<InvalidOperationException>.IsNull(expression);
    }

    [Fact]
    public void IsNull_WithCustomMessage_ThrowsWithCustomMessage()
    {
string? testValue = null;
        

        Expression<Func<string?>> expression = () => testValue;
        string customMessage = "Value cannot be null";

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsNull(expression, customMessage));
        
        Assert.Equal(customMessage, exception.Message);
    }

    [Fact]
    public void IsEqualTo_WhenValueIsEqualToTestValue_ThrowsSpecifiedException()
    {
        int testValue = 5;
        Expression<Func<int>> expression = () => testValue;

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsEqualTo(expression, 5));
        
        Assert.Contains("is not equal to 5", exception.Message);
    }

    [Fact]
    public void IsEqualTo_WhenValueIsNotEqualToTestValue_DoesNotThrow()
    {
        int testValue = 5;
        Expression<Func<int>> expression = () => testValue;

        ThrowIf<InvalidOperationException>.IsEqualTo(expression, 10);
    }

    [Fact]
    public void IsEqualTo_WithDefaultValue_WhenValueIsNotDefault_DoesNotThrow()
    {
        int testValue = 5;
        Expression<Func<int>> expression = () => testValue;

        ThrowIf<InvalidOperationException>.IsEqualTo(expression);
    }

    [Fact]
    public void IsEqualTo_WithCustomMessage_ThrowsWithCustomMessage()
    {
        int testValue = 5;
        Expression<Func<int>> expression = () => testValue;
        string customMessage = "Values cannot be equal";

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsEqualTo(expression, 5, customMessage));
        
        Assert.Equal(customMessage, exception.Message);
    }

    #endregion

    #region Different Exception Types Tests

    [Fact]
    public void ThrowIfGeneric_WithArgumentNullException_CreatesProperException()
    {
string? testValue = null;
        

        Expression<Func<string?>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentNullException>(() =>
            ThrowIf<ArgumentNullException>.IsNull(expression));
        
        Assert.NotNull(exception);
    }

    [Fact]
    public void ThrowIfGeneric_WithArgumentException_CreatesProperException()
    {
string? testValue = null;
        

        Expression<Func<string?>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentException>(() =>
            ThrowIf<ArgumentException>.IsNull(expression));
        
        Assert.Contains("testValue", exception.Message);
    }

    [Fact]
    public void ThrowIfGeneric_WithArgumentOutOfRangeException_CreatesProperException()
    {
        int testValue = 5;
        Expression<Func<int>> expression = () => testValue;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThrowIf<ArgumentOutOfRangeException>.IsEqualTo(expression, 5));
        
        Assert.Equal("testValue", exception.ParamName);
        Assert.Equal(5, exception.ActualValue);
    }

    [Fact]
    public void ThrowIfGeneric_WithCustomException_CreatesProperException()
    {
string? testValue = null;
        

        Expression<Func<string?>> expression = () => testValue;

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ThrowIf<InvalidOperationException>.IsNull(expression));
        
        Assert.NotNull(exception);
    }

    #endregion
}