using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAuraView : MonoBehaviour
{

    [Header("Canvas Group References")]
    [SerializeField] private CanvasGroup _canvasGroup;


    [Header("Image References")]
    [SerializeField] private Image _auraImage;


    [Header("Animation Settings")]
    [SerializeField] private float _minAuraScale;
    [SerializeField] private float _maxAuraScale;
    [SerializeField] private float _scaleChangeDuration;
    [SerializeField] private float _waitDuration;
    [SerializeField] private float _fadeDuration;

    private Sequence _auraSequence;

    private void Start() => Initialize();        
    private void Initialize()
    {
        _auraImage.transform.localScale = new Vector3(_maxAuraScale, _maxAuraScale, _maxAuraScale);
        _canvasGroup.alpha = GameConstant.MathConstant.NORMALIZED_MIN;
    }
    public void ToggleAura(bool isActive)
    {
        KillAuraSequence();

        if (isActive)
        {
            _canvasGroup.DOFade(GameConstant.MathConstant.NORMALIZED_MAX, _fadeDuration).OnComplete(() => StartAuraLoopEffect());
        }
        else
        {
            _canvasGroup.DOFade(GameConstant.MathConstant.NORMALIZED_MIN, _fadeDuration);
        }
    }
    private void StartAuraLoopEffect()
    {
        KillAuraSequence();

        _auraSequence = DOTween.Sequence();

        _auraSequence.Append(_auraImage.transform.DOScale(_minAuraScale, _scaleChangeDuration));

        _auraSequence.AppendInterval(_waitDuration);

        _auraSequence.Append(_auraImage.transform.DOScale(_maxAuraScale, _scaleChangeDuration));

        _auraSequence.AppendInterval(_waitDuration);

        _auraSequence.SetLoops(-1, LoopType.Restart);
    }
    private void KillAuraSequence()
    {
        if (_auraSequence != null && _auraSequence.IsActive())
        {
            _auraSequence.Kill();
            _auraSequence = null;
        }
    }
    private void OnDestroy()
    {
        // 2. ÖNEMLİ: Sahne kapanırken veya obje yok olurken mutlaka Kill çağrılmalı!
        KillAuraSequence();
    }
}
