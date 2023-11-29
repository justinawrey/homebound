using UnityEngine;

public class BaseStats : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;

    public float Speed = 5f;
    public float Damage = 1f;
    public float FireInterval = 1f;
    public int Lifetime = 1;

    public float ActualSpeed()
    {
        return Speed;
    }

    public float ActualDamage()
    {
        return _playerStatsSO.Damage.Amount.Value + Damage;
    }

    public float ActualFireInterval()
    {
        return FireInterval;
    }

    public int ActualLifetime()
    {
        return Lifetime;
    }
}