using System;
using UnityEngine;

public class CycleLight : MonoBehaviour
{
    [SerializeField] private float _yRotationPerDay = 360f;

    private Vector3 _originalRotation;
    private Action _RotateToStart;
    private Action _RotateToEnd;

    private void Awake()
    {
        _RotateToStart = () => ChangeLightRotation(0);
        _RotateToEnd = () => ChangeLightRotation(1);

        _originalRotation = transform.eulerAngles;
        EventBus.OnDayStart += _RotateToStart;
        EventBus.OnDayEnd += _RotateToEnd;
        EventBus.OnDayTick += ChangeLightRotation;
    }

    private void OnDestroy()
    {
        EventBus.OnDayStart -= _RotateToStart;
        EventBus.OnDayEnd -= _RotateToEnd;
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
