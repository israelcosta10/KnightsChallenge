using KnightsChallenge.Entities.Core;
using KnightsChallenge.Entities.Core.Errors;
using KnightsChallenge.Entities.Events;

namespace KnightsChallenge.Entities;

public class Knight : Aggregate
{
  public string Name { get; set; }

  public string Nickname { get; set; }

  public DateTime Birth { get; set; }

  public List<Weapon> Weapons { get; set; }

  public Attributes Attributes { get; set; }

  public string KeyAttribute { get; set; }

  private IDateTimer DateTimer { get; set; }

  public int Attack
  {
    get => GetAttack();
    private set { }
  }

  public double Exp
  {
    get => GetExp();
    private set { }
  }

  public int Age
  {
    get => GetAge();
    private set { }
  }

  public Knight (IDateTimer? dateTimer = null)
  {
    DateTimer = dateTimer ?? new DateTimer();
  }


  public static Knight Build (string name, string nickname, string birth, List<Weapon> weapons, Attributes attributes,
    string keyAttribute, IDateTimer? dateTimer = null)
  {
    if (!DateTime.TryParse(birth, out DateTime convertedBirth))
      throw new BadRequestError();

    var knight = new Knight(dateTimer);
    
    knight.Name = name;
    knight.Nickname = nickname;
    knight.Birth = convertedBirth;
    knight.Weapons = weapons;
    knight.Attributes = attributes;
    
    if (knight.Attributes.Keys.FirstOrDefault(k => k.ToLower() == keyAttribute) is null)
      throw new BadRequestError();
    
    knight.KeyAttribute = keyAttribute;

    return knight;
  }

  public void UpdateNickname (string nickname)
  {
    Nickname = nickname;
  }

  public void Delete()
  {
    CommitDomainEvent(new RemovedKnightEvent(Name, Nickname, Birth,
      Weapons.Select(w => new WeaponPayload(w.Id, w.Name, w.Mod, w.Attr, w.Equipped)).ToList(),
      new AttributesPayload(Attributes.Id, Attributes.Strength, Attributes.Dexterity, Attributes.Constitution,
        Attributes.Intelligence, Attributes.Wisdom, Attributes.Charisma), KeyAttribute));
  }

  public int GetAttack()
  {
    var equippedWeapon = Weapons.FirstOrDefault(w => w.Equipped);

    var keyAttrValue = Attributes.GetValueFromKey(KeyAttribute);
    var keyAttrValueConverted = int.Parse(keyAttrValue.ToString()!);

    int? mod = null;
    if (keyAttrValueConverted >= 0 && keyAttrValueConverted <= 8)
      mod = -2;
    if (keyAttrValueConverted >= 9 && keyAttrValueConverted <= 10)
      mod = -1;
    if (keyAttrValueConverted >= 11 && keyAttrValueConverted <= 12)
      mod = 0;
    if (keyAttrValueConverted >= 13 && keyAttrValueConverted <= 15)
      mod = +1;
    if (keyAttrValueConverted >= 16 && keyAttrValueConverted <= 18)
      mod = +2;
    if (keyAttrValueConverted >= 19 && keyAttrValueConverted <= 20)
      mod = +3;

    return 10 + (mod ?? 0) + (equippedWeapon?.Mod ?? 0);
  }

  public int GetAge()
  {
    DateTime today = DateTime.Today;
    int age = today.Year - Birth.Year;

    if (Birth.Date > today.AddYears(-age))
    {
      age--;
    }

    return age;
  }

  public double GetExp()
  {
    if (GetAge() < 7)
      return 0;

    return Math.Floor((GetAge() - 7) * Math.Pow(22, 1.45));
  }
}