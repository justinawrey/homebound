using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

// NOTE: this transform is required to be a child of the "placements container"
public class PlaceAdjacentToBuildingBlock : MonoBehaviour, IHoverable, ISelectable
{
  [SerializeField] private PlayerStatsSO _playerStatsSO;

  private GhostObject _ghostObject;
  private Transform _placementContainer;
  private Vector3 _cachedNormal;

  private void Start()
  {
    _ghostObject = TagUtils.FindWithTag(TagName.GhostObject).GetComponent<GhostObject>();

    // TODO: not the greatest but fine
    _placementContainer = TagUtils.FindWithTag(TagName.House).transform.GetChild(0);
    // EventBus.OnHouseRotationEnd += Snap;
  }

  // private void OnDestroy()
  // {
  // EventBus.OnHouseRotationEnd -= Snap;
  // }

  // private void Snap()
  // {
  //   MoveGhostObject(_cachedNormal);
  // }

  private bool IsValidNormal(Vector3 localSpaceNormal)
  {
    // print(localSpaceNormal);
    // print(Vector3.forward);
    // return localSpaceNormal.Approximately(Vector3.forward);
    return localSpaceNormal == Vector3.forward;
  }

  public void OnHoverEnter(RaycastHit hit)
  {
    _cachedNormal = hit.normal.normalized;

    // only allow placing ghost object in the local space forward direction
    if (!IsValidNormal(_placementContainer.InverseTransformDirection(_cachedNormal)))
    {
      return;
    }

    // _cachedNormal = Vector3Int.RoundToInt(dir);
    MoveGhostObject(_cachedNormal);
  }

  private void MoveGhostObject(Vector3 normal)
  {
    Vector3 desiredPosWorld = transform.position + normal;
    Vector3Int desiredPosLocal = Vector3Int.RoundToInt(transform.parent.InverseTransformPoint(desiredPosWorld));
    bool spotOccupied = _playerStatsSO.HouseBuild.Placements.ContainsKey(desiredPosLocal);

    _ghostObject.SetGhostObjectPosition(desiredPosWorld);
    _ghostObject.AlignGhostObjectRotationToNormal(normal);
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
  public void OnHoverExit(RaycastHit hit)
  {
    _ghostObject.HideGhostObject();
  }

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