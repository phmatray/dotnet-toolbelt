using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CCrossThrowIf;
using System.Linq.Expressions;

namespace CCrossThrowIf.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(warmupCount: 3, iterationCount: 10)]
public class GuardBenchmarks
{
    private string _testString = "Hello, World!";
    private string? _nullString = null;
    private int _testInt = 42;
    
    [Benchmark(Baseline = true)]
    public void OldApi_StringValidation()
    {
        try
        {
            // Using expression-based API
            Expression<Func<string>> expression = () => _testString;
            // This would be the old API call:
            // ThrowIf.Argument.IsNullOrWhiteSpace(expression);
            
            // Simulating the expression compilation overhead
            var compiled = expression.Compile();
            var value = compiled();
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(_testString));
            }
        }
        catch
        {
            // Expected when validation fails
        }
    }
    
    [Benchmark]
    public string NewApi_StringValidation()
    {
        try
        {
            // Using new direct API
            return Guard.Against.NullOrWhiteSpace(_testString);
        }
        catch
        {
            // Expected when validation fails
            return string.Empty;
        }
    }
    
    [Benchmark]
    public void OldApi_NumericValidation()
    {
        try
        {
            // Using expression-based API
            Expression<Func<int>> expression = () => _testInt;
            
            // Simulating the expression compilation overhead
            var compiled = expression.Compile();
            var value = compiled();
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_testInt));
            }
        }
        catch
        {
            // Expected when validation fails
        }
    }
    
    [Benchmark]
    public int NewApi_NumericValidation()
    {
        try
        {
            // Using new direct API
            return Guard.Against.Negative(_testInt);
        }
        catch
        {
            // Expected when validation fails
            return 0;
        }
    }
    
    [Benchmark]
    public void OldApi_ChainedValidation()
    {
        try
        {
            // Old API requires separate validation and processing
            Expression<Func<string>> expr1 = () => _testString;
            // ThrowIf.Argument.IsNullOrWhiteSpace(expr1);
            
            var value = _testString.Trim();
            
            Expression<Func<string>> expr2 = () => value;
            // ThrowIf.Argument.IsNullOrEmpty(expr2);
            
            var result = value.ToUpperInvariant();
        }
        catch
        {
            // Expected when validation fails
        }
    }
    
    [Benchmark]
    public string NewApi_ChainedValidation()
    {
        try
        {
            // New API allows chaining
            return Guard.Against.NullOrWhiteSpace(_testString)
                .Trim()
                .ToUpperInvariant();
        }
        catch
        {
            // Expected when validation fails
            return string.Empty;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<GuardBenchmarks>();
    }
}