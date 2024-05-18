using System.Reflection;
using KnightsChallenge.Infraestructure.Database.Outbox;
using MassTransit;
using MongoDB.Driver;
using Newtonsoft.Json;
using Polly;
using Quartz;
using Serilog;

namespace KnightsChallenge.Infraestructure.Jobs;

public class ProcessOutboxMessagesJob (IMongoCollection<OutboxMessage> collection, ILogger logger, IBus bus) : IJob
{
  public async Task Execute (IJobExecutionContext context)
  {
    var messages = await collection.Find(message => message.ProcessedOn == null).Limit(20).ToListAsync();

    foreach (var message in messages)
    {
      var domainEventType = Assembly.Load(message.Assembly).GetType(message.Type);

      if (domainEventType is null)
      {
        logger.Error($"Error trying to get type {message.Type}");
        continue;
      }

      var domainEvent = JsonConvert.DeserializeObject(message.Content, domainEventType);

      if (domainEvent is null)
      {
        logger.Error($"Error when processing the outbox message: {message.Content}");
        continue;
      }

      var policy = Policy.Handle<Exception>()
        .WaitAndRetryAsync(3, attemp => TimeSpan.FromMilliseconds(50 * attemp));

      var result = await policy.ExecuteAndCaptureAsync(() => bus.Publish(domainEvent, domainEventType));

      message.Error = result.FinalException?.ToString();
      message.ProcessedOn = DateTime.Now;

      await collection.ReplaceOneAsync(_message => _message.Id == message.Id, message);
    }
  }
}