using UnityEngine;

public class MotionSkew : MonoBehaviour
{
    [SerializeField] private float _maxSkewAngle = 5;
    private Vector3 _originalEulerAngles;

    private void Awake()
    {
        _originalEulerAngles = transform.eulerAngles;
    }

    private void Update()
    {
        // Vector3 motion = HouseMovementController.Motion;
        // print(motion);
        // Vector3 eulerSkew = math.remap(Vector3.zero, motion, Vector3.zero, Vector3.one * _maxSkewAngle, motion);
        // print(eulerSkew);
        // transform.eulerAngles = _originalEulerAngles + eulerSkew;
    }
}
