using UnityEngine;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
  [SerializeField] private float _initialHealth = 10;
  [SerializeField] private float _totalHealth = 10;
  private float _currHealth;

  private void Awake()
  {
    _currHealth = _initialHealth;
  }

  public float GetHealth()
  {
    return _currHealth;
  }

  public float GetTotalHealth()
  {
    return _totalHealth;
  }

  public void SetHealth(float to)
  {
    _currHealth = to;
  }
}