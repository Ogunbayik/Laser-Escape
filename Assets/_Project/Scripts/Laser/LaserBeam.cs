using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [Header("Transform References")]
    [SerializeField] private Transform _firePoint;

    [Header("Laser Beam Settings")]
    [SerializeField] private float _maxDistance = 50f;
    [SerializeField] private int _maxReflections = 3;

    [Header("Layer Filters")]
    [Tooltip("Lazerin durmasına veya yansımasına sebep olan katmanlar (Wall, Mirror vb.)")]
    [SerializeField] private LayerMask _blockerMask;
    [Tooltip("Lazerin etkileşime girdiği katmanlar (Player vb.)")]
    [SerializeField] private LayerMask _interactableMask;

    private LineRenderer _lineRenderer;
    private readonly List<Vector3> _laserPoints = new List<Vector3>();
    private readonly HashSet<ILaserTargetable> _currentTargets = new HashSet<ILaserTargetable>();
    private readonly HashSet<ILaserTargetable> _previousTargets = new HashSet<ILaserTargetable>();

    public LineRenderer LineRenderer => _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        ProcessLaser();
    }

    private void ProcessLaser()
    {
        // Target takibini sıfırla
        _previousTargets.Clear();
        foreach (var target in _currentTargets) _previousTargets.Add(target);
        _currentTargets.Clear();

        // 1) Işın başlangıcı
        _laserPoints.Clear();
        Vector3 currentPosition = _firePoint.position;
        Vector3 currentDirection = _firePoint.forward;
        float remainingDistance = _maxDistance;

        _laserPoints.Add(currentPosition);

        for (int i = 0; i < _maxReflections; i++)
        {
            if (remainingDistance <= 0f) 
                break;

            bool hasBlocker = Physics.Raycast(currentPosition, currentDirection, out RaycastHit hit, remainingDistance, _blockerMask);
            float segmentCheckDistance = hasBlocker ? hit.distance : remainingDistance;

            CheckTargetsOnSegment(currentPosition, currentDirection, segmentCheckDistance);

            // Blocker (Wall / Mirror) var mı?
            if (hasBlocker)
            {
                _laserPoints.Add(hit.point);
                remainingDistance -= hit.distance;

                if (hit.collider.TryGetComponent(out ILaserTargetable target))
                {
                    RegisterTarget(target);
                }

                if (hit.collider.CompareTag("Mirror"))
                {
                    currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                    currentPosition = hit.point + (currentDirection * 0.01f);
                }
                else
                {
                    break;
                }
            }
            else
            {
                _laserPoints.Add(currentPosition + (currentDirection * remainingDistance));
                break;
            }
        }

        // 4) Target durumlarını güncelle
        UpdateTargetEvents();

        // 1) Işını çiz
        _lineRenderer.positionCount = _laserPoints.Count;
        _lineRenderer.SetPositions(_laserPoints.ToArray());
    }

    private void CheckTargetsOnSegment(Vector3 origin, Vector3 direction, float distance)
    {
        RaycastHit[] hits = Physics.RaycastAll(origin, direction, distance, _interactableMask);
        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent(out ILaserTargetable target))
            {
                RegisterTarget(target);
            }
        }
    }

    private void RegisterTarget(ILaserTargetable target)
    {
        if (_currentTargets.Add(target))
        {
            target.HitByLaser();
        }
    }

    private void UpdateTargetEvents()
    {
        foreach (var prevTarget in _previousTargets)
        {
            if (!_currentTargets.Contains(prevTarget))
            {
                prevTarget.LaserLost();
            }
        }
    }
    private void OnDisable()
    {
        foreach (var target in _currentTargets)
        {
            target?.LaserLost();
        }
        _currentTargets.Clear();
        _previousTargets.Clear();

        if (_lineRenderer != null)
            _lineRenderer.positionCount = 0;
    }
}
