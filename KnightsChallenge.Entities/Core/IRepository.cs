namespace KnightsChallenge.Entities.Core;

public interface IRepository<TAggregate> where TAggregate : Aggregate
{
  Task<TAggregate?> FindByIdAsync (string id);

  void Save (TAggregate aggregate);
  
  void Update (TAggregate aggregate);

  void Delete (TAggregate aggregate);
}