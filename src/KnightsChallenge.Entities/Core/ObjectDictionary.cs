using System.Reflection;
using KnightsChallenge.Entities.Core.Errors;

namespace KnightsChallenge.Entities.Core;

public abstract class ObjectDictionary
{
  public string[] Keys
  {
    get
    {
      PropertyInfo[] props = GetType().GetProperties();
      string[] keys = new string[props.Length];

      for (int i = 0; i < props.Length; i++)
      {
        keys[i] = props[i].Name;
      }

      return keys;
    }
  }

  public object GetValueFromKey (string key)
  {
    PropertyInfo? prop = GetType().GetProperty(Char.ToUpper(key[0]) + key.Substring(1));
        
    if (prop != null && prop.GetValue(this) != null)
    {
      return prop.GetValue(this);
    }
    else
    {
      throw new InternalServerError($"Property '{key}' doesn't exists");
    }
  }
}