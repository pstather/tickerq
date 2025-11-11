using Microsoft.EntityFrameworkCore;
using TickerQ.Caching.StackExchangeRedis.DependencyInjection;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.DbContextFactory;
using TickerQ.EntityFrameworkCore.DependencyInjection;
using TickerQ.Instrumentation.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTickerQ(x =>
{
  x.ConfigureRequestJsonOptions(o =>
  {
    o.PropertyNameCaseInsensitive = true;
  });

  x.AddTickerQDiscovery([typeof(ClassLibrary1.TestSystem2).Assembly]);

  x.AddStackExchangeRedis(y =>
  {
    y.Configuration = "127.0.0.1:6379";
    y.NodeHeartbeatInterval = TimeSpan.FromMinutes(1);
  });
  x.AddOpenTelemetryInstrumentation();
  x.AddDashboard(y =>
  {
    y.WithNoAuth();
    y.SetBasePath("/background");
    y.ConfigureDashboardJsonOptions(o =>
    {
      o.PropertyNameCaseInsensitive = true;
    });
  });
  x.AddOperationalStore(y =>
  {
    x.ConfigureRequestJsonOptions(o =>
    {
      o.PropertyNameCaseInsensitive = true;
    });

    y.SetDbContextPoolSize(34);
    y.UseTickerQDbContext<TickerQDbContext>(z =>
    {
      z.UseSqlite("Data Source=test.db", o =>
      {
        o.MigrationsAssembly("WebApplication3");
      });
    });
  });
  x.ConfigureScheduler(y =>
  {
    y.SchedulerTimeZone = TimeZoneInfo.Local;
  });
});

var app = builder.Build();

app.UseTickerQ();

app.MapGet("/", () => "Hello World!");

app.Run();