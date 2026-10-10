using Cysharp.Threading.Tasks;
using OB.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckPointTrigger _trigger;

    [Header("Identity Settings")]
    [SerializeField] private int _checkPointID;


    [Header("Spawn Settings")]
    [SerializeField] private Transform _spawnPosition;


    [Header("Offset Settings")]
    [SerializeField] private Vector3 _offsetSpawnCam;


    [Header("Object References")]
    [SerializeField] private List<Plant> _plantsList;


    [Header("Delay Settings")]
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;

    public int CheckPointID => _checkPointID;
    public Transform SpawnPosition => _spawnPosition;
    public Vector3 OffsetSpawnCam => _offsetSpawnCam;
    private void Awake() => _trigger = GetComponentInChildren<CheckPointTrigger>();

    private void Start() => Initialize();
    private void OnEnable() 
    { 
        GameEvents.OnRoomTransitioned += GameEvents_OnRoomTransitioned;
        _trigger.OnPlayerEnterCheckPoint += OnPlayerEnterCheckPoint;
    }
    private void OnDisable()
    {
        GameEvents.OnRoomTransitioned -= GameEvents_OnRoomTransitioned;
    }
    private void Initialize()
    {
        if(_trigger.HasTriggered)
        {
            foreach (var plant in _plantsList)
            {
                plant.TogglePlant(true);
                plant.SetPlantSize(Vector3.one);
            }
        }
    }
    private void GameEvents_OnRoomTransitioned(int nextRoomID)
    {
        if (nextRoomID != _checkPointID)
            return;

        CheckPointManager.Instance.SetCheckPoint(this);
    }
    private void OnPlayerEnterCheckPoint()
    {
        PlayGrowPlantsAnimation().Forget();
    }

    private async UniTask PlayGrowPlantsAnimation()
    {
        var plants = _plantsList;

        plants.Shuffle();

        for (int i = 0; i < plants.Count; i++)
        {
            var delay = RandomUtils.GetRandomFloat(_minDelay, _maxDelay);

            plants[i].PlayGrowAnimation();

            await UniTask.Delay(TimeSpan.FromSeconds(delay));
        }
    }
}
