using System;
using System.Collections.Generic;
using ReactiveUnity;
using UnityEngine;

[Serializable]
public struct Placement
{
  public Vector3Int Position;
  public PlacementSO PlacementSO;

  public Placement(int i, int j, int k, PlacementSO placementSO)
  {
    Position = new Vector3Int(i, j, k);
    PlacementSO = placementSO;
  }
}

[CreateAssetMenu(fileName = "HouseBuildSO", menuName = "Assets/HouseBuildSO")]
public class HouseBuildSO : IdentifiableSO
{
  // key: LOCAL SPACE positions
  public ReactiveDict<Vector3Int, PlacementSO> Placements = new ReactiveDict<Vector3Int, PlacementSO>();

  // Serialize a list because we cant serialize a dictionary
  [SerializeField] private List<Placement> _placements = new List<Placement>();
  [SerializeField] private PlacementSO _baseBuildingBlock;

  private void OnValidate()
  {
    Placements.Clear();

    foreach (Placement placement in _placements)
    {
      Placements.Add(placement.Position, placement.PlacementSO);
    }
  }

  // This is for debug reasons.  In the future we may need a more robust system
  // for "blueprinting" house shapes, i.e. character select? lol
  [ContextMenu("Initialize a 3x3 house")]
  public void Init3x3()
  {
    Placements.Clear();
    _placements.Clear();

    int[] idxs = new int[] { -1, 0, 1 };
    for (int i = 0; i < idxs.Length; i++)
    {
      var idxi = idxs[i];

      for (int j = 0; j < idxs.Length; j++)
      {
        var idxj = idxs[j];

        for (int k = 0; k < idxs.Length; k++)
        {
          var idxk = idxs[k];
          _placements.Add(new Placement(idxi, idxj, idxk, _baseBuildingBlock));
        }
      }
    }
  }
}