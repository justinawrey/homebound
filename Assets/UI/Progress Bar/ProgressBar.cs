using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _marker;

    private RectTransform _markerRt;
    private float _progressBarWidth;

    private void Awake()
    {
        _progressBarWidth = GetComponent<RectTransform>().rect.width;
        _markerRt = _marker.GetComponent<RectTransform>();
        EventBus.OnDayStart += () => SetProgress(0);
        EventBus.OnDayEnd += () => SetProgress(1);
        EventBus.OnDayTick += SetProgress;
    }

    private void OnDestroy()
    {
        EventBus.OnDayStart -= () => SetProgress(0);
        EventBus.OnDayEnd -= () => SetProgress(1);
        EventBus.OnDayTick -= SetProgress;
    }

    public void SetProgress(float percent)
    {
        float xPos = Mathf.Lerp(0, _progressBarWidth, percent);
        _markerRt.anchoredPosition = new Vector2(xPos, _markerRt.anchoredPosition.y);
    }
}
