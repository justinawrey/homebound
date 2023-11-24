using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

// NOTE: this transform is required to be a child of the "placements container"
public class PlaceAdjacentToBuildingBlock : MonoBehaviour, IHoverable, ISelectable
{
  [SerializeField] private PlayerStatsSO _playerStatsSO;

  private GhostObject _ghostObject;
  private Vector3Int _cachedNormal;

  private void Start()
  {
    _ghostObject = TagUtils.FindWithTag(TagName.GhostObject).GetComponent<GhostObject>();
  }

  public void OnHoverEnter(RaycastHit hit)
  {
    Vector3 dir = hit.normal.normalized;
    _cachedNormal = Vector3Int.RoundToInt(dir);
    MoveGhostObject(_cachedNormal);
  }

  private void MoveGhostObject(Vector3Int normal)
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

  // its not great that im raycasting again, but this is hacktober baby
  // when the normal dir changes, treat that as a ghost object movement as well
  private void Update()
  {
    Vector2 rawMousePos = Mouse.current.position.ReadValue();
    Vector2 remappedMousePos = math.remap(
        Vector2.zero,
        new Vector2(Screen.width, Screen.height),
        Vector2.zero,
        ScreenUtils.ScreenResolution,
        rawMousePos
    );

    Ray ray = Camera.main.ScreenPointToRay(remappedMousePos);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerUtils.GetMask(LayerName.BuildingBlocks)))
    {
      // only do this for this game object!
      if (hit.collider.gameObject.GetInstanceID() != gameObject.GetInstanceID())
      {
        return;
      }

      Vector3Int roundedNormal = Vector3Int.RoundToInt(hit.normal.normalized);
      if (roundedNormal != _cachedNormal)
      {
        print("surface change");
        // we moved to a new face!
        _cachedNormal = roundedNormal;
        MoveGhostObject(_cachedNormal);
      }
    }
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