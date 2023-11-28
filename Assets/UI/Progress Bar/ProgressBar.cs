using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _marker;
    [SerializeField] private float _rightOffset = 5f;

    private RectTransform _markerRt;
    private float _progressBarWidth;
    private Action _SetProgressToZero;
    private Action _SetProgressToOne;

    private void Awake()
    {
        _progressBarWidth = GetComponent<RectTransform>().rect.width;
        _markerRt = _marker.GetComponent<RectTransform>();
        _SetProgressToZero = () => SetProgress(0);
        _SetProgressToOne = () => SetProgress(1);

        EventBus.OnDayStart += _SetProgressToZero;
        EventBus.OnDayEnd += _SetProgressToOne;
        EventBus.OnDayTick += SetProgress;
    }

    private void OnDestroy()
    {
        EventBus.OnDayStart -= _SetProgressToZero;
        EventBus.OnDayEnd -= _SetProgressToOne;
        EventBus.OnDayTick -= SetProgress;
    }

    public void SetProgress(float percent)
    {
        float xPos = Mathf.Lerp(0, _progressBarWidth - _rightOffset, percent);
        _markerRt.anchoredPosition = new Vector2(xPos, _markerRt.anchoredPosition.y);
    }
}
