using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractView : MonoBehaviour
{
    private const float DEFAULT_SCALE = 1f;


    [Header("Image References")] 
    [SerializeField] private Image _interactImage;


    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI _interactText;


    [Header("Canvas References")]
    [SerializeField] private CanvasGroup _canvasGroup;


    [Header("Duration Settings")]
    [SerializeField] private float _toggleDuration;
    [SerializeField] private float _sizeChangeDuration;
    [SerializeField] private float _waitDuration;


    [Header("Animation Settings")]
    [SerializeField] private float _targetScale;

    private Sequence _interactSequence;

    private void Start() => ResetInteractView();
    public void ShowInteractView()
    {
        _canvasGroup.transform.DOKill();
        ToggleInteractView(true);
        _canvasGroup.DOFade(GameConstant.MathConstant.NORMALIZED_MAX, _toggleDuration).OnComplete(() => PlayInteractAnimation());
    }
    public void HideInteractView()
    {
        _canvasGroup.transform.DOKill();

        if (_interactSequence != null && _interactSequence.IsActive())
            _interactSequence.Kill();

        _canvasGroup.DOFade(GameConstant.MathConstant.NORMALIZED_MIN, _toggleDuration).OnComplete(() => ResetInteractView());
    }
    public void PlayInteractAnimation()
    {
        _interactImage.transform.DOKill();

        if (_interactSequence != null && _interactSequence.IsActive())
            _interactSequence.Kill();

        _interactImage.transform.localScale = Vector3.one;

        _interactSequence = DOTween.Sequence();

        _interactSequence.Append(_interactImage.transform.DOScale(DEFAULT_SCALE * _targetScale, _sizeChangeDuration).SetEase(Ease.InOutSine));

        _interactSequence.AppendInterval(_waitDuration);

        _interactSequence.Append(_interactImage.transform.DOScale(DEFAULT_SCALE, _sizeChangeDuration).SetEase(Ease.InOutSine));

        _interactSequence.SetLoops(-1, LoopType.Restart);
    }
    private void ResetInteractView()
    {
        if (_interactSequence != null && _interactSequence.IsActive())
            _interactSequence.Kill();

        _interactImage.transform.localScale = Vector3.one;
        ToggleInteractView(false);
    }
    private void ToggleInteractView(bool isActive)
    {
        _interactImage.gameObject.SetActive(isActive);
        _interactText.gameObject.SetActive(isActive);
    }
}
