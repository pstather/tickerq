namespace WebApplication3;

using System.Reflection;
using TickerQ.Utilities.Base;
using TickerQ.Utilities.Enums;

public class Test
{
  private readonly ILogger<Test> _logger;

  public Test(ILogger<Test> logger)
  {
    _logger = logger;
  }

  [TickerFunction("Test1", TickerTaskPriority.Normal)]
  public Task Test1()
  {
    _logger.LogInformation("Test1");
    return Task.CompletedTask;
  }

  [TickerFunction("Test2", TickerTaskPriority.Normal)]
  public async Task Test2(TickerFunctionContext<Test2Input> context)
  {
    _logger.LogInformation("Delaying for {Seconds} seconds for function {FunctionName}", context.Request.SecondsTimeout, context.FunctionName);
    await Task.Delay(TimeSpan.FromSeconds(context.Request.SecondsTimeout));
    _logger.LogInformation("Input Message for {FunctionName} is: {Message}", context.FunctionName, context.Request.Input);
  }
}