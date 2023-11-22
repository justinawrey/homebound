using DG.Tweening;
using UnityEngine;

public class DriftAndExpire : MonoBehaviour
{
    [SerializeField] private float _expiryTime = 2f;
    [SerializeField] private float _driftHeight = 1f;

    private void Start()
    {
        transform.DOLocalMoveY(_driftHeight, _expiryTime).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
