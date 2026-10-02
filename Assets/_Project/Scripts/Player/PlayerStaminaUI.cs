using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaUI : MonoBehaviour
{
    private PlayerBase _player;
    private CanvasGroup _canvasGroup;


    [Header("Image References")]
    [SerializeField] private Image _staminaFillImage;

    [Header("Settings")]
    [SerializeField] private bool _hideWhenFull = true;
    [SerializeField] private float _fadeDuration = 0.25f;

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
        }
    }

    private void OnDisable()
    {
        if (_player != null)
        {
            _player.OnStaminaChanged -= HandleStaminaChanged;
        }

        _fadeTween?.Kill();
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
            float targetAlpha = ratio >= 0.99f ? 0f : 1f;

            if (Mathf.Approximately(_canvasGroup.alpha, targetAlpha) && !_fadeTween.IsActive())
                return;

            _fadeTween?.Kill();
            _fadeTween = _canvasGroup.DOFade(targetAlpha, _fadeDuration).SetEase(Ease.OutQuad);
        }
    }
}
