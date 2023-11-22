using UnityEngine;

public class BulletStats : MonoBehaviour
{
    [HideInInspector] public float Speed = 1f;
    [HideInInspector] public float Damage = 1f;
    [HideInInspector] public int Lifetime = 1;

    public void Initialize(BaseStats stats)
    {
        Speed = stats.Speed;
        Damage = stats.Damage;
        Lifetime = stats.Lifetime;
    }
}
