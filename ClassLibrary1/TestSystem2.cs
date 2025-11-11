namespace ClassLibrary1;

using System.Reflection;
using Microsoft.Extensions.Logging;
using TickerQ.Utilities.Base;
using TickerQ.Utilities.Enums;

public class TestSystem2
{
  private readonly ILogger<TestSystem2> _logger;

  public TestSystem2(ILogger<TestSystem2> logger)
  {
    _logger = logger;
  }

  [TickerFunction("Test3", TickerTaskPriority.Normal)]
  public Task Test3()
  {
    _logger.LogInformation("Test3");
    return Task.CompletedTask;
  }

  [TickerFunction("Test4", TickerTaskPriority.Normal)]
  public async Task Test4(TickerFunctionContext<Test4Input> context)
  {
    var functionName = MethodBase.GetCurrentMethod()!.Name;
    _logger.LogInformation("Delaying for {Seconds} seconds for function {FunctionName}", context.Request.SecondsTimeout, functionName);
    await Task.Delay(TimeSpan.FromSeconds(context.Request.SecondsTimeout));
    _logger.LogInformation("Input Value for {FUnctionName} is {InputValue}", functionName, context.Request.Input);
  }
}