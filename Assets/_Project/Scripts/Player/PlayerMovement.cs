using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerBase _player;
    private Rigidbody _rb;

    private Vector3 _movementDirection;

    [Header("Visual References")]
    [SerializeField] private Transform _visual;
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

        HandleRotation();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector3(_movementDirection.x, _rb.linearVelocity.y, _movementDirection.z);
    }
    private void HandleRotation()
    {
        if (_movementDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);

            _visual.transform.rotation = Quaternion.Slerp(_visual.transform.rotation, targetRotation, _player.Data.RotationSpeed * Time.deltaTime);
        }
    }
}
