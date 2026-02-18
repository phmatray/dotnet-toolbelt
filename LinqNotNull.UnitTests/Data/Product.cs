namespace LinqNotNull.UnitTests.Data;

/// <summary>
/// This is a product
/// </summary>
/// <param name="SKU">SKU is mandatory</param>
/// <param name="Description">Description is optional and can be null</param>
public record Product(
    string SKU,
    string? Description = null);

public class ProductRepository
{
    private readonly List<Product?> _products = new()
    {
        new Product("ABC123"),
        null,
        new Product("XYZ789", "description"),
        null
    };

    // This method use the out-of-the-box Select 
    //
    // Nullability of reference types in value of type 'System.Collections.Generic.List<string?>'
    // does not match target type 'System.Collections.Generic.List<string>'
    public List<string> SelectProductDescriptionsOld()
        => _products
            .Select(x => x?.Description)
            .ToList();

    // This method use a custom Select method to fix the nullability
    public List<string> SelectProductDescriptions()
        => _products
            .SelectNotNull(x => x?.Description)
            .ToList();
}
