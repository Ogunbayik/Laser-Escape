using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public event Action OnPlayerSpawned;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayWalkAnimation(bool isMoving) => _animator.SetBool(GameConstant.PlayerAnimation.MOVE_HASH, isMoving);
    public void PlaySprintAnimation(bool isSprint) => _animator.SetBool(GameConstant.PlayerAnimation.SPRINT_HASH, isSprint);
    public void PlayDeadAnimation() => _animator.SetTrigger(GameConstant.PlayerAnimation.DEAD_HASH);
    public void PlayStandUpAnimation() => _animator.SetTrigger(GameConstant.PlayerAnimation.STAND_UP_HASH);

    public void OnPlayerSpawnedEvent() => OnPlayerSpawned?.Invoke();
}
