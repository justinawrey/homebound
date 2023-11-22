using System;
using System.Collections;
using UnityEngine;

public class DayEventsText : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    [SerializeField] private BlinkText _startDayText;
    [SerializeField] private BlinkText _endDayText;
    [SerializeField] private BlinkText _levelUpText;
    [SerializeField] private float _alertDuration = 1f;
    [SerializeField] private SceneSettingsSO _upgradeSceneSettings;

    private void Awake()
    {
        _startDayText.enabled = false;
        _endDayText.enabled = false;
        _levelUpText.enabled = false;

        EventBus.OnDayStart += () => StartCoroutine(AlertRoutine(_startDayText));
        EventBus.OnDayEnd += () => StartCoroutine(AlertRoutine(_endDayText, SwitchScenes));

        _playerStatsSO.Level.OnChange((_, __) => StartCoroutine(AlertRoutine(_levelUpText)));
    }

    private void SwitchScenes()
    {
        _upgradeSceneSettings.Load();
    }

    private void OnDestroy()
    {
        EventBus.OnDayStart -= () => StartCoroutine(AlertRoutine(_startDayText));
        EventBus.OnDayEnd -= () => StartCoroutine(AlertRoutine(_endDayText));
    }

    private IEnumerator AlertRoutine(BlinkText text, Action cb = null)
    {
        text.enabled = true;
        yield return new WaitForSeconds(_alertDuration);
        text.enabled = false;

        if (cb != null)
        {
            cb();
        }
    }
}
