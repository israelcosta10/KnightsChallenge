using KnightsChallenge.Entities.Core;

namespace KnightsChallenge.Entities.Events;

public record RemovedKnightEvent (
  string Name,
  string Nickname,
  DateTime Birth,
  List<WeaponPayload> Weapons,
  AttributesPayload Attributes,
  string KeyAttribute) : DomainEvent;