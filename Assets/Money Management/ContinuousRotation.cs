using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5;

    private void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * _rotationSpeed, 0));
    }
}
