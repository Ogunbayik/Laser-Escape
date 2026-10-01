using DG.Tweening;
using UnityEngine;

public class Button : MonoBehaviour, ILaserTargetable
{
    private MeshRenderer _meshRenderer;

    [Header("Transform References")]
    [SerializeField] private Transform _visual;

    [Header("Color Settings")]
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _activeColor;

    [Header("Trigger Settings")]
    [SerializeField] private float _triggerOffset;
    [SerializeField] private float _triggerDuration;

    
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
        Sequence hitSequence = DOTween.Sequence();

        hitSequence.Append(_visual.transform.DOLocalMoveZ(_triggerOffset, _triggerDuration));
        hitSequence.Join(_meshRenderer.material.DOColor(_activeColor, _triggerDuration));
    }
    private void HandleLostLaser()
    {
        Sequence lostSequence = DOTween.Sequence();

        lostSequence.Append(_visual.transform.DOLocalMoveZ(0f, _triggerDuration));
        lostSequence.Join(_meshRenderer.material.DOColor(_defaultColor, _triggerDuration));
    }
}
