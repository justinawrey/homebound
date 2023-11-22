using UnityEngine;

public class DestroyDeathReceiver : MonoBehaviour, IDeathReceiver
{
    // Before dying, change all of the floating text components so they dont get destroyed
    // when this object is destroyed
    public void OnDeath(GameObject _)
    {
        Destroy(gameObject);
    }
}
