using System.Numerics;
using CCrossThrowIf;

namespace CCrossThrowIf.Demo;

public class GuardDemo
{
    public void DemonstrateNewApi()
    {
        // Example 1: Traditional validation (old way with expressions)
        // ThrowIf.Argument.IsNullOrWhiteSpace(() => username);
        
        // Example 2: New API - returns the validated value
        string username = null!;
        string validatedUsername = Guard.Against.NullOrWhiteSpace(username);
        // validatedUsername is guaranteed to be non-null and non-whitespace
        
        // Example 3: Method chaining with return values
        var processedValue = Guard.Against.NullOrWhiteSpace(GetUserInput())
            .Trim()
            .ToUpperInvariant();
        
        // Example 4: Fluent extension methods
        var email = GetEmail()
            .GuardNullOrWhiteSpace()
            .ToLowerInvariant();
        
        // Example 5: Numeric guards with generic math
        int quantity = -5;
        var validQuantity = Guard.Against.Negative(quantity); // throws
        
        // Example 6: Range validation
        var percentage = Guard.Against.OutOfRange(inputValue, 0, 100);
        
        // Example 7: Constructor parameter validation with return
        IRepository myRepository = null!; // Would come from DI
        string myConnectionString = ""; // Would come from config
        var service = new MyService(
            Guard.Against.Null(myRepository),
            Guard.Against.NullOrEmpty(myConnectionString)
        );
    }
    
    public class MyService
    {
        private readonly IRepository _repository;
        private readonly string _connectionString;
        
        public MyService(IRepository repository, string connectionString)
        {
            // Old way with expressions:
            // ThrowIf.Argument.IsNull(() => repository);
            // ThrowIf.Argument.IsNullOrEmpty(() => connectionString);
            
            // New way - validates and assigns in one line:
            _repository = Guard.Against.Null(repository);
            _connectionString = Guard.Against.NullOrEmpty(connectionString);
        }
        
        public async Task<User?> GetUserAsync(int userId, CancellationToken cancellationToken)
        {
            // Validate and use the value
            var validUserId = Guard.Against.NegativeOrZero(userId);
            
            return await _repository.GetUserAsync(validUserId, cancellationToken);
        }
    }
    
    // Example with generic math constraints (C# 11 / .NET 7+)
    public T CalculateAverage<T>(T sum, T count) where T : INumber<T>
    {
        // Ensures count is not zero and returns it for use
        var validCount = Guard.Against.Zero(count);
        
        return sum / validCount;
    }
    
    // Dummy implementations for demo
    private string GetUserInput() => "test";
    private string GetEmail() => "user@example.com";
    private int inputValue = 50;
    
    public interface IRepository
    {
        Task<User?> GetUserAsync(int userId, CancellationToken cancellationToken);
    }
    
    public record User(int Id, string Name);
}