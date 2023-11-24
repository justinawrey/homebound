using System;
using ReactiveUnity;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class HouseRotationController : MonoBehaviour
{
    [SerializeField] private float _smoothTime = 0.1f;

    private float _currAngle;
    private float _targetAngle;
    private float _velocity;

    private Reactive<bool> _rotating = new Reactive<bool>(false);
    private Action _unsub;

    private void Awake()
    {
        _targetAngle = 0;
        _currAngle = _targetAngle;
        CustomInputManager.SubscribeToAction(ActionMapName.Default, ActionName.RotateHouseClockwise, RotateClockwise);
        CustomInputManager.SubscribeToAction(ActionMapName.Default, ActionName.RotateHouseCounterClockwise, RotateCounterClockwise);

        _unsub = _rotating.OnChange(OnRotatingChange);
    }

    private void OnDestroy()
    {
        _unsub();
    }

    private void OnRotatingChange(bool prev, bool curr)
    {
        if (curr)
        {
            EventBus.HouseRotationStart();
        }
        else
        {
            EventBus.HouseRotationEnd();
        }
    }

    public void OnTransitionOutEnd()
    {
        CustomInputManager.UnsubscribeFromAction(ActionMapName.Default, ActionName.RotateHouseClockwise, RotateClockwise);
        CustomInputManager.UnsubscribeFromAction(ActionMapName.Default, ActionName.RotateHouseCounterClockwise, RotateCounterClockwise);
    }

    private void Update()
    {
        _currAngle = Mathf.SmoothDamp(_currAngle, _targetAngle, ref _velocity, _smoothTime);
        SetYRotation(_currAngle);

        if (Mathf.Approximately(_currAngle, _targetAngle))
        {
            _rotating.Value = false;
        }
    }

    private void SetYRotation(float y)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
    }

    public void RotateClockwise(CallbackContext _)
    {
        _rotating.Value = true;
        EventBus.HouseRotationStart();
        _targetAngle += 90;
    }

    public void RotateCounterClockwise(CallbackContext _)
    {
        _rotating.Value = true;
        EventBus.HouseRotationStart();
        _targetAngle -= 90;
    }
}

