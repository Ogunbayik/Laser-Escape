using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Identity Settings")]
    [SerializeField] private int _doorID;

    [Header("Collider References")]
    [SerializeField] private BoxCollider _postCollider;

    private Animator _animator;

    private bool _isOpen = false;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
        EnableTrigger();
        _animator.SetTrigger(GameConstant.EnvironmentAnimation.OPEN_HASH);
    }
    public void Close()
    {
        if (!_isOpen)
            return;

        _isOpen = false;
        DisableTrigger();
        _animator.SetTrigger(GameConstant.EnvironmentAnimation.CLOSE_HASH);
    }

    public void EnableTrigger() => _postCollider.isTrigger = true;
    public void DisableTrigger() => _postCollider.isTrigger = false;
    
}
