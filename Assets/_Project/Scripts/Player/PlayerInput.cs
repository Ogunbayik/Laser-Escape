using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public event Action OnPressedInteract;
    public event Action OnPressedTestInteract;

    private GameInput _gameInput;
    private void Awake()
    {
        _gameInput = new GameInput();

        _gameInput.Player.Interact.performed += OnInteractPerformed;
        _gameInput.Player.TestInteract.performed += OnTestInteractPerformed;
    }
    private void OnEnable() => _gameInput.Enable();
    private void OnDisable() => _gameInput.Disable();
    private void OnDestroy()
    {
        if (_gameInput != null)
        {
            _gameInput.Player.Interact.performed -= OnInteractPerformed;
        }
    }
    private void OnInteractPerformed(InputAction.CallbackContext obj)
    {
        OnPressedInteract?.Invoke();
    }
    private void OnTestInteractPerformed(InputAction.CallbackContext context)
    {
        OnPressedTestInteract?.Invoke();
    }
    public Vector2 GetPlayerInput() => _gameInput.Player.Movement.ReadValue<Vector2>();

}
