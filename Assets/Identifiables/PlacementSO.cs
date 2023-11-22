using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlacementSO", menuName = "Assets/Placements/PlacementSO")]
public class PlacementSO : IdentifiableSO
{
  public GameObject Prefab;
  public List<ModSO> Mods;
}