namespace KnightsChallenge.Entities.Core;

public record DomainEvent
{
  public Guid Id { get; private set; } = Guid.NewGuid();

  public DateTime At { get; private set; } = DateTime.Now;
}