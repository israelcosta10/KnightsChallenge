namespace KnightsChallenge.Entities.Core;

public abstract class Query<TAggregateId, TParams> (TAggregateId aggregateId, TParams parameters)
{
  public TAggregateId AggregateId { get; set; } = aggregateId;

  public TParams Parameters { get; set; } = parameters;
}