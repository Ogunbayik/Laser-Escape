using DG.Tweening;
using UnityEngine;

public class DeadState : IState
{
    private readonly PlayerBase _player;

    public DeadState(PlayerBase player) => _player = player;
    public void EnterState()
    {
        _player.AnimationController.PlayDeadAnimation();

        HandleDeadEffect();
    }
    public void FixedTick()
    {

    }
    public void Tick()
    {

    }
    public void ExitState()
    {

    }
    private void HandleDeadEffect()
    {
        if (_player.InstancedMaterial == null)
            return;

        _player.InstancedMaterial.SetFloat(GameConstant.ShaderProperties.DissolveID, GameConstant.ShaderValue.MaxValue);
        Color targetColor = Color.black;

        Sequence deathSeq = DOTween.Sequence();

        deathSeq.Append(_player.InstancedMaterial.DOColor(targetColor, GameConstant.ShaderProperties.ColorID, _player.Data.BurnDuration));

        deathSeq.Append(_player.InstancedMaterial.DOFloat(GameConstant.ShaderValue.MinValue, GameConstant.ShaderProperties.DissolveID, _player.Data.DissolveDuration));

        deathSeq.OnComplete(() => {
            Debug.Log("Dead");
        });
    }
}
