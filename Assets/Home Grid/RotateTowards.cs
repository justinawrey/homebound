using UnityEngine;

// Rotate gameObject towards target transform, with y axis set as the up vector.
// We're assuming here that transform.position is the center point.
public class RotateTowards : MonoBehaviour
{
  private Vector3 GetRaycastPoint()
  {
    Plane plane = new Plane(Vector3.up, transform.position);
    Ray ray = Camera.main.ScreenPointToRay(MouseUtils.GetMouseScreenPos());

    float distance;
    if (plane.Raycast(ray, out distance))
    {
      return ray.GetPoint(distance);
    }

    return new Vector3(0, 0, 0);
  }

  private void Update()
  {
    Vector3 targetDirection = GetRaycastPoint() - transform.position;
    Quaternion quaternion;

    // Get rid of annoying error message
    if (targetDirection == Vector3.zero)
    {
      quaternion = Quaternion.identity;
    }
    else
    {
      quaternion = Quaternion.LookRotation(targetDirection, Vector3.up);
      Vector3 angles = quaternion.eulerAngles;
      quaternion = Quaternion.Euler(0, angles.y, 0);
    }

    transform.rotation = quaternion;
  }
}
