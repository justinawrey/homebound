using System.Collections;
using UnityEngine;

public class Build : MonoBehaviour
{
  [SerializeField] private GhostObject _ghostObject;

  private void Awake()
  {
    EventBus.OnUpgradePhaseEnd += StartBuildPhase;
  }

  private void OnDestroy()
  {
    EventBus.OnUpgradePhaseEnd -= StartBuildPhase;
  }

  private void StartBuildPhase(UpgradeSO[] upgrades)
  {
    EventBus.StartBuildPhase(upgrades);
    // TODO: just make this a scene transition
    // CustomInputManager.SetCurrActionMap(ActionMapName.BuildPhase);
    StartCoroutine(PlaceRoutine(upgrades));
  }

  private IEnumerator PlaceRoutine(UpgradeSO[] upgrades)
  {
    foreach (UpgradeSO upgrade in upgrades)
    {
      _ghostObject.StartPlacement(upgrade.UpgradePrefab);
      yield return new WaitUntil(() => _ghostObject.IsPlaced());
    }

    EventBus.EndBuildPhase();
  }
}