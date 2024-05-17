using System.ComponentModel.DataAnnotations.Schema;

namespace KnightsChallenge.Entities.Core;

public class Aggregate : Entity
{
  [NotMapped] public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

  public void CommitDomainEvent (DomainEvent domainEvent)
  {
    DomainEvents.Add(domainEvent);
  }

  public void PushDomainEvents()
  {
    DomainEvents = new List<DomainEvent>();
  }
}