using System.Collections;
using UnityEngine;

public class Day : MonoBehaviour
{
    [SerializeField] private float _realtimeSecondsPerDay = 30f;

    private void Start()
    {
        StartCoroutine(DayRoutine());
    }

    private IEnumerator DayRoutine()
    {
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
    }
}
