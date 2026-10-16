using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckPointTrigger _trigger;

    [Header("Plant References")]
    public PlantGroup _plantGroup;


    [Header("Spawn Settings")]
    [SerializeField] private Transform _spawnPosition;


    [Header("Offset Settings")]
    [SerializeField] private Vector3 _offsetSpawnCam;

    public Transform SpawnPosition => _spawnPosition;
    public Vector3 OffsetSpawnCam => _offsetSpawnCam;
    private void Awake() => _trigger = GetComponentInChildren<CheckPointTrigger>();
    private void OnEnable()
    {
        _trigger.OnPlayerEnterCheckPoint += OnPlayerEnterCheckPoint;
    }
    private void OnDisable()
    {
        _trigger.OnPlayerEnterCheckPoint -= OnPlayerEnterCheckPoint;
    }
    private void OnPlayerEnterCheckPoint()
    {
        CheckPointManager.Instance.SetCheckPoint(this);
        _plantGroup.PlayGrowPlantsAnimation().Forget();
    }
}
