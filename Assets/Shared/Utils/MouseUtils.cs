using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public static class MouseUtils
{
  public static Vector2 GetMouseScreenPos()
  {
    Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
    Vector2 remappedMouseScreenPos = math.remap(
        Vector2.zero,
        new Vector2(Screen.width, Screen.height),
        Vector2.zero,
        ScreenUtils.ScreenResolution,
        mouseScreenPos
    );

    return remappedMouseScreenPos;
  }

  public static Vector2 GetMouseScreenPercentage()
  {
    Vector2 screenPos = GetMouseScreenPos();

    float percentX = Mathf.InverseLerp(0, ScreenUtils.ScreenResolution.x, screenPos.x);
    float percentY = Mathf.InverseLerp(0, ScreenUtils.ScreenResolution.y, screenPos.y);

    return new Vector2(percentX, percentY);
  }
}