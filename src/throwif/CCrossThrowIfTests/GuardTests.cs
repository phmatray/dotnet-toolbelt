using CCrossThrowIf;

namespace CCrossThrowIfTests;

public class GuardTests
{
    #region String Guards

    [Fact]
    public void NullOrWhiteSpace_WithValidString_ReturnsValue()
    {
        const string input = "valid string";
        
        var result = Guard.Against.NullOrWhiteSpace(input);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void NullOrWhiteSpace_WithNull_ThrowsArgumentNullException()
    {
        string? input = null;
        
        var exception = Assert.Throws<ArgumentNullException>(() => 
            Guard.Against.NullOrWhiteSpace(input));
        
        Assert.Equal("input", exception.ParamName);
    }

    [Fact]
    public void NullOrWhiteSpace_WithEmpty_ThrowsArgumentNullException()
    {
        const string input = "";
        
        var exception = Assert.Throws<ArgumentNullException>(() => 
            Guard.Against.NullOrWhiteSpace(input));
        
        Assert.Equal("input", exception.ParamName);
    }

    [Fact]
    public void NullOrWhiteSpace_WithWhitespace_ThrowsArgumentNullException()
    {
        const string input = "   ";
        
        Assert.Throws<ArgumentNullException>(() => 
            Guard.Against.NullOrWhiteSpace(input));
    }

    [Fact]
    public void NullOrWhiteSpace_WithCustomMessage_UsesCustomMessage()
    {
        string? input = null;
        const string customMessage = "Custom message";
        
        var exception = Assert.Throws<ArgumentNullException>(() => 
            Guard.Against.NullOrWhiteSpace(input, message: customMessage));
        
        Assert.Contains(customMessage, exception.Message);
    }

    [Fact]
    public void NullOrEmpty_WithValidString_ReturnsValue()
    {
        const string input = "valid";
        
        var result = Guard.Against.NullOrEmpty(input);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void NullOrEmpty_WithNull_ThrowsArgumentNullException()
    {
        string? input = null;
        
        Assert.Throws<ArgumentNullException>(() => 
            Guard.Against.NullOrEmpty(input));
    }

    [Fact]
    public void NullOrEmpty_WithEmpty_ThrowsArgumentNullException()
    {
        const string input = "";
        
        Assert.Throws<ArgumentNullException>(() => 
            Guard.Against.NullOrEmpty(input));
    }

    [Fact]
    public void NullOrEmpty_WithWhitespace_ReturnsValue()
    {
        const string input = "   ";
        
        var result = Guard.Against.NullOrEmpty(input);
        
        Assert.Equal(input, result);
    }

    #endregion

    #region Null Guards

    [Fact]
    public void Null_WithValidReference_ReturnsValue()
    {
        var input = new object();
        
        var result = Guard.Against.Null(input);
        
        Assert.Same(input, result);
    }

    [Fact]
    public void Null_WithNullReference_ThrowsArgumentNullException()
    {
        object? input = null;
        
        var exception = Assert.Throws<ArgumentNullException>(() => 
            Guard.Against.Null(input));
        
        Assert.Equal("input", exception.ParamName);
    }

    [Fact]
    public void Null_WithNullableStruct_ReturnsValue()
    {
        int? input = 42;
        
        var result = Guard.Against.Null(input);
        
        Assert.Equal(42, result);
    }

    [Fact]
    public void Null_WithNullNullableStruct_ThrowsArgumentNullException()
    {
        int? input = null;
        
        Assert.Throws<ArgumentNullException>(() => 
            Guard.Against.Null(input));
    }

    #endregion

    #region Numeric Guards

    [Fact]
    public void Negative_WithPositiveValue_ReturnsValue()
    {
        const int input = 42;
        
        var result = Guard.Against.Negative(input);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void Negative_WithNegativeValue_ThrowsArgumentOutOfRangeException()
    {
        const int input = -1;
        
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => 
            Guard.Against.Negative(input));
        
        Assert.Equal("input", exception.ParamName);
        Assert.Equal(input, exception.ActualValue);
    }

    [Fact]
    public void Negative_WithZero_ReturnsValue()
    {
        const int input = 0;
        
        var result = Guard.Against.Negative(input);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void Zero_WithNonZeroValue_ReturnsValue()
    {
        const int input = 5;
        
        var result = Guard.Against.Zero(input);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void Zero_WithZeroValue_ThrowsArgumentOutOfRangeException()
    {
        const int input = 0;
        
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            Guard.Against.Zero(input));
    }

    [Fact]
    public void NegativeOrZero_WithPositiveValue_ReturnsValue()
    {
        const int input = 1;
        
        var result = Guard.Against.NegativeOrZero(input);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void NegativeOrZero_WithZero_ThrowsArgumentOutOfRangeException()
    {
        const int input = 0;
        
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            Guard.Against.NegativeOrZero(input));
    }

    [Fact]
    public void NegativeOrZero_WithNegative_ThrowsArgumentOutOfRangeException()
    {
        const int input = -5;
        
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            Guard.Against.NegativeOrZero(input));
    }

    #endregion

    #region Range Guards

    [Fact]
    public void OutOfRange_WithValueInRange_ReturnsValue()
    {
        const int input = 50;
        
        var result = Guard.Against.OutOfRange(input, 0, 100);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void OutOfRange_WithValueBelowRange_ThrowsArgumentOutOfRangeException()
    {
        const int input = -1;
        
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            Guard.Against.OutOfRange(input, 0, 100));
    }

    [Fact]
    public void OutOfRange_WithValueAboveRange_ThrowsArgumentOutOfRangeException()
    {
        const int input = 101;
        
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            Guard.Against.OutOfRange(input, 0, 100));
    }

    [Fact]
    public void OutOfRange_WithValueAtMinimum_ReturnsValue()
    {
        const int input = 0;
        
        var result = Guard.Against.OutOfRange(input, 0, 100);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void OutOfRange_WithValueAtMaximum_ReturnsValue()
    {
        const int input = 100;
        
        var result = Guard.Against.OutOfRange(input, 0, 100);
        
        Assert.Equal(input, result);
    }

    #endregion

    #region DateTime Guards

    [Fact]
    public void PastDateTime_WithFutureDate_ReturnsValue()
    {
        var input = DateTime.Now.AddDays(1);
        
        var result = Guard.Against.PastDateTime(input);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void PastDateTime_WithPastDate_ThrowsArgumentException()
    {
        var input = DateTime.Now.AddDays(-1);
        
        Assert.Throws<ArgumentException>(() => 
            Guard.Against.PastDateTime(input));
    }

    [Fact]
    public void FutureDateTime_WithPastDate_ReturnsValue()
    {
        var input = DateTime.Now.AddDays(-1);
        
        var result = Guard.Against.FutureDateTime(input);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void FutureDateTime_WithFutureDate_ThrowsArgumentException()
    {
        var input = DateTime.Now.AddDays(1);
        
        Assert.Throws<ArgumentException>(() => 
            Guard.Against.FutureDateTime(input));
    }

    #endregion

    #region TimeSpan Guards

    [Fact]
    public void NegativeOrZeroTimeSpan_WithPositiveTimeSpan_ReturnsValue()
    {
        var input = TimeSpan.FromSeconds(1);
        
        var result = Guard.Against.NegativeOrZeroTimeSpan(input);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void NegativeOrZeroTimeSpan_WithZero_ThrowsArgumentOutOfRangeException()
    {
        var input = TimeSpan.Zero;
        
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            Guard.Against.NegativeOrZeroTimeSpan(input));
    }

    [Fact]
    public void NegativeOrZeroTimeSpan_WithNegative_ThrowsArgumentOutOfRangeException()
    {
        var input = TimeSpan.FromSeconds(-1);
        
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            Guard.Against.NegativeOrZeroTimeSpan(input));
    }

    #endregion

    #region Collection Guards

    [Fact]
    public void NullOrEmpty_Collection_WithValidList_ReturnsValue()
    {
        var input = new List<int> { 1, 2, 3 };
        
        var result = Guard.Against.NullOrEmpty(input);
        
        Assert.Same(input, result);
    }

    [Fact]
    public void NullOrEmpty_Collection_WithNullList_ThrowsArgumentNullException()
    {
        List<int>? input = null;
        
        Assert.Throws<ArgumentNullException>(() => 
            Guard.Against.NullOrEmpty(input));
    }

    [Fact]
    public void NullOrEmpty_Collection_WithEmptyList_ThrowsArgumentException()
    {
        var input = new List<int>();
        
        Assert.Throws<ArgumentException>(() => 
            Guard.Against.NullOrEmpty(input));
    }

    [Fact]
    public void NullOrEmpty_Collection_WithArray_ReturnsValue()
    {
        var input = new[] { 1, 2, 3 };
        
        var result = Guard.Against.NullOrEmpty(input);
        
        Assert.Same(input, result);
    }

    #endregion

    #region Boolean Guards

    [Fact]
    public void True_WithFalse_ReturnsValue()
    {
        const bool input = false;
        
        var result = Guard.Against.True(input);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void True_WithTrue_ThrowsArgumentException()
    {
        const bool input = true;
        
        Assert.Throws<ArgumentException>(() => 
            Guard.Against.True(input));
    }

    [Fact]
    public void False_WithTrue_ReturnsValue()
    {
        const bool input = true;
        
        var result = Guard.Against.False(input);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void False_WithFalse_ThrowsArgumentException()
    {
        const bool input = false;
        
        Assert.Throws<ArgumentException>(() => 
            Guard.Against.False(input));
    }

    #endregion

    #region Equality Guards

    [Fact]
    public void EqualTo_WithDifferentValues_ReturnsValue()
    {
        const int input = 5;
        const int testValue = 10;
        
        var result = Guard.Against.EqualTo(input, testValue);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void EqualTo_WithEqualValues_ThrowsArgumentException()
    {
        const int input = 5;
        const int testValue = 5;
        
        Assert.Throws<ArgumentException>(() => 
            Guard.Against.EqualTo(input, testValue));
    }

    [Fact]
    public void Default_WithNonDefaultValue_ReturnsValue()
    {
        const int input = 42;
        
        var result = Guard.Against.Default(input);
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void Default_WithDefaultValue_ThrowsArgumentException()
    {
        const int input = 0;
        
        Assert.Throws<ArgumentException>(() => 
            Guard.Against.Default(input));
    }

    [Fact]
    public void Default_WithDefaultString_ThrowsArgumentException()
    {
        string? input = null;
        
        Assert.Throws<ArgumentException>(() => 
            Guard.Against.Default(input));
    }

    #endregion

    #region Extension Methods

    [Fact]
    public void GuardNull_Extension_WithValidValue_ReturnsValue()
    {
        var input = new object();
        
        var result = input.GuardNull();
        
        Assert.Same(input, result);
    }

    [Fact]
    public void GuardNullOrEmpty_Extension_WithValidString_ReturnsValue()
    {
        const string input = "valid";
        
        var result = input.GuardNullOrEmpty();
        
        Assert.Equal(input, result);
    }

    [Fact]
    public void GuardNullOrWhiteSpace_Extension_WithValidString_ReturnsValue()
    {
        const string input = "valid";
        
        var result = input.GuardNullOrWhiteSpace();
        
        Assert.Equal(input, result);
    }

    #endregion

    #region Method Chaining

    [Fact]
    public void MethodChaining_WorksCorrectly()
    {
        const string input = "  test  ";
        
        var result = Guard.Against.NullOrWhiteSpace(input)
            .Trim()
            .ToUpperInvariant();
        
        Assert.Equal("TEST", result);
    }

    #endregion
}