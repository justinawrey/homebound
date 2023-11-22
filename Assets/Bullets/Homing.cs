using UnityEngine;

public class Homing : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private BulletStats _stats;

    public void Initialize(Health target)
    {
        _target = target.gameObject.transform;
    }

    // TODO: this would be better object pooled, and the Vector3.Distance might be too heavy
    private void Update()
    {
        // TODO: this is bad.  if the target is destroyed and there are inflight bullets, make
        // sure that these bullets dont fail when trying to access the broken reference.
        // We should never even get to this case if possible
        if (!_target)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _stats.Speed * Time.deltaTime);
    }
}