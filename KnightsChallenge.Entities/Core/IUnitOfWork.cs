namespace KnightsChallenge.Entities.Core;

public interface IUnitOfWork
{
  IDisposable Session { get; }

  void AddOperation (Action operation, Aggregate aggregate);

  void CleanOperations ();
  
  Task SaveChangesAsync(CancellationToken cancellationToken);
}