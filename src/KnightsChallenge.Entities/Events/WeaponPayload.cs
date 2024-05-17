namespace KnightsChallenge.Entities.Events;

public record WeaponPayload (string Id, string Name, int Mod, string Attr, bool Equipped);