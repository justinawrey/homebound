using System.Collections.Generic;
using UnityEngine;

public class BulletLifetimeController : MonoBehaviour
{
    [SerializeField] private List<TagName> _affectedTags;
    private BulletStats _bulletStats;

    private void Awake()
    {
        _bulletStats = GetComponent<BulletStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (TagName tagName in _affectedTags)
        {
            if (TagUtils.CompareTag(other.gameObject, tagName))
            {
                // Only decrement lifetime by 1 per on trigger enter invocation
                DecrementLifetime();
                return;
            }
        }
    }

    private void DecrementLifetime()
    {
        _bulletStats.Lifetime -= 1;
    }

    private void Update()
    {
        if (_bulletStats.Lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
