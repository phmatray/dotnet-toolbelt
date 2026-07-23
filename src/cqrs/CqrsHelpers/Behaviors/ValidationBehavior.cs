using CqrsHelpers.Abstractions;

namespace CqrsHelpers.Behaviors;

public abstract class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default)
    {
        var validationResult = await ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        return await next();
    }
    
    protected abstract Task<ValidationResult> ValidateAsync(TRequest request, CancellationToken cancellationToken);
}

public class ValidationResult
{
    public bool IsValid { get; init; }
    public List<string> Errors { get; init; } = new();
    
    public static ValidationResult Success() => new() { IsValid = true };
    public static ValidationResult Failure(params string[] errors) => new() { IsValid = false, Errors = errors.ToList() };
}

public class ValidationException : Exception
{
    public List<string> Errors { get; }
    
    public ValidationException(List<string> errors) : base("Validation failed")
    {
        Errors = errors;
    }
}