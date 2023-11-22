using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class BuildPhaseCursorRaycast : MonoBehaviour
{
  private Transform _homeGridTransform;
  private HomeGrid _homeGrid;
  private GhostObject _ghostObject;
  private Vector3 _activeWorldPos;
  private Vector3Int _activeGridPos;
  private Quaternion _activeGridRot;

  private void Awake()
  {
    // CustomInputManager.SubscribeToAction(ActionMapName.BuildPhase, ActionName.Select, GlueObject);
  }

  private void Start()
  {
    GameObject homeObject = TagUtils.FindWithTag(TagName.House);
    _homeGridTransform = homeObject.transform;
    _homeGrid = homeObject.GetComponent<HomeGrid>();
    _ghostObject = TagUtils.FindWithTag(TagName.GhostObject).GetComponent<GhostObject>();
  }

  public void OnBeforeNextSceneLoad()
  {
    // CustomInputManager.UnsubscribeFromAction(ActionMapName.BuildPhase, ActionName.Select, GlueObject);
  }

  // private void GlueObject(CallbackContext context)
  // {
  //   _homeGrid.Add(_activeGridPos, _ghostObject.GetPendingObject(), _activeGridRot, _activeWorldPos);
  //   _ghostObject.EndPlacement();
  // }

  // TODO: THIS IS JANK SEE OTHER TODO
  // private void Update()
  // {
  //   Vector2 remappedMousePos = MouseUtils.GetMouseScreenPos();
  //   Ray ray = Camera.main.ScreenPointToRay(remappedMousePos);
  //   RaycastHit hit;
  //   if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerUtils.GetMask(LayerName.BuildingBlocks)))
  //   {
  //     Vector3 newWorldPos = hit.collider.gameObject.transform.position + hit.normal;
  //     Vector3Int newGridPos = Vector3Int.RoundToInt(_homeGrid.GetPosition(hit.collider.gameObject) + hit.normal);

  //     if (!_homeGrid.IsOccupied(newGridPos))
  //     {
  //       _activeWorldPos = newWorldPos;
  //       _activeGridPos = newGridPos;
  //       _activeGridRot = Quaternion.identity;
  //       _ghostObject.SetGhostObjectPosition(_activeWorldPos, _activeGridRot);
  //     }
  //   }
  // }
}