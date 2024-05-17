using KnightsChallenge.Entities.Core;

namespace KnightsChallenge.Entities;

public class Weapon : Entity
{
  public string Name { get; set; }
  
  public int Mod { get; set; }
  
  public string Attr { get; set; }
  
  public bool Equipped { get; set; }

  public static Weapon Build(string name, int mod, string attr, bool equipped)
  {
    return new Weapon
    {
      Name = name,

      Mod = mod,

      Attr = attr,

      Equipped = equipped
    };
  }
}