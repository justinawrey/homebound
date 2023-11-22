using UnityEngine;

public class ContactDamageDealer : MonoBehaviour, IDamageDealer
{
    [SerializeField] private float _damage = 5f;

    public void DealDamage(Health health)
    {
        health.DecrementHealth(_damage, gameObject);
    }
}
