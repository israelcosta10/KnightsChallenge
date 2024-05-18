using KnightsChallenge.Commands.CreateKnight;
using KnightsChallenge.Entities;
using KnightsChallenge.Entities.Core;
using KnightsChallenge.Events.Consumers;
using KnightsChallenge.Infraestructure.Database;
using KnightsChallenge.Infraestructure.Database.Outbox;
using KnightsChallenge.Infraestructure.Jobs;
using KnightsChallenge.Infraestructure.Repository;
using KnightsChallenge.Infraestructure.Repository.Contracts;
using KnightsChallenge.Queries.GetKnight;
using KnightsChallenge.WebApi.Middlewares;
using MassTransit;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Quartz;
using Serilog;
using ILogger = Serilog.ILogger;

namespace KnightsChallenge.WebApi;

public class Startup
{
  public void ConfigureServices (IServiceCollection services)
  {
    var connectionString = Environment.GetEnvironmentVariable("MONGO_DB_CONNECTION_URI");
    var databaseName = Environment.GetEnvironmentVariable("MONGO_DB_CONNECTION_DATABASE");
    var client = new MongoClient(connectionString);

    services.AddSingleton<IMongoClient>(client);
    services.AddTransient(typeof(IMongoCollection<Knight>),
      (sp) => client.GetDatabase(databaseName).GetCollection<Knight>("knights"));
    services.AddTransient(typeof(IMongoCollection<Hero>),
      (sp) => client.GetDatabase(databaseName).GetCollection<Hero>("heroes"));
    services.AddTransient(typeof(IMongoCollection<OutboxMessage>),
      (sp) => client.GetDatabase(databaseName).GetCollection<OutboxMessage>("messages"));

    services.AddSingleton<IUnitOfWork, UnitOfWork>();

    var logger = new LoggerConfiguration().WriteTo.OpenTelemetry().CreateLogger();
    services.AddSingleton<ILogger>(logger);

    services.AddQuartz(configure =>
    {
      var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

      configure
        .AddJob<ProcessOutboxMessagesJob>(jobKey)
        .AddTrigger(trigger =>
          trigger.ForJob(jobKey)
            .WithSimpleSchedule(schedule =>
              schedule.WithIntervalInSeconds(20)
                .RepeatForever()));
    });

    services.AddQuartzHostedService();

    services.AddTransient<IKnightRepository, KnightRepository>();
    services.AddTransient<IHeroRepository, HeroRepository>();

    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateKnightCommand)));
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetKnightQuery)));

    services.AddMassTransit(x =>
    {
      x.AddConsumers(typeof(RemovedKnightEventConsumer).Assembly);

      x.UsingRabbitMq((ctx, conf) =>
      {
        conf.Host(Environment.GetEnvironmentVariable("RABBITMQ_HOSTNAME"), "/", h =>
        {
          h.Username(Environment.GetEnvironmentVariable("RABBITMQ_USERNAME")!);
          h.Password(Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")!);
        });

        conf.ConfigureEndpoints(ctx);
      });
    });

    services.AddControllers();
    services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc(
        "knights",
        new OpenApiInfo
        {
          Title = "Knights API",
          Version = "v1"
        }
      );
    });
    services.AddCors(
      options =>
      {
        options.AddDefaultPolicy(
          policy =>
          {
            policy
              .AllowAnyHeader()
              .AllowAnyOrigin()
              .AllowAnyMethod();
          }
        );
      }
    );
  }

  public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }

    app.UseCors();

    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
      config.SwaggerEndpoint("/swagger/knights/swagger.json", "Knights API");
      config.RoutePrefix = "docs";
    });

    app.UseRouting();
    app.UseMiddleware<GlobalExceptionMiddleware>();
    app.UseEndpoints(endpoints => endpoints.MapControllers());
  }
}