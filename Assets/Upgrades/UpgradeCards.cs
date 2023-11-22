using System;
using System.Collections;
using UnityEngine;

public class UpgradeCards : MonoBehaviour
{
    [SerializeField] private float _staggerTime = 0.2f;
    [SerializeField] private UpgradeCard _upgradeCard1;
    [SerializeField] private UpgradeCard _upgradeCard2;
    [SerializeField] private UpgradeCard _upgradeCard3;

    private UpgradeSO[] _upgradeSOs;

    public static event Action<UpgradeSO[]> OnUpgradePhaseEnd;

    private void Awake()
    {
        EventBus.OnDayEnd += StartUpgradePhase;
        _upgradeSOs = Resources.LoadAll<UpgradeSO>("UpgradeSOs");
    }

    private void OnDestroy()
    {
        EventBus.OnDayEnd += StartUpgradePhase;
    }

    public void StartUpgradePhase()
    {
        // TODO: this is just going to be part of the build scene
        // CustomInputManager.SetCurrActionMap(ActionMapName.UpgradePhase);
        StartCoroutine(StartUpgradeRoutine());
    }

    private IEnumerator StartUpgradeRoutine()
    {
        InitializeUpgradeCard(_upgradeCard1, () => EndUpgradePhase(new UpgradeCard[] { _upgradeCard2, _upgradeCard3 }, _upgradeCard1));
        yield return new WaitForSeconds(_staggerTime);
        InitializeUpgradeCard(_upgradeCard2, () => EndUpgradePhase(new UpgradeCard[] { _upgradeCard1, _upgradeCard3 }, _upgradeCard2));
        yield return new WaitForSeconds(_staggerTime);
        InitializeUpgradeCard(_upgradeCard3, () => EndUpgradePhase(new UpgradeCard[] { _upgradeCard1, _upgradeCard2 }, _upgradeCard3));
    }

    private void InitializeUpgradeCard(UpgradeCard upgradeCard, Action selectedCb)
    {
        UpgradeSO upgrade = ChooseRandomUpgrade();
        upgradeCard.InitializeUpgrade(upgrade, selectedCb);
    }

    private void EndUpgradePhase(UpgradeCard[] cardsToStaggerOut, UpgradeCard selected)
    {
        // Once this is called, disable the upgrade action map so we cant spam click the cards lol
        CustomInputManager.DisableCurrActionMap();
        StartCoroutine(EndUpgradePhaseRoutine(cardsToStaggerOut, selected));
    }

    private IEnumerator EndUpgradePhaseRoutine(UpgradeCard[] cardsToStaggerOut, UpgradeCard selected)
    {
        foreach (UpgradeCard upgradeCard in cardsToStaggerOut)
        {
            upgradeCard.TweenOut();
            yield return new WaitForSeconds(_staggerTime);
        }

        selected.TweenToCenterAndOut(() =>
        {
            EventBus.EndUpgradePhase(new UpgradeSO[] { selected.Upgrade });
        });
    }

    private UpgradeSO ChooseRandomUpgrade()
    {
        int randomIdx = UnityEngine.Random.Range(0, _upgradeSOs.Length);
        return _upgradeSOs[randomIdx];
    }
}
