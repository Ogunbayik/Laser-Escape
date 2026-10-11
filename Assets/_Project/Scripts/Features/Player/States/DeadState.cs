using Cysharp.Threading.Tasks;
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

        _player.InstancedMaterial.SetFloat(GameConstant.ShaderProperties.DissolveID, GameConstant.ShaderValue.MAX_VALUE);
        Color targetColor = Color.black;

        Sequence deathSeq = DOTween.Sequence();

        deathSeq.Append(_player.InstancedMaterial.DOColor(targetColor, GameConstant.ShaderProperties.BaseColorID, _player.Data.BurnDuration));

        deathSeq.Append(_player.InstancedMaterial.DOFloat(GameConstant.ShaderValue.MIN_VALUE, GameConstant.ShaderProperties.DissolveID, _player.Data.DissolveDuration));

        deathSeq.OnComplete(() => {
            HandleSpawnEffect().Forget();
        });
    }
    private async UniTask HandleSpawnEffect()
    {
        await CameraManager.Instance.MoveCameraToRespawnPoint();

        //PlayerSpawn pozisyonu değişecek
        CameraManager.Instance.SwitchCamera(CameraType.Gameplay);
        _player.transform.position = CheckPointManager.Instance.CurrentCheckPoint.SpawnPosition.position;
        _player.Visual.rotation = Quaternion.identity;

        _player.InstancedMaterial.SetColor(GameConstant.ShaderProperties.BaseColorID, Color.white);
        await _player.InstancedMaterial.DOFloat(GameConstant.ShaderValue.MAX_VALUE, GameConstant.ShaderProperties.DissolveID, _player.Data.DissolveDuration).AsyncWaitForCompletion();

        //Tekrardan kontrol edilebilir olacak
        _player.AnimationController.PlayStandUpAnimation();
        _player.SetDeadStatus(false);
    }
}
