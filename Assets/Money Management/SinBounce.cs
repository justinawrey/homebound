using UnityEngine;

public class SinBounce : MonoBehaviour
{
    [SerializeField] private float _amplitude = 2;
    [SerializeField] private float _speed = 5;

    private Vector3 _originalPosition;

    private void Awake()
    {
        _originalPosition = transform.position;
    }

    private void Update()
    {
        float yPos = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.position = new Vector3(
            _originalPosition.x,
            _originalPosition.y + yPos,
            _originalPosition.z
        );
    }
}
