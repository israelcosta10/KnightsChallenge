using KnightsChallenge.WebApi.Extensions;

namespace KnightsChallenge.WebApi;

public abstract class Program
{
  public static void Main (string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    
    builder
      .ConfigureOpenTelemetry();

    var startup = new Startup();
    startup
      .ConfigureServices(builder.Services);
    
    var app = builder.Build();
    startup.Configure(app, builder.Environment);
    app
      .Run();
  }
}