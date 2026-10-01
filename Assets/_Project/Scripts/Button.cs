using DG.Tweening;
using UnityEngine;

public class Button : MonoBehaviour, ILaserTargetable
{
    [Header("Transform References")]
    [SerializeField] private Transform _visual;

    [Header("Identity Settings")]
    [SerializeField] private int _buttonID;

    [Header("Color Settings")]
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _activeColor;

    [Header("Trigger Settings")]
    [SerializeField] private float _triggerOffset;
    [SerializeField] private float _triggerDuration;


    private MeshRenderer _meshRenderer;
    private Sequence _activeSequence;
    private bool _isActive;
    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }
    private void Start()
    {
        _meshRenderer.material.color = _defaultColor;
    }
    public void HitByLaser()
    {
        if (_isActive)
            return;

        _isActive = true;
        HandleHitLaser();
    }
    public void LaserLost()
    {
        if (!_isActive)
            return;

        _isActive = false;
        HandleLostLaser();
    }
    private void HandleHitLaser()
    {
        InteractionEvents.ButtonTrigger(_buttonID, true);

        _activeSequence?.Kill();
        _activeSequence = DOTween.Sequence();

        _activeSequence.Append(_visual.DOLocalMoveZ(_triggerOffset, _triggerDuration));
        _activeSequence.Join(_meshRenderer.material.DOColor(_activeColor, _triggerDuration));
    }
    private void HandleLostLaser()
    {
        InteractionEvents.ButtonTrigger(_buttonID, false);

        _activeSequence?.Kill();
        _activeSequence = DOTween.Sequence();

        _activeSequence.Append(_visual.DOLocalMoveZ(0f, _triggerDuration));
        _activeSequence.Join(_meshRenderer.material.DOColor(_defaultColor, _triggerDuration));
    }
}
