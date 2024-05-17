namespace KnightsChallenge.Entities.Core;

public class Entity : ObjectDictionary
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
}