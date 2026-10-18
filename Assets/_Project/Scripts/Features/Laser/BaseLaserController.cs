using DG.Tweening;
using OB.Utils;
using UnityEngine;

public abstract class BaseLaserController : MonoBehaviour
{
    protected MaterialPropertyBlock _propBlock;

    protected LaserBeam _laserBeam;
    protected BaseLaserRotator _laserRotator;

    [Header("Laser Status")]
    [SerializeField] protected LaserStatus _currentStatus;


    [Header("Renderer References")]
    [SerializeField] protected MeshRenderer _baseRenderer;
    [SerializeField] protected MeshRenderer _headRenderer;


    [Header("Activate Settings")]
    [SerializeField] protected bool IsLaserBeamActive;


    [Header("Visaul References")]
    [SerializeField] protected Transform _headVisual;



    [Header("Default Color Settings")]
    [SerializeField] protected Color _defBaseColor;
    [ColorUsage(true, true)]
    [SerializeField] protected Color _defEmissionColor;
    [Header("Static Color Settings")]
    [SerializeField] protected Color _staticBaseColor;
    [ColorUsage(true,true)]
    [SerializeField] protected Color _staticEmissionColor;

    [Header("Continuous Color Settings")]
    [SerializeField] protected Color _continuousBaseColor;
    [ColorUsage(true, true)]
    [SerializeField] protected Color _continuousEmissionColor;

    [Header("Step Color Settings")]
    [SerializeField] protected Color _stepBaseColor;
    [ColorUsage(true, true)]
    [SerializeField] protected Color _stepEmissionColor;

    [Header("Variable Color Settings")]
    [SerializeField] protected Color _variableBaseColor;
    [ColorUsage(true, true)]
    [SerializeField] protected Color _variableEmissionColor;

    public Transform Head => _headVisual;
    public BaseLaserRotator LaserRotator => _laserRotator;

    private float _riseOffsetY = 0f;
    private float _riseDuration = 0.75f;
    private int _baseInsideIndex = 1;
    private int _headInsideIndex = 0;

    protected virtual void Awake()
    {
        if (_laserBeam == null)
            _laserBeam = GetComponentInChildren<LaserBeam>();

        if (_laserRotator == null)
            _laserRotator = GetComponent<BaseLaserRotator>();

        _propBlock = new MaterialPropertyBlock();
    }
    protected virtual void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        SetLaserBeamActivateStatus(false);
        SetLaserColor(_defBaseColor, _defEmissionColor);
        SetLaserStatus(LaserStatus.Standby);
    }
    protected void SetLaserBeamActivateStatus(bool isActive)
    {
        IsLaserBeamActive = isActive;
        ApplyLaserState(isActive);
    }
    private void ApplyLaserState(bool isActive)
    {
        _laserBeam.enabled = isActive;

        if (_laserRotator != null)
            _laserRotator.enabled = isActive;
    }
    private void UpdateLaserColor()
    {
        if (_laserRotator == null)
        {
            SetLaserColor(_staticBaseColor, _staticEmissionColor);
            return;
        }

        switch (_laserRotator.RotationType)
        {
            case LaserRotationType.Continuous: 
                SetLaserColor(_continuousBaseColor, _continuousEmissionColor);
                break;
            case LaserRotationType.StepRotatable: 
                SetLaserColor(_stepBaseColor, _stepEmissionColor);
                break;
            case LaserRotationType.VariableSpeed: 
                SetLaserColor(_variableBaseColor, _variableEmissionColor);
                break;
            default:
                SetLaserColor(_staticBaseColor, _staticEmissionColor);
                break;
        }
    }
    private void SetLaserColor(Color baseColor, Color emissionColor)
    {
        if (_baseRenderer != null)
        {
            _baseRenderer.GetPropertyBlock(_propBlock, _baseInsideIndex);
            _propBlock.SetColor(GameConstant.ShaderProperties.BaseColorID, baseColor);
            _propBlock.SetColor(GameConstant.ShaderProperties.EmissionID, emissionColor);
            _baseRenderer.SetPropertyBlock(_propBlock, _baseInsideIndex);
        }

        if (_headRenderer != null)
        {
            _headRenderer.GetPropertyBlock(_propBlock, _headInsideIndex);
            _propBlock.SetColor(GameConstant.ShaderProperties.BaseColorID, baseColor);
            _propBlock.SetColor(GameConstant.ShaderProperties.EmissionID, emissionColor);
            _headRenderer.SetPropertyBlock(_propBlock, _headInsideIndex);
        }
    }
    public void PlayLaserActivateSequence()
    {
        Sequence activateSequence = DOTween.Sequence();

        activateSequence.Append(_headVisual.transform.DOMoveY(_riseOffsetY, _riseDuration));

        activateSequence.AppendCallback(() => UpdateLaserColor());
        activateSequence.JoinCallback(() => ActivateLaser());
    }
    public void PlayLaserDeactivateSequence()
    {
        Sequence deactivateSequence = DOTween.Sequence();

        deactivateSequence.AppendCallback(() => SetLaserStatus(LaserStatus.Inert));
        deactivateSequence.JoinCallback(() => SetLaserBeamActivateStatus(false));

        deactivateSequence.Append(_headVisual.transform.DOMoveY(-0.5f, _riseDuration)).OnComplete(() => SetLaserColor(_defBaseColor, _defEmissionColor));
    }
    private void ActivateLaser()
    {
        SetLaserStatus(LaserStatus.Active);
        SetLaserBeamActivateStatus(true);
    }
    public void SetLaserStatus(LaserStatus newStatus)
    {
        if (_currentStatus == newStatus)
            return;

        _currentStatus = newStatus;
    }
}

public enum LaserStatus
{
    Standby, // AFK / Beklemede (Bölüm başında pasif, henüz oyuncu tetiklemedi)
    Active,  // Aktif ve çalışıyor (Işın saçıyor, hareket ediyor/dönüyor)
    Inert,  // Deaktif / Devre Dışı (Oyuncu bulmacayı çözdü, lazer tamamen kapandı)
    Telegraphing  // (Opsiyonel) Çalışmadan 1-2 sn önce yanıp sönen uyarı durumu
}
