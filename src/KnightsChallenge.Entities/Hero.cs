namespace KnightsChallenge.Entities;

public class Hero : Knight
{
  public DateTime BecameAHeroIn { get; set; }
  
  public static Hero Build (string name, string nickname, DateTime birth, List<Weapon> weapons, Attributes attributes)
  {
    return new Hero
    {
      Name = name,

      Nickname = nickname,

      Birth = birth,

      Weapons = weapons,

      Attributes = attributes
    };
  }
}