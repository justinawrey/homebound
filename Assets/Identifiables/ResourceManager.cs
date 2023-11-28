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

  public static bool GetById<T>(int id, out T result) where T : IdentifiableSO
  {
    var list = _identifiables.Values.OfType<T>().Where(identifiable => identifiable.Id == id);
    if (list.Count() <= 0)
    {
      result = null;
      return false;
    }

    result = list.First();
    return true;
  }

  public static bool GetByName<T>(string name, out T result) where T : IdentifiableSO
  {
    var list = _identifiables.Values.OfType<T>().Where(identifiable => identifiable.Name == name);
    if (list.Count() <= 0)
    {
      result = null;
      return false;
    }

    result = list.First();
    return true;
  }

  public static bool GetAllOfType<T>(out T[] results) where T : IdentifiableSO
  {
    var list = _identifiables.Values.OfType<T>();
    if (list.Count() <= 0)
    {
      results = null;
      return false;
    }

    results = list.ToArray();
    return true;
  }
}