namespace KnightsChallenge.Entities.Core;

public abstract class Command<TAggregateId> (TAggregateId aggregateId)
{
  public TAggregateId AggregateId { get; set; } = aggregateId;
}

public abstract class Command<TAggregateId, TPayload> (TAggregateId aggregateId, TPayload payload)
  : Command<TAggregateId>(aggregateId)
{
  public TPayload Payload { get; set; } = payload;
}