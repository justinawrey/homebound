using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Day : MonoBehaviour
{
    [SerializeField] private float _realtimeSecondsPerDay = 30f;
    [SerializeField] private MMF_Player _feedbacksPlayer;
    [SerializeField] private DayStats _dayStats;

    private void Start()
    {
        StartCoroutine(DayRoutine());
    }

    private IEnumerator DayRoutine()
    {
        _dayStats.ResetDayStats();
        EventBus.StartDay();

        float currSeconds = 0;
        while (currSeconds < _realtimeSecondsPerDay)
        {
            float percentDayComplete = Mathf.InverseLerp(0, _realtimeSecondsPerDay, currSeconds);
            EventBus.TickDay(percentDayComplete);
            currSeconds += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        EventBus.EndDay();
        _feedbacksPlayer.PlayFeedbacks();
    }
}
