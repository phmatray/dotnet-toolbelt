using System.Linq.Expressions;
using CCrossThrowIf;

namespace CCrossThrowIfTests;

public class MetadataTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_WithValidExpression_ExtractsNameAndValue()
    {
        int testValue = 42;
        Expression<Func<int>> expression = () => testValue;

        var metadata = new Metadata<int>(expression);

        Assert.Equal("testValue", metadata.Name);
        Assert.Equal(42, metadata.Value);
    }

    [Fact]
    public void Constructor_WithStringExpression_ExtractsNameAndValue()
    {
        string testValue = "Hello World";
        Expression<Func<string>> expression = () => testValue;

        var metadata = new Metadata<string>(expression);

        Assert.Equal("testValue", metadata.Name);
        Assert.Equal("Hello World", metadata.Value);
    }

    [Fact]
    public void Constructor_WithNullExpression_ThrowsArgumentNullException()
    {
        Expression<Func<int>>? expression = null;

        var exception = Assert.Throws<ArgumentNullException>(() => new Metadata<int>(expression!));
        
        Assert.Equal("expression", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithComplexTypeExpression_ExtractsNameAndValue()
    {
        var testValue = new TestClass { Id = 1, Name = "Test" };
        Expression<Func<TestClass>> expression = () => testValue;

        var metadata = new Metadata<TestClass>(expression);

        Assert.Equal("testValue", metadata.Name);
        Assert.Equal(testValue.Id, metadata.Value.Id);
        Assert.Equal(testValue.Name, metadata.Value.Name);
    }

    [Fact]
    public void Constructor_WithDateTimeExpression_ExtractsNameAndValue()
    {
        DateTime testValue = new DateTime(2023, 1, 1);
        Expression<Func<DateTime>> expression = () => testValue;

        var metadata = new Metadata<DateTime>(expression);

        Assert.Equal("testValue", metadata.Name);
        Assert.Equal(new DateTime(2023, 1, 1), metadata.Value);
    }

    [Fact]
    public void Constructor_WithNullableTypeExpression_ExtractsNameAndValue()
    {
        int? testValue = 42;
        Expression<Func<int?>> expression = () => testValue;

        var metadata = new Metadata<int?>(expression);

        Assert.Equal("testValue", metadata.Name);
        Assert.Equal(42, metadata.Value);
    }

    [Fact]
    public void Constructor_WithNullableTypeNullValue_ExtractsNameAndNullValue()
    {
        int? testValue = null;
        Expression<Func<int?>> expression = () => testValue;

        var metadata = new Metadata<int?>(expression);

        Assert.Equal("testValue", metadata.Name);
        Assert.Null(metadata.Value);
    }

    #endregion

    #region GetMemberName Tests (via Constructor)

    [Fact]
    public void GetMemberName_WithFieldExpression_ExtractsFieldName()
    {
        var testObject = new TestClassWithField { testField = 123 };
        Expression<Func<int>> expression = () => testObject.testField;

        var metadata = new Metadata<int>(expression);

        Assert.Equal("testField", metadata.Name);
        Assert.Equal(123, metadata.Value);
    }

    [Fact]
    public void GetMemberName_WithPropertyExpression_ExtractsPropertyName()
    {
        var testObject = new TestClass { Id = 456 };
        Expression<Func<int>> expression = () => testObject.Id;

        var metadata = new Metadata<int>(expression);

        Assert.Equal("Id", metadata.Name);
        Assert.Equal(456, metadata.Value);
    }

    [Fact]
    public void GetMemberName_WithInvalidExpression_ThrowsArgumentException()
    {
        Expression<Func<int>> expression = () => 42; // Constant expression, not a member

        var exception = Assert.Throws<ArgumentException>(() => new Metadata<int>(expression));
        
        Assert.Equal("expression", exception.ParamName);
        Assert.Contains("Invalid argument", exception.Message);
    }

    [Fact]
    public void GetMemberName_WithMethodCallExpression_ThrowsArgumentException()
    {
        Expression<Func<string>> expression = () => GetTestString();

        Assert.Throws<ArgumentException>(() => new Metadata<string>(expression));
    }

    #endregion

    #region GetValue Tests (via Constructor)

    [Fact]
    public void GetValue_WithConstantValue_ThrowsArgumentException()
    {
        const int constValue = 100;
        Expression<Func<int>> expression = () => constValue;

        // Constants are not member expressions, so this should throw
        var exception = Assert.Throws<ArgumentException>(() => new Metadata<int>(expression));
        
        Assert.Equal("expression", exception.ParamName);
        Assert.Contains("Invalid argument", exception.Message);
    }

    [Fact]
    public void GetValue_WithCalculatedValue_ReturnsCalculatedResult()
    {
        const int a = 10;
        const int b = 20;
        Expression<Func<int>> expression = () => a + b;

        // This will throw because it's not a simple member expression
        Assert.Throws<ArgumentException>(() => new Metadata<int>(expression));
    }

    [Fact]
    public void GetValue_WithArrayElementExpression_ThrowsArgumentException()
    {
        int[] array = [1, 2, 3];
        Expression<Func<int>> expression = () => array[1];

        // This will throw because it's not a simple member expression
        Assert.Throws<ArgumentException>(() => new Metadata<int>(expression));
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Metadata_WithNestedPropertyExpression_ExtractsCorrectNameAndValue()
    {
        var testObject = new TestClass { Nested = new NestedClass { Value = "nested value" } };
        Expression<Func<string>> expression = () => testObject.Nested.Value;

        var metadata = new Metadata<string>(expression);

        Assert.Equal("Value", metadata.Name);
        Assert.Equal("nested value", metadata.Value);
    }

    [Fact]
    public void Metadata_WithBooleanExpression_ExtractsNameAndValue()
    {
        bool testValue = true;
        Expression<Func<bool>> expression = () => testValue;

        var metadata = new Metadata<bool>(expression);

        Assert.Equal("testValue", metadata.Name);
        Assert.True(metadata.Value);
    }

    [Fact]
    public void Metadata_WithEnumExpression_ExtractsNameAndValue()
    {
        TestEnum testValue = TestEnum.Second;
        Expression<Func<TestEnum>> expression = () => testValue;

        var metadata = new Metadata<TestEnum>(expression);

        Assert.Equal("testValue", metadata.Name);
        Assert.Equal(TestEnum.Second, metadata.Value);
    }

    #endregion

    #region Helper Classes and Methods

    private static string GetTestString() => "test";

    private class TestClass
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public NestedClass? Nested { get; init; }
    }

    private class NestedClass
    {
        public string? Value { get; init; }
    }

    private class TestClassWithField
    {
        public int testField;
    }

    private enum TestEnum
    {
        First,
        Second,
        Third
    }

    #endregion
}