using System;
using UnityEngine;

public enum LayerName
{
  BuildingBlocks,
  DamageDealer
}

public class LayerUtils
{
  private static string GetLayerString(LayerName name)
  {
    return Enum.GetName(typeof(LayerName), name);
  }

  public static int GetMask(LayerName name)
  {
    return LayerMask.GetMask(GetLayerString(name));
  }

  public static bool CompareLayer(GameObject gameObject, LayerName name)
  {
    return gameObject.layer == LayerMask.NameToLayer(GetLayerString(name));
  }
}