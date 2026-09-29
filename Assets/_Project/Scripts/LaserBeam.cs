using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [Header("Transform References")]
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private Transform _visual;

    [Header("Laser Beam Settings")]
    [SerializeField] private float maxDistance;
    [SerializeField] private int _maxReflections;
    [SerializeField] private float _rotationDegree;
    [SerializeField] private float _rotationDuration;

    [Header("Collision Settings")]
    [SerializeField] private LayerMask collisionMask;

    private LineRenderer _lineRenderer;
    private List<Vector3> laserPoints = new List<Vector3>();

    private Vector3 _currentRotation;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
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
        Vector3 currentDirection = transform.forward;

        laserPoints.Add(currentPosition);

        for (int i = 0; i < _maxReflections; i++)
        {
            if (Physics.Raycast(currentPosition, currentDirection, out RaycastHit hit, maxDistance, collisionMask))
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
                else if(hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Hitted Player!");
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
                laserPoints.Add(currentPosition + (currentDirection * maxDistance));
                break;
            }
        }

        _lineRenderer.positionCount = laserPoints.Count;
        _lineRenderer.SetPositions(laserPoints.ToArray());
    }
    public void RotateLaser(bool isRight)
    {
        float degree = isRight ? _rotationDegree : -_rotationDegree;
        Vector3 targetRotation = _currentRotation + new Vector3(0f, degree, 0f);

        _visual.transform.DORotate(targetRotation, _rotationDuration).SetEase(Ease.InOutSine).OnComplete(() => _currentRotation = targetRotation);
    }
}
