namespace KnightsChallenge.Infraestructure.Database.Outbox;

public class OutboxMessage
{
  public Guid Id { get; set; } = Guid.NewGuid();

  public string Type { get; set; } = string.Empty;
  
  public string? Assembly { get; set; }

  public string Content { get; set; } = string.Empty;
  
  public DateTime OccurredOn { get; set; }
  
  public DateTime? ProcessedOn { get; set; }
  
  public string? Error { get; set; }
}