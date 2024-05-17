using KnightsChallenge.Entities;

namespace KnightsChallenge.Queries.Models;

public record KnightView (string Id, string Name, int Age, int Weapons, string Attribute, int Attack, double Exp)
{
  public static KnightView FromKnight (Knight knight) => new(Id: knight.Id, Name: knight.Name, Age: knight.Age,
    Weapons: knight.Weapons.Count,
    Attribute: knight.KeyAttribute, Attack: knight.Attack, Exp: knight.Exp);

  public static KnightView FromHero (Hero hero) => new(Id: hero.Id, Name: hero.Name, Age: hero.Age,
    Weapons: hero.Weapons.Count,
    Attribute: hero.KeyAttribute, Attack: hero.Attack, Exp: hero.Exp);
}