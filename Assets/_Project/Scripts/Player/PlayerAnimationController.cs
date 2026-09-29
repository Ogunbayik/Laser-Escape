using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private const string _isMoving = "IsMoving";

    public static int MOVING_HASH = Animator.StringToHash(_isMoving);

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayMoveAnimation(bool isMoving)
    {
        _animator.SetBool(MOVING_HASH, isMoving);
    }
}
