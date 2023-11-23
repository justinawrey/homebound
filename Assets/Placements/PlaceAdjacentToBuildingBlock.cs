using UnityEngine;

public class PlaceAdjacentToBuildingBlock : MonoBehaviour, IHoverable, ISelectable
{
  private GhostObject _ghostObject;

  private void Start()
  {
    _ghostObject = TagUtils.FindWithTag(TagName.GhostObject).GetComponent<GhostObject>();
  }

  public void OnHoverEnter(RaycastHit hit)
  {
    Vector3 dir = hit.normal.normalized;
    _ghostObject.SetGhostObjectPosition(transform.position + dir);
  }

  // no-op
  public void OnHoverExit(RaycastHit hit) { }

  public void OnSelect(RaycastHit hit)
  {
    _ghostObject.EndPlacement();
  }
}