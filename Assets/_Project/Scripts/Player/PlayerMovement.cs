using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerBase _player;
    private Rigidbody _rb;

    private Vector3 _movementDirection;

    [Header("Visual References")]
    [SerializeField] private Transform _visual;

    private bool _isMoving;
    private void Awake()
    {
        _player = GetComponent<PlayerBase>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector2 input = _player.Input.GetPlayerInput();

        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

        _movementDirection = direction * _player.Data.MovementSpeed;

        if (direction.sqrMagnitude > 0.01f)
            _isMoving = true;
        else
            _isMoving = false;

        _player.AnimationController.PlayMoveAnimation(_isMoving);

        HandleRotation();
    }

    private void FixedUpdate()
    {
        if (_isMoving)
            _rb.linearVelocity = new Vector3(_movementDirection.x, _rb.linearVelocity.y, _movementDirection.z);
    }
    private void HandleRotation()
    {
        if (_isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);

            _visual.transform.rotation = Quaternion.Slerp(_visual.transform.rotation, targetRotation, _player.Data.RotationSpeed * Time.deltaTime);
        }
    }
    public void SetMovingStatus(bool isMoving) => _isMoving = isMoving;
}
