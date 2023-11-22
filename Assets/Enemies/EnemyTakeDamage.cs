using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!LayerUtils.CompareLayer(other.gameObject, LayerName.DamageDealer))
        {
            return;
        }

        // TODO: maybe I need something more generic than this? idk
        var bulletStats = other.GetComponent<BulletStats>();
        if (bulletStats == null)
        {
            return;
        }

        _health.DecrementHealth(bulletStats.Damage, other.gameObject);
    }
}
