namespace LinqNotNull.UnitTests;

public class EnumerableExtensionsTests
{
    private readonly List<Product?> _products = new()
    {
        new Product("ABC123"),
        null,
        new Product("XYZ789", "description"),
        null
    };
    
    private static void AssertNonNullItems<T>(
        IReadOnlyCollection<T> result, int expectedCount, IEnumerable<T> expectedItems)
    {
        result.Should().NotContainNulls();
        result.Should().HaveCount(expectedCount);
    
        if (expectedCount > 0)
        {
            result.Should().Contain(expectedItems);
        }
        else
        {
            result.Should().BeEmpty();
        }
    }
    
    [Theory]
    [InlineData(new[] { "abc", null, "def", null, "ghi" }, 3, new[] { "abc", "def", "ghi" })]
    [InlineData(new string?[] { null, null, null, null, null }, 0, new string[] { })]
    [InlineData(new string?[] { }, 0, new string[] { })]
    public void SelectNotNull_WithVariousSource_ShouldReturnNonNullItems(
        string?[] source, int expectedCount, string[] expectedItems)
    {
        // Arrange

        // Act
        var result = source.SelectNotNull(x => x).ToList();

        // Assert
        AssertNonNullItems(result, expectedCount, expectedItems);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 3, new[] { "1", "3", "5" })]
    [InlineData(new[] { 2, 4, 6, 8, 10 }, 0, new string[] { })]
    public void SelectNotNull_WithVariousSourceAndTransform_ShouldReturnNonNullTransformedItems(
        int[] source, int expectedCount, string[] expectedItems)
    {
        // Arrange
        string? Transform(int x) => x % 2 == 0 ? null : x.ToString();

        // Act
        var result = source.SelectNotNull(Transform).ToList();

        // Assert
        AssertNonNullItems(result, expectedCount, expectedItems);
    }
    
    [Fact]
    public void SelectNotNull_WhenLargeInput_ReturnsCorrectResult()
    {
        // Arrange
        var largeInput = Enumerable.Range(1, 10000).Select(x => x % 2 == 0 ? null : x.ToString()).ToList();
        var expectedCount = 5000;
        var expectedResult = Enumerable.Range(1, 10000).Where(x => x % 2 != 0).Select(x => x.ToString());

        // Act
        var result = largeInput.SelectNotNull(x => x).ToList();

        // Assert
        AssertNonNullItems(result, expectedCount, expectedResult);
    }
    
    [Fact]
    public void SelectNotNull_WhenCalledOnProductList_ShouldReturnNonNullProducts()
    {
        // Arrange

        // Act
        var result = _products.SelectNotNull(x => x).ToList();
            
        // Assert
        result.Should().NotContainNulls();
        result.Should().HaveCount(2);
        result.Should().OnlyContain(p => p.SKU == "ABC123" || p.SKU == "XYZ789");
    }
        
    [Fact]
    public void SelectNotNull_WhenCalledOnProductListWithDescriptionSelector_ShouldReturnNonNullProductDescriptions()
    {
        // Arrange

        // Act
        var result = _products.SelectNotNull(x => x?.Description).ToList();
            
        // Assert
        result.Should().NotContainNulls();
        result.Should().HaveCount(1);
        result.Should().ContainSingle(d => d == "description");
    }
    
    [Fact]
    public void WhereNotNull_WhenCalledOnProductList_ShouldReturnNonNullProducts()
    {
        // Arrange

        // Act
        var result = _products.WhereNotNull().ToList();
            
        // Assert
        result.Should().NotContainNulls();
        result.Should().HaveCount(2);
        result.Should().OnlyContain(p => p.SKU == "ABC123" || p.SKU == "XYZ789");
    }
    
    [Fact]
    public void SelectNotNull_WhenNullSource_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<string>? nullSource = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => nullSource!.SelectNotNull(x => x));
    }

    [Fact]
    public void SelectNotNull_WhenNullSelector_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<string> source = new List<string> { "test" };
        Func<string, string>? nullSelector = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => source.SelectNotNull(nullSelector!));
    }
}