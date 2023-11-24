using UnityEngine;

public static class VectorUtils
{
  public static bool Approximately(this Vector3 vec, Vector3 other)
  {
    return
      Mathf.Approximately(vec.x, other.x) &&
      Mathf.Approximately(vec.y, other.y) &&
      Mathf.Approximately(vec.z, other.z);
  }
}