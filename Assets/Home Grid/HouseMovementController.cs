using UnityEngine;
using UnityEngine.InputSystem;

public class HouseMovementController : MonoBehaviour
{
    // Parameters
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;

    // Own components
    private InputAction _moveAction;
    protected Rigidbody _rb;
    protected Collider _collider;
    private GameObject _vCam;

    // Inputs
    public static Vector3 Motion = new Vector3(0, 0, 0);

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _vCam = TagUtils.FindWithTag(TagName.VCam);
        _moveAction = CustomInputManager.GetAction(ActionMapName.Default, ActionName.Move);
    }

    private void Update()
    {
        var motion = _moveAction.ReadValue<Vector2>();
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, _vCam.transform.eulerAngles.y, 0));
        var skewed = matrix.MultiplyPoint3x4(new Vector3(motion.x, 0, motion.y).normalized);
        Motion = skewed;
    }

    public void FixedUpdate()
    {
        _rb.AddForce(new Vector3(CalculateXMovementForce(), 0f, CalculateZMovementForce()));
    }

    private float CalculateXMovementForce()
    {
        float targetSpeed = Motion.x * _maxSpeed;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _deceleration;
        float speedDiff = targetSpeed - _rb.velocity.x;
        float movement = speedDiff * accelRate;

        return movement;
    }

    private float CalculateZMovementForce()
    {
        float targetSpeed = Motion.z * _maxSpeed;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _deceleration;
        float speedDiff = targetSpeed - _rb.velocity.z;
        float movement = speedDiff * accelRate;

        return movement;
    }
}


