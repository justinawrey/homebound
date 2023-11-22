using UnityEngine;

public class MoveForwards : MonoBehaviour
{
    private BulletStats _bulletStats;

    private void Awake()
    {
        _bulletStats = GetComponent<BulletStats>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _bulletStats.Speed * Time.deltaTime);
    }
}
