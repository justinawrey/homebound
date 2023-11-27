using UnityEngine;

public class ShootForwards : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletOrigin;

    private BaseStats _stats;
    private float _counter = 0;
    private GameObject _bulletContainer;

    private void Awake()
    {
        _stats = GetComponent<BaseStats>();
        _bulletContainer = TagUtils.FindWithTag(TagName.BulletsContainer);
    }

    // TOD: this might be a good case for inheritance if i need this more....
    private void Update()
    {
        _counter += Time.deltaTime;
        if (_counter >= _stats.FireInterval)
        {
            Fire();
            _counter = 0;
        }
    }

    private void Fire()
    {
        var bullet = Instantiate(_bulletPrefab, _bulletOrigin.position, transform.rotation, _bulletContainer.transform);
        bullet.GetComponent<BulletStats>().Initialize(_stats);
    }
}
