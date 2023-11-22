using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ResourceManager
{
  private static Dictionary<int, IdentifiableSO> _identifiables = new Dictionary<int, IdentifiableSO>();

  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
  public static void LoadAllIdentifiablesIntoMemory()
  {
    IdentifiableSO[] identifiables = Resources.LoadAll<IdentifiableSO>("Identifiables");
    foreach (IdentifiableSO identifiable in identifiables)
    {
      _identifiables.Add(identifiable.Id, identifiable);
    }
  }

  public static T GetById<T>(int id) where T : IdentifiableSO
  {
    return _identifiables.Values.OfType<T>().Where(identifiable => identifiable.Id == id).First();
  }

  public static T GetByName<T>(string name) where T : IdentifiableSO
  {
    return _identifiables.Values.OfType<T>().Where(identifiable => identifiable.Name == name).First();
  }

  public static T[] GetAllOfType<T>() where T : IdentifiableSO
  {
    return _identifiables.Values.OfType<T>().ToArray();
  }
}