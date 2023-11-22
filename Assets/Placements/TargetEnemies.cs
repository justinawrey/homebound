using UnityEngine;

public class TargetEnemies : MonoBehaviour
{
    [SerializeField] private int _concurrentTargetCount = 1;
    [SerializeField] private float _radiusSize = 10f;
    [SerializeField] private TagName _targetTag = TagName.Enemy;
    [SerializeField] private Transform _bulletOrigin;
    [SerializeField] private GameObject _bulletPrefab;

    private BaseStats _baseStats;
    private GameObject _bulletsContainer;
    private float _timeUntilNextFire = 0;

    private void Awake()
    {
        _baseStats = GetComponent<BaseStats>();
    }

    private void Start()
    {
        _bulletsContainer = TagUtils.FindWithTag(TagName.BulletsContainer);
    }

    private bool IsTargetable(GameObject gameObject, out Health health)
    {
        health = gameObject.GetComponent<Health>();
        if (!health)
        {
            return false;
        }

        if (!TagUtils.CompareTag(gameObject, _targetTag))
        {
            return false;
        }

        return true;
    }

    private void Update()
    {
        _timeUntilNextFire += Time.deltaTime;
        if (_timeUntilNextFire >= _baseStats.FireInterval)
        {
            Fire();
            _timeUntilNextFire = 0;
        }
    }

    private void Fire()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, _radiusSize);

        int found = 0;
        foreach (Collider collider in enemyColliders)
        {
            GameObject gameObject = collider.gameObject;
            Health health;
            if (IsTargetable(gameObject, out health))
            {
                GameObject bullet = Instantiate(_bulletPrefab, _bulletOrigin.position, Quaternion.identity, _bulletsContainer.transform);
                bullet.GetComponent<BulletStats>().Initialize(_baseStats);
                bullet.GetComponent<Homing>().Initialize(health);
                found++;
            }

            if (found >= _concurrentTargetCount)
            {
                break;
            }
        }
    }
}
