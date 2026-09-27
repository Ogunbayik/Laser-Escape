using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerBase _player;
    private Rigidbody _rb;

    [SerializeField] private float _movementSpeed;

    private Vector3 _movementDirection;

    private void Awake()
    {
        _player = GetComponent<PlayerBase>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector2 input = _player.Input.GetPlayerInput();

        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

        _movementDirection = direction * _movementSpeed;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector3(_movementDirection.x, _rb.linearVelocity.y, _movementDirection.z);
    }
}
