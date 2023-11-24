using UnityEngine;

// NOTE: this transform is required to be a child of the "placements container"
public class PlaceAdjacentToBuildingBlock : MonoBehaviour, IHoverable, ISelectable
{
  [SerializeField] private PlayerStatsSO _playerStatsSO;
  private GhostObject _ghostObject;

  private void Start()
  {
    _ghostObject = TagUtils.FindWithTag(TagName.GhostObject).GetComponent<GhostObject>();
  }

  public void OnHoverEnter(RaycastHit hit)
  {
    Vector3 dir = hit.normal.normalized;
    Vector3 desiredPosWorld = transform.position + dir;
    Vector3Int desiredPosLocal = Vector3Int.RoundToInt(transform.parent.InverseTransformPoint(desiredPosWorld));
    bool spotOccupied = _playerStatsSO.HouseBuild.Placements.ContainsKey(desiredPosLocal);

    _ghostObject.SetGhostObjectPosition(desiredPosWorld);
    _ghostObject.AlignGhostObjectRotationToNormal(dir);
    if (spotOccupied)
    {
      _ghostObject.HideGhostObject();
    }
    else
    {
      _ghostObject.ShowGhostObject();
    }
  }

  // no-op
  public void OnHoverExit(RaycastHit hit) { }

  public void OnSelect(RaycastHit hit)
  {
    Vector3 ghostObjectPosWorld = _ghostObject.GetGhostObjectPosition();
    Vector3Int ghostObjectPosLocal = Vector3Int.RoundToInt(transform.parent.InverseTransformPoint(ghostObjectPosWorld));
    bool spotOccupied = _playerStatsSO.HouseBuild.Placements.ContainsKey(ghostObjectPosLocal);

    if (!spotOccupied)
    {
      _ghostObject.EndPlacement();
    }
  }
}