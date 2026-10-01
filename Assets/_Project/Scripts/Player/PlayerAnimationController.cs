using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayMoveAnimation(bool isMoving) => _animator.SetBool(GameConstant.PlayerAnimation.MOVE_HASH, isMoving);
    public void PlaySprintAnimation(bool isSprint) => _animator.SetBool("IsSprint", isSprint);
    public void PlayDeadAnimation() => _animator.SetTrigger(GameConstant.PlayerAnimation.DEAD_HASH);
}
