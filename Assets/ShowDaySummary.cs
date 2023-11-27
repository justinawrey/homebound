using System;
using System.Collections;
using Febucci.UI.Core;
using Febucci.UI.Core.Parsing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowDaySummary : MonoBehaviour
{
    [SerializeField] private SceneSettingsSO _nextScene;
    [SerializeField] private DayStats _dayStats;
    [SerializeField] private TextMeshProUGUI _enemiesSlainText;
    [SerializeField] private TextMeshProUGUI _moneyAcquiredText;
    [SerializeField] private TextMeshProUGUI _levelsAcquiredText;
    [SerializeField] private TextMeshProUGUI _proceedText;
    [SerializeField] private TypewriterCore _proceedTypewriter;
    [SerializeField] private Image _bg;
    [SerializeField] private float _bgSlideDuration;

    private void OnEnable()
    {
        _proceedTypewriter.onMessage.AddListener(OnTypewriterMessage);

        StartCoroutine(LerpBg());
        _enemiesSlainText.text = $"<waitfor=0.3>ENEMIES SLAIN: {_dayStats.EnemiesSlainInDay}";
        _moneyAcquiredText.text = $"<waitfor=0.6>DUBLOONS ACQUIRED: {_dayStats.MoneyAcquiredInDay}";
        _levelsAcquiredText.text = $"<waitfor=0.9>LEVELS GLEANED: {_dayStats.LevelsAcquiredInDay}";
    }

    private IEnumerator LerpBg()
    {
        float timeElapsed = 0;
        while (timeElapsed < _bgSlideDuration)
        {
            float t = timeElapsed / _bgSlideDuration;
            t = t * t * (3f - 2f * t);
            _bg.fillAmount = Mathf.Lerp(0, 1, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _bg.fillAmount = 1;
    }

    private void OnDisable()
    {
        _proceedTypewriter.onMessage.RemoveListener(OnTypewriterMessage);
    }

    private void OnTypewriterMessage(EventMarker eventMarker)
    {
        if (eventMarker.name == "countdown")
        {
            _nextScene.Load();
            // StartCoroutine(ToUpgradeSceneRoutine());
        }
    }

    // private IEnumerator ToUpgradeSceneRoutine()
    // {
    //     int seconds = 3;
    //     while (seconds > 0)
    //     {
    //         yield return new WaitForSeconds(1);
    //         seconds--;
    //         SetProceedSeconds(seconds);
    //     }

    //     _nextScene.Load();
    // }

    // private void SetProceedSeconds(int seconds)
    // {
    //     _proceedText.text = $"<waitfor=1.2><wave>PROCEEDING IN... 3<waitfor=1>2<waitfor=1>1<waitfor=1></wave><?countdown>";
    // }
}