using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LaserBeam : MonoBehaviour
{
    [Header("Transform References")]
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private Transform _visual;

    [Header("Laser Beam Settings")]
    [SerializeField] private float _maxDistance;
    [SerializeField] private int _maxReflections;

    [Header("Rotation Settings")]
    [SerializeField] private float _rotationDegree;
    [SerializeField] private float _rotationDuration;
    [SerializeField] private bool _isRight;

    [Header("Collision Settings")]
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private LayerMask _playerMask;

    private LineRenderer _lineRenderer;
    private List<Vector3> laserPoints = new List<Vector3>();

    private Vector3 _currentRotation;

    private ILaserTargetable _currentTarget;
    private bool _isRotating;
    private void Awake()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();
    }
    private void Start()
    {
        _currentRotation = transform.localRotation.eulerAngles;
    }
    void Update()
    {
        CastLaser();
    }
    private void CastLaser()
    {
        laserPoints.Clear();

        Vector3 currentPosition =  _shootPosition.position;
        Vector3 currentDirection = _visual.transform.forward;

        laserPoints.Add(currentPosition);

        ILaserTargetable hitTargetThisFrame = null;

        for (int i = 0; i < _maxReflections; i++)
        {
            CheckLaserSegment(currentPosition, currentDirection);

            if (Physics.Raycast(currentPosition, currentDirection, out RaycastHit hit, _maxDistance, _collisionMask))
            {
                // Çarpışma noktasını listeye ekle
                laserPoints.Add(hit.point);

                // Çarpan nesne bir ayna mı? (Tag kontrolü ile bakıyoruz)
                if (hit.collider.CompareTag("Mirror"))
                {
                    // Vector3.Reflect ile gelen açıyı yüzeyin normaline göre yansıt
                    currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                    currentPosition = hit.point; // Yeni ışın için başlangıç noktasını güncelle
                }
                else if(hit.collider.TryGetComponent<ILaserTargetable>(out ILaserTargetable target))
                {
                    hitTargetThisFrame = target;
                    target.HitByLaser();
                    break;
                }
                else
                {
                    // Duvara veya yansıtmayan bir objeye çarptıysa lazer burada biter
                    break;
                }
            }
            else
            {
                // Hiçbir şeye çarpmadıysa maksimum mesafeye kadar düz gider ve biter
                laserPoints.Add(currentPosition + (currentDirection * _maxDistance));
                break;
            }
        }


        if (_currentTarget != null && _currentTarget != hitTargetThisFrame)
        {
            _currentTarget.LaserLost();
        }

        _currentTarget = hitTargetThisFrame;

        _lineRenderer.positionCount = laserPoints.Count;
        _lineRenderer.SetPositions(laserPoints.ToArray());
    }
    public void RotateLaser()
    {
        if (_isRotating)
            return;

        _isRotating = true;
        float degree = _isRight ? _rotationDegree : -_rotationDegree;
        Vector3 targetRotation = _currentRotation + new Vector3(0f, degree, 0f);

        _visual.transform.DORotate(targetRotation, _rotationDuration).SetEase(Ease.InOutSine).OnComplete(() => OnRotationCompleted(targetRotation));
    }
    private void CheckLaserSegment(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hit, _maxDistance, _playerMask))
        {
            if (hit.transform.TryGetComponent<PlayerBase>(out PlayerBase player))
                player.Die();
        }
    }
    private void OnRotationCompleted(Vector3 targetRotation)
    {
        _currentRotation = targetRotation;
        _isRotating = false;
    }
}
