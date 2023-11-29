using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpatialEffect
{
  public Vector3Int Offset;
  public List<ModSO> Mods;
}

[CreateAssetMenu(fileName = "SpatialEffectPlacementSO", menuName = "Assets/Placements/SpatialEffectPlacementSO")]
public class SpatialEffectPlacementSO : PlacementSO
{
  public List<SpatialEffect> SpatialEffects;
}