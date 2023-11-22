using UnityEngine;

public class CycleLight : MonoBehaviour
{
    [SerializeField] private float _yRotationPerDay = 360f;
    private Vector3 _originalRotation;

    private void Awake()
    {
        _originalRotation = transform.eulerAngles;
        EventBus.OnDayStart += () => ChangeLightRotation(0);
        EventBus.OnDayEnd += () => ChangeLightRotation(1);
        EventBus.OnDayTick += ChangeLightRotation;
    }

    private void OnDestroy()
    {
        EventBus.OnDayStart -= () => ChangeLightRotation(0);
        EventBus.OnDayEnd -= () => ChangeLightRotation(1);
        EventBus.OnDayTick -= ChangeLightRotation;
    }

    private void ChangeLightRotation(float percent)
    {
        float yRotation = Mathf.Lerp(0, _yRotationPerDay, percent);
        transform.eulerAngles = new Vector3(
            _originalRotation.x,
            _originalRotation.y + yRotation,
            _originalRotation.z
        );
    }
}
