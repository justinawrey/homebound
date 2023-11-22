using System;
using System.Reflection;
using UnityEngine;

public class IdentifiableSO : ScriptableObject, Persistable<string>
{
  public int Id { get { return GetInstanceID(); } }
  public string Name;
  [TextArea(10, 100)] public string Description;

  public void SaveToDisk(Type t)
  {
    // Collect all save data from attributes.
    // Using reflection.
    FieldInfo[] infos = t.GetFields();
    foreach (var info in infos)
    {
      if (Attribute.IsDefined(info, typeof(SaveData)))
      {
        // do some saving....
        // TODO: finish this...
      };
    }
  }

  public void ReloadFromDisk(Type t)
  {
  }

  public string GetPersistedVal()
  {
    throw new NotImplementedException();
  }

  public void SetPersistedVal(string to)
  {
    throw new NotImplementedException();
  }
}