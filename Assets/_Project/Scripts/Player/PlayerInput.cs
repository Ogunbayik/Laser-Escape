using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private GameInput _gameInput;
    private void Awake() => _gameInput = new GameInput();
    private void OnEnable() => _gameInput.Enable();
    private void OnDisable() => _gameInput.Disable();
    public Vector2 GetPlayerInput() => _gameInput.Player.Movement.ReadValue<Vector2>();

}
