using DG.Tweening;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private float _fadeDuration = 1f;

    private Renderer _renderer;
    private MaterialPropertyBlock _mpb;
    private float _currentOpacity = 0.8f;

    private void Awake()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
        _mpb = new MaterialPropertyBlock();
    }
    public void FadeOutBarrier()
    {
        DOTween.To(() => _currentOpacity, x => UpdateOpacity(x), GameConstant.MathConstant.NORMALIZED_MIN, _fadeDuration)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
    private void UpdateOpacity(float value)
    {
        _currentOpacity = value;

        _renderer.GetPropertyBlock(_mpb);
        _mpb.SetFloat(GameConstant.ShaderProperties.OpacityPropertyID, _currentOpacity);
        _renderer.SetPropertyBlock(_mpb);
    }
}
