using UnityEngine;

// transient, lives only the life of a bullet
public class BulletStats : MonoBehaviour
{
    [HideInInspector] public float Speed = 1f;
    [HideInInspector] public float Damage = 1f;
    [HideInInspector] public int Lifetime = 1;

    public void Initialize(BaseStats stats)
    {
        Speed = stats.ActualSpeed();
        Damage = stats.ActualDamage();
        Lifetime = stats.ActualLifetime();
    }
}
