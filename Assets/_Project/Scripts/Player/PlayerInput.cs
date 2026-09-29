using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action OnPressedInteract;

    private GameInput _gameInput;
    private void Awake()
    {
        _gameInput = new GameInput();

        _gameInput.Player.Interact.performed += OnInteractPerformed;
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
    private void OnInteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPressedInteract?.Invoke();
    }
    public Vector2 GetPlayerInput() => _gameInput.Player.Movement.ReadValue<Vector2>();

}
