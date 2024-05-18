﻿using KnightsChallenge.Entities.Core;

namespace KnightsChallenge.Entities;

public class Attributes : Entity
{
  public int Strength { get; set; }
  
  public int Dexterity { get; set; }
  
  public int Constitution { get; set; }
  
  public int Intelligence { get; set; }
  
  public int Wisdom { get; set; }
  
  public int Charisma { get; set; }

  public static Attributes Build (int strength, int dexterity, int constitution, int intelligence, int wisdom,
    int charisma)
  {
    return new Attributes
    {
      Strength = strength,

      Dexterity = dexterity,

      Constitution = constitution,

      Intelligence = intelligence,

      Wisdom = wisdom,

      Charisma = charisma
    };
  }
}