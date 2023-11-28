using System.Collections;
using UnityEngine;

public class Build : MonoBehaviour
{
  [SerializeField] private PlayerStatsSO _playerStatsSO;
  [SerializeField] private PlacementSO _placementSO;
  [SerializeField] private Transform _placementContainer;

  private GhostObject _ghostObject;

  private void Start()
  {
    _ghostObject = TagUtils.FindWithTag(TagName.GhostObject).GetComponent<GhostObject>();
    StartCoroutine(TryPlaceActivePlacementRoutine());
  }

  //   private void StartBuildPhase(UpgradeSO[] upgrades)
  //   {
  //     EventBus.StartBuildPhase(upgrades);
  //     // TODO: just make this a scene transition
  //     // CustomInputManager.SetCurrActionMap(ActionMapName.BuildPhase);
  //     StartCoroutine(PlaceRoutine(upgrades));
  //   }

  //   private IEnumerator PlaceRoutine(UpgradeSO[] upgrades)
  //   {
  //     foreach (UpgradeSO upgrade in upgrades)
  //     {
  //       _ghostObject.StartPlacement(upgrade.UpgradePrefab);
  //       yield return new WaitUntil(() => _ghostObject.IsPlaced());
  //     }

  //     EventBus.EndBuildPhase();
  //   }

  private IEnumerator TryPlaceActivePlacementRoutine()
  {
    while (true)
    {
      _ghostObject.StartPlacement(_placementSO.Prefab);

      // placementPos is in world space
      yield return new WaitUntil(() => _ghostObject.IsPlaced());

      // now place! we need to place in local space
      Vector3 localPlacementPos = _placementContainer.InverseTransformPoint(_ghostObject.GetPrevPlacedPosition());
      Placement placement = new Placement(_placementSO, _ghostObject.GetPrevPlacedRotation());
      _playerStatsSO.HouseBuild.Placements.Add(Vector3Int.RoundToInt(localPlacementPos), placement);
    }
  }

  // TODO: this needs a better home.
  public void IncrementDay()
  {
    _playerStatsSO.CurrentDay.Value += 1;
  }
}