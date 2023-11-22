using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class TrackCursorForOffset : MonoBehaviour
{
  [SerializeField] private float maxSwayPercentX = 0.1f;
  [SerializeField] private float sensitivityRangePercentX = 0.25f;

  private float maxSwayPercentY;
  private float sensitivityRangePercentY;
  private float centerX;
  private float centerY;

  private CinemachineFramingTransposer transposer;

  private void Awake()
  {
    transposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
    centerX = transposer.m_ScreenX;
    centerY = transposer.m_ScreenY;

    // calculate y sway off of x sway so its even
    maxSwayPercentY = maxSwayPercentX * ScreenUtils.AspectRatio;
    sensitivityRangePercentY = sensitivityRangePercentX * ScreenUtils.AspectRatio;
  }

  private void Update()
  {
    Vector2 screenPercentage = MouseUtils.GetMouseScreenPercentage();
    Vector2 percentFromCenter = new Vector2(
        Mathf.Min(Mathf.Abs(screenPercentage.x - centerX), sensitivityRangePercentX),
        Mathf.Min(Mathf.Abs(screenPercentage.y - centerY), sensitivityRangePercentY)
    );

    Vector2 remappedPercent = math.remap(
        new Vector2(0, 0),
        new Vector2(sensitivityRangePercentX, sensitivityRangePercentY),
        new Vector2(0, 0),
        new Vector2(maxSwayPercentX, maxSwayPercentY),
        percentFromCenter
    );

    float signX = Mathf.Sign(screenPercentage.x - centerX);
    float signY = Mathf.Sign(screenPercentage.y - centerY);
    remappedPercent *= new Vector2(signX, signY);

    SetScreenX(centerX - remappedPercent.x);
    SetScreenY(centerY + remappedPercent.y);
  }

  private void SetScreenX(float x)
  {
    transposer.m_ScreenX = x;
  }

  private void SetScreenY(float y)
  {
    transposer.m_ScreenY = y;
  }
}
