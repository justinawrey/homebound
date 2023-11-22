using UnityEngine;

public class BaseAilmentSO : ScriptableObject
{
  public float Rate = 2f;
  public float Duration = 10f;

  // Tick also runs immediately when applied
  public virtual void Tick(GameObject ailed, Collider agentCollider) { }
  public virtual void OnStart(GameObject ailed, Collider agentCollider) { }
  public virtual void OnFinish(GameObject ailed, Collider agentCollider) { }
}