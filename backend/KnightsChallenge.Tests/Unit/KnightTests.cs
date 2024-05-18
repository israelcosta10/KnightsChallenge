using KnightsChallenge.Entities;
using KnightsChallenge.Entities.Core;
using KnightsChallenge.Entities.Core.Errors;

namespace TestProject1.Unit;

public class OwnDateTimer : IDateTimer
{
  public DateTime Now => new DateTime(2024, 05, 17);
}

public class KnightTests
{
  [Fact]
  public void ShouldNotCreateWithInvalidBirth()
  {
    Assert.Throws<BadRequestError>(() =>
      Knight.Build("Knight name", "nick2093", "2004-07-10asdfasdfasf",
        [Weapon.Build("sword", 3, "strength", true),],
        Attributes.Build(0, 0, 0, 0, 0, 0),
        "strength", new OwnDateTimer()));
  }
  
  [Fact]
  public void ShouldNotCreateWithInvalidKeyAttribute()
  {
    Assert.Throws<BadRequestError>(() =>
      Knight.Build("Knight name", "nick2093", "2004-07-10T00:00:00.000Z",
        [Weapon.Build("sword", 3, "strength", true),],
        Attributes.Build(0, 0, 0, 0, 0, 0),
        "invalid_key_attribute", new OwnDateTimer()));
  }
  
  [Fact]
  public void ShouldReturnTheCorrectAge()
  {
    var knight = Knight.Build("Knight name", "nick2093", "2004-07-10T00:00:00.000Z",
      [Weapon.Build("sword", 3, "strength", true),],
      Attributes.Build(0, 0, 0, 0, 0, 0),
      "strength", new OwnDateTimer());
    
    Assert.Equal(19, knight.Age);
  }
  
  [Fact]
  public void ShouldReturnCorrectValueForExpIfKnightHasLessThanSeveYears()
  {
    var knight = Knight.Build("Knight name", "nick2093", "2022-02-02T00:00:00.000Z",
      [Weapon.Build("sword", 3, "strength", true),],
      Attributes.Build(0, 0, 0, 0, 0, 0),
      "strength", new OwnDateTimer());
    
    Assert.Equal(0, knight.Exp);
  }
  
  [Fact]
  public void ShouldReturnCorrectValueForExpIfKnightHasMoreThanSeveYears()
  {
    var knight = Knight.Build("Knight name", "nick2093", "2004-07-10T00:00:00.000Z",
      [Weapon.Build("sword", 3, "strength", true),],
      Attributes.Build(0, 0, 0, 0, 0, 0),
      "strength", new OwnDateTimer());
    
    Assert.Equal(1060, knight.Exp);
  }
  
  [Theory]
  [InlineData(0, 3, 11)]
  [InlineData(9, 3, 12)]
  [InlineData(11, 3, 13)]
  [InlineData(13, 3, 14)]
  [InlineData(15, 3, 14)]
  [InlineData(16, 3, 15)]
  [InlineData(19, 3, 16)]
  [InlineData(20, 3, 16)]
  public void ShouldReturnTheCorrectAttack(int keyAttr, int equippedWeaponMod, int result)
  {
    var knight = Knight.Build("Knight name", "nick2093", "2004-07-10T00:00:00.000Z",
      [Weapon.Build("sword", equippedWeaponMod, "strength", true),],
      Attributes.Build(keyAttr, 0, 0, 0, 0, 0),
      "strength", new OwnDateTimer());
    
    Assert.Equal(result, knight.Attack);
  }
}