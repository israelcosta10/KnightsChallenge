using KnightsChallenge.Entities.Core;
using KnightsChallenge.Infraestructure.Database.Outbox;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace KnightsChallenge.Infraestructure.Database;

public class UnitOfWork (IMongoClient mongoClient, IMongoCollection<OutboxMessage> collection) : IUnitOfWork
{
  private IClientSessionHandle _session { get; } = mongoClient.StartSession();

  private List<Action> Operations { get; } = [];
  
  private List<Aggregate> Aggregates { get; } = [];
  
  public IDisposable Session => _session;
  
  public void AddOperation(Action operation, Aggregate aggregate)
  {
    Operations.Add(operation);
    Aggregates.Add(aggregate);
  }

  public void CleanOperations()
  {
    Operations.Clear();
    Aggregates.Clear();
  }

  public async Task SaveChangesAsync(CancellationToken cancellationToken)
  {
    /**
     * No caso de um banco de dados utilizando replica sets, as operações begin commit do mongodb funcionam
     */
    // _session.StartTransaction();

    Operations.ForEach(o =>
    {
      o.Invoke();
    });

    var domainEvents = Aggregates.SelectMany(aggregate => aggregate.DomainEvents);
    var outboxMessages = domainEvents.Select(domainEvent =>
      new OutboxMessage
      {
        OccurredOn = DateTime.Now,

        Assembly = domainEvent.GetType().Assembly.FullName,

        Type = domainEvent.GetType().FullName,

        Content = JsonConvert.SerializeObject(domainEvent,
          new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }),
      });
    
    if (outboxMessages.Count() > 0)
      await collection.InsertManyAsync(outboxMessages);
    
    CleanOperations();

    // await _session.CommitTransactionAsync(cancellationToken);
  }
}