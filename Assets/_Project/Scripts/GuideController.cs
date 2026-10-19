using DG.Tweening;
using UnityEngine;

public class GuideController : MonoBehaviour
{
    [Header("Canvas Group References")]
    [SerializeField] private CanvasGroup _canvasGroup;


    [Header("Animation Settings")]
    [SerializeField] private float _fadeLoopDuration;
    [SerializeField] private float _loopDelay;
    [SerializeField] private float _hideDuration;


    [Header("Blink Settings")]
    [SerializeField] private int _blinkCount;

    private void Awake() => _canvasGroup.alpha = GameConstant.MathConstant.NORMALIZED_MIN;
    private void Start() => BlinkEffect();
    private void BlinkEffect()
    {
        var targetAlpha = GameConstant.MathConstant.NORMALIZED_MAX / 2f;
        var showDelay = _hideDuration / 2f;

        Sequence blinkSequence = DOTween.Sequence();

        blinkSequence.AppendInterval(showDelay);

        for (int i = 0; i < _blinkCount; i++)
        {
            blinkSequence.Append(_canvasGroup.DOFade(targetAlpha, _fadeLoopDuration));
            blinkSequence.AppendInterval(_loopDelay);
            blinkSequence.Append(_canvasGroup.DOFade(GameConstant.MathConstant.NORMALIZED_MAX, _fadeLoopDuration));
            blinkSequence.AppendInterval(_loopDelay);
        }

        blinkSequence.Append(_canvasGroup.DOFade(GameConstant.MathConstant.NORMALIZED_MIN, _hideDuration)).OnComplete(() => gameObject.SetActive(false));
    }
}
