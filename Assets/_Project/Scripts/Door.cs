using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Identity Settings")]
    [SerializeField] private int _doorID;

    private Animator _animator;
    private BoxCollider _collider;

    private bool _isOpen = false;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();
    }
    private void OnEnable()
    {
        InteractionEvents.OnButtonTriggered += HandleButtonTriggered;
    }
    private void OnDisable()
    {
        InteractionEvents.OnButtonTriggered -= HandleButtonTriggered;
    }
    private void HandleButtonTriggered(int id, bool isActive)
    {
        if (id != _doorID)
            return;

        if (isActive)
            Open();
        else
            Close();
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
