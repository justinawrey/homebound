using UnityEngine;

public class DestroyDeathReceiver : MonoBehaviour, IDeathReceiver
{
    [SerializeField] private GameObject _deadEnemy;

    // Before dying, change all of the floating text components so they dont get destroyed
    // when this object is destroyed
    public void OnDeath(GameObject _)
    {
        Instantiate(_deadEnemy, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
