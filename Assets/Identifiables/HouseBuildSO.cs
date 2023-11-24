using System;
using System.Collections.Generic;
using ReactiveUnity;
using UnityEngine;

[Serializable]
public struct SerializablePlacement
{
  public Vector3Int Position;
  public PlacementSO PlacementSO;
  public Vector3 Orientation;

  public SerializablePlacement(int i, int j, int k, PlacementSO placementSO)
  {
    Position = new Vector3Int(i, j, k);
    PlacementSO = placementSO;
    Orientation = Vector3.up;
  }
}

public class Placement
{
  public PlacementSO PlacementSO;
  public Vector3 Orientation;

  public Quaternion Quaternion
  {
    get
    {
      return Quaternion.Euler(Orientation);
    }
  }

  public Placement(PlacementSO placementSO, Vector3 orientation)
  {
    PlacementSO = placementSO;
    Orientation = orientation;
  }
}

[CreateAssetMenu(fileName = "HouseBuildSO", menuName = "Assets/HouseBuildSO")]
public class HouseBuildSO : IdentifiableSO
{
  // key: LOCAL SPACE positions
  public ReactiveDict<Vector3Int, Placement> Placements = new ReactiveDict<Vector3Int, Placement>();

  // Serialize a list because we cant serialize a dictionary
  [SerializeField] private List<SerializablePlacement> _placements = new List<SerializablePlacement>();
  [SerializeField] private PlacementSO _baseBuildingBlock;

  private void OnValidate()
  {
    Placements.Clear();

    foreach (SerializablePlacement placement in _placements)
    {
      Placements.Add(placement.Position, new Placement(placement.PlacementSO, placement.Orientation));
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
          _placements.Add(new SerializablePlacement(idxi, idxj, idxk, _baseBuildingBlock));
        }
      }
    }
  }
}