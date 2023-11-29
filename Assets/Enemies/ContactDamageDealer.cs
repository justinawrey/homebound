using UnityEngine;

public class ContactDamageDealer : MonoBehaviour, IDamageDealer
{
    [SerializeField] private float _damage = 5f;

    public void DealDamage(Health health, float armorVal)
    {
        float damageDealt = Mathf.Max(_damage - armorVal, 0);
        health.DecrementHealth(damageDealt, gameObject);
    }
}