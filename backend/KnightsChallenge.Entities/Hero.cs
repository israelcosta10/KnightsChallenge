namespace KnightsChallenge.Entities;

public class Hero : Knight
{
  public static Hero Build (string name, string nickname, DateTime birth, List<Weapon> weapons, Attributes attributes, string keyAttribute)
  {
    return new Hero
    {
      Name = name,

      Nickname = nickname,

      Birth = birth,

      Weapons = weapons,

      Attributes = attributes,
      
      KeyAttribute = keyAttribute
    };
  }
}