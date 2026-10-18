using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaUI : MonoBehaviour
{
    private PlayerBase _player;
    private CanvasGroup _canvasGroup;


    [Header("Image References")]
    [SerializeField] private Image _staminaFillImage;
    [SerializeField] private Image _runImage;


    [Header("Settings")]
    [SerializeField] private bool _hideWhenFull = true;
    [SerializeField] private float _fadeDuration = 0.25f;
    [SerializeField] private float _colorChangeDuration = 0.5f;



    private Tween _fadeTween;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerBase>();

        _canvasGroup = GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        if (_player != null)
        {
            _player.OnStaminaChanged += HandleStaminaChanged;
            _player.OnPlayerDied += HideStaminaBar;
            _player.OnExhaustedChanged += ChangeRunImageColor;
        }
    }
    private void OnDisable()
    {
        if (_player != null)
        {
            _player.OnStaminaChanged -= HandleStaminaChanged;
            _player.OnPlayerDied -= HideStaminaBar;
        }

        _fadeTween?.Kill();
    }
    private void ChangeRunImageColor(bool isExhausted)
    {
        Color targetColor = isExhausted ? Color.red : Color.white;
        ChangeRunColorWhenExhausted(targetColor);
    }
    public void ChangeRunColorWhenExhausted(Color targetColor) => _runImage.DOColor(targetColor, _colorChangeDuration);
    private void HideStaminaBar() => _canvasGroup.DOFade(0f, _fadeDuration).OnComplete(() => ResetStamina());
    private void ResetStamina()
    {
        _staminaFillImage.fillAmount = GameConstant.MathConstant.NORMALIZED_MAX;
        _player.ResetStamina();
    }
    private void Start()
    {
        if (_player != null)
        {
            float ratio = _player.CurrentStamina / _player.Data.MaxStamina;
            _staminaFillImage.fillAmount = ratio;

            if (_hideWhenFull && _canvasGroup != null)
            {
                _canvasGroup.alpha = 0f;
            }
        }
    }
    private void HandleStaminaChanged(float current, float max)
    {
        float ratio = current / max;
        _staminaFillImage.fillAmount = ratio;

        if (_hideWhenFull && _canvasGroup != null)
        {
            float targetAlpha = ratio >= 0.99f ? GameConstant.MathConstant.NORMALIZED_MIN : GameConstant.MathConstant.NORMALIZED_MAX;

            if (Mathf.Approximately(_canvasGroup.alpha, targetAlpha) && !_fadeTween.IsActive())
                return;

            _fadeTween?.Kill();
            _fadeTween = _canvasGroup.DOFade(targetAlpha, _fadeDuration).SetEase(Ease.OutQuad);
        }
    }
}
