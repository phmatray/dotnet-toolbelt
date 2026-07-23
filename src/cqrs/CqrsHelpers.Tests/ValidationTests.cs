using CqrsHelpers.Abstractions;
using CqrsHelpers.Behaviors;
using CqrsHelpers.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace CqrsHelpers.Tests;

public class ValidationTests
{
    [Fact]
    public async Task Command_WithFailingValidation_ShouldThrowValidationException()
    {
        var services = new ServiceCollection();
        services.AddScoped<ICommandHandler<CreateProductCommand, int>, CreateProductCommandHandler>();
        services.AddScoped<IPipelineBehavior<ICommand<int>, int>, CreateProductCommandValidation>();
        services.AddScoped<IDispatcher, Dispatcher>();
        
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        
        var command = new CreateProductCommand { Name = "", Price = -10 };
        
        var exception = await Should.ThrowAsync<ValidationException>(async () => 
            await dispatcher.SendAsync(command));
        
        exception.Errors.ShouldContain("Product name is required");
        exception.Errors.ShouldContain("Price must be greater than zero");
    }
    
    [Fact]
    public async Task Command_WithPassingValidation_ShouldSucceed()
    {
        var services = new ServiceCollection();
        services.AddScoped<ICommandHandler<CreateProductCommand, int>, CreateProductCommandHandler>();
        services.AddScoped<IPipelineBehavior<ICommand<int>, int>, CreateProductCommandValidation>();
        services.AddScoped<IDispatcher, Dispatcher>();
        
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        
        var command = new CreateProductCommand { Name = "Valid Product", Price = 99.99m };
        
        var result = await dispatcher.SendAsync(command);
        
        result.ShouldBe(42);
    }
    
    public record CreateProductCommand : ICommand<int>
    {
        public required string Name { get; init; }
        public required decimal Price { get; init; }
    }
    
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
    {
        public Task<int> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(42);
        }
    }
    
    public class CreateProductCommandValidation : ValidationBehavior<ICommand<int>, int>
    {
        protected override Task<ValidationResult> ValidateAsync(ICommand<int> request, CancellationToken cancellationToken)
        {
            if (request is CreateProductCommand command)
            {
                var errors = new List<string>();
                
                if (string.IsNullOrWhiteSpace(command.Name))
                {
                    errors.Add("Product name is required");
                }
                
                if (command.Price <= 0)
                {
                    errors.Add("Price must be greater than zero");
                }
                
                return Task.FromResult(errors.Count > 0 
                    ? ValidationResult.Failure(errors.ToArray()) 
                    : ValidationResult.Success());
            }
            
            return Task.FromResult(ValidationResult.Success());
        }
    }
}