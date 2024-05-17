using KnightsChallenge.Entities.Core;
using KnightsChallenge.Infraestructure.Database.Outbox;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace KnightsChallenge.Infraestructure.Database;

public class UnitOfWork (IMongoClient mongoClient) : IUnitOfWork
{
  private IClientSessionHandle _session { get; } = mongoClient.StartSession();

  private readonly IMongoCollection<OutboxMessage> _outboxMessagesCollection =
    mongoClient.GetDatabase("MONGO_DB_CONNECTION_DATABASE").GetCollection<OutboxMessage>("messages");

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
  }

  public async Task SaveChangesAsync(CancellationToken cancellationToken)
  {
    _session.StartTransaction();

    Operations.ForEach(o =>
    {
      o.Invoke();
    });

    var domainEvents = Aggregates.SelectMany(aggregate => aggregate.DomainEvents);
    _outboxMessagesCollection.InsertMany(
      domainEvents.Select(domainEvent =>
        new OutboxMessage
        {
          OccurredOn = DateTime.Now,

          Assembly = domainEvent.GetType().Assembly.FullName,
          
          Type = domainEvent.GetType().FullName,

          Content = JsonConvert.SerializeObject(domainEvent,
            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }),
        }));

    await _session.CommitTransactionAsync(cancellationToken);

    CleanOperations();
  }
}