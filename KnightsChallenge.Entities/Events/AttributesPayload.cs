namespace KnightsChallenge.Entities.Events;

public record AttributesPayload (
  string Id,
  int Strength,
  int Dexterity,
  int Constitution,
  int Intelligence,
  int Wisdom,
  int Charisma);