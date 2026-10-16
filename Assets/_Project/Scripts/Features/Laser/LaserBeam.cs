using DG.Tweening;
using System;
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
    [Tooltip("Lazer çarptığında SÜREKLİ kıvılcım çıkacak duvar/metal katmanları")]
    [SerializeField] private LayerMask _sparkableMask;
    [Tooltip("Lazerin etkileşime girdiği katmanlar (Player vb.)")]
    [SerializeField] private LayerMask _interactableMask;

    [Header("VFX Prefabs")]
    [SerializeField] private ParticleSystem _continuousSparkPrefab;
    private ParticleSystem _spawnedSpark;

    private LineRenderer _lineRenderer;
    private readonly List<Vector3> _laserPoints = new List<Vector3>();
    private readonly HashSet<ILaserTargetable> _currentTargets = new HashSet<ILaserTargetable>();
    private readonly HashSet<ILaserTargetable> _previousTargets = new HashSet<ILaserTargetable>();

    public LineRenderer LineRenderer => _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        if (_continuousSparkPrefab != null)
        {
            _spawnedSpark = Instantiate(_continuousSparkPrefab, transform);
            _spawnedSpark.Stop();
        }
    }
    private void Update()
    {
        ProcessLaser();
    }
    private void ProcessLaser()
    {
        _previousTargets.Clear();
        foreach (var target in _currentTargets) _previousTargets.Add(target);
        _currentTargets.Clear();

        _laserPoints.Clear();
        Vector3 currentPosition = _firePoint.position;
        Vector3 currentDirection = _firePoint.forward;
        float remainingDistance = _maxDistance;

        _laserPoints.Add(currentPosition);

        bool hitSparkableBlocker = false;
        LayerMask combinedRaycastMask = _blockerMask | _sparkableMask;

        for (int i = 0; i < _maxReflections; i++)
        {
            if (remainingDistance <= 0f)
                break;

            bool hasBlocker = Physics.Raycast(currentPosition, currentDirection, out RaycastHit hit, remainingDistance, combinedRaycastMask);
            float segmentCheckDistance = hasBlocker ? hit.distance : remainingDistance;

            CheckTargetsOnSegment(currentPosition, currentDirection, segmentCheckDistance);

            if (hasBlocker)
            {
                _laserPoints.Add(hit.point);
                remainingDistance -= hit.distance;

                if (hit.collider.TryGetComponent(out ILaserTargetable target))
                {
                    RegisterTarget(target, hit.point, hit.normal);
                }

                if (hit.collider.CompareTag("Mirror"))
                {
                    currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                    currentPosition = hit.point + (currentDirection * 0.01f);
                }
                else
                {
                    // Duvara takıldı: Katman Sparkable ise lokal parçacığı buraya taşı ve oynat
                    if (((1 << hit.collider.gameObject.layer) & _sparkableMask) != 0)
                    {
                        if (_spawnedSpark != null)
                        {
                            _spawnedSpark.transform.position = hit.point;
                            _spawnedSpark.transform.rotation = Quaternion.LookRotation(hit.normal);

                            if (!_spawnedSpark.isPlaying) _spawnedSpark.Play();
                        }
                        hitSparkableBlocker = true;
                    }

                    break;
                }
            }
            else
            {
                _laserPoints.Add(currentPosition + (currentDirection * remainingDistance));
                break;
            }
        }

        // Lazer kıvılcım çıkaran bir duvara çarpmıyorsa parçacığı durdur
        if (!hitSparkableBlocker && _spawnedSpark != null && _spawnedSpark.isPlaying)
        {
            _spawnedSpark.Stop();
        }

        UpdateTargetEvents();

        _lineRenderer.positionCount = _laserPoints.Count;
        _lineRenderer.SetPositions(_laserPoints.ToArray());
    }

    private void CheckTargetsOnSegment(Vector3 origin, Vector3 direction, float distance)
    {
        RaycastHit[] hits = Physics.RaycastAll(origin, direction, distance, _interactableMask);

        if (hits.Length == 0)
            return;

        Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent(out ILaserTargetable target))
            {
                RegisterTarget(target, hit.point, hit.normal);
                break;
            }
        }
    }
    private void RegisterTarget(ILaserTargetable target, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (_currentTargets.Add(target))
        {
            target.HitByLaser(hitPoint, hitNormal);
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

        if (_spawnedSpark != null && _spawnedSpark.isPlaying)
        {
            _spawnedSpark.Stop();
        }

        if (_lineRenderer != null)
            _lineRenderer.positionCount = 0;
    }
}
