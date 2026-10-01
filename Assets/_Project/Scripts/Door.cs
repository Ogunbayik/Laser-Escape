using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private BoxCollider _collider;

    private bool _isOpen = false;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();
    }

    public void Open()
    {
        if (_isOpen)
            return;

        _isOpen = true;
        _animator.SetTrigger(GameConstant.EnvironmentAnimation.OPEN_HASH);
    }
    public void Close()
    {
        if (!_isOpen)
            return;

        _isOpen = false;
        _animator.SetTrigger(GameConstant.EnvironmentAnimation.CLOSE_HASH);
    }

    public void EnableTrigger() => _collider.isTrigger = true;
    public void DisableTrigger() => _collider.isTrigger = false;
    
}
