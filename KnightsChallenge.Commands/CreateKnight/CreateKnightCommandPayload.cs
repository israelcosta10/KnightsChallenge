namespace KnightsChallenge.Commands.CreateKnight;

public record CreateKnightCommandPayload (
  string Name,
  string Nickname,
  string Birthday,
  List<WeaponPayload> Weapons,
  AttributesPayload Attributes,
  string KeyAttribute);