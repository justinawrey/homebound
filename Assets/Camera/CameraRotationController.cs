using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class CameraRotationController : MonoBehaviour
{
  [SerializeField] private float smoothTime = 0.1f;

  private float currAngle;
  private float targetAngle;
  private float velocity;

  private void Awake()
  {
    targetAngle = 45;
    currAngle = targetAngle;
    CustomInputManager.SubscribeToAction(ActionMapName.Default, ActionName.RotateCameraClockwise, RotateCameraClockwise);
    CustomInputManager.SubscribeToAction(ActionMapName.Default, ActionName.RotateCameraCounterClockwise, RotateCameraCounterClockwise);
  }

  public void OnBeforeNextSceneSetup()
  {
    CustomInputManager.UnsubscribeFromAction(ActionMapName.Default, ActionName.RotateCameraClockwise, RotateCameraClockwise);
    CustomInputManager.UnsubscribeFromAction(ActionMapName.Default, ActionName.RotateCameraCounterClockwise, RotateCameraCounterClockwise);
  }

  private void Update()
  {
    currAngle = Mathf.SmoothDamp(currAngle, targetAngle, ref velocity, smoothTime);
    SetYRotation(currAngle);
  }

  private void SetYRotation(float y)
  {
    transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
  }

  public void RotateCameraClockwise(CallbackContext _)
  {
    targetAngle += 90;
  }

  public void RotateCameraCounterClockwise(CallbackContext _)
  {
    targetAngle -= 90;
  }
}
