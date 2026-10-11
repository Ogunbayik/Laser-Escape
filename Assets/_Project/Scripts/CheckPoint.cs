using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("Plant References")]
    public PlantGroup PlantGroup;


    [Header("Identity Settings")]
    [SerializeField] private int _checkPointID;


    [Header("Spawn Settings")]
    [SerializeField] private Transform _spawnPosition;


    [Header("Offset Settings")]
    [SerializeField] private Vector3 _offsetSpawnCam;

    public int CheckPointID => _checkPointID;
    public Transform SpawnPosition => _spawnPosition;
    public Vector3 OffsetSpawnCam => _offsetSpawnCam;
    private void OnEnable() 
    { 
        GameEvents.OnRoomTransitioned += GameEvents_OnRoomTransitioned;
    }
    private void OnDisable()
    {
        GameEvents.OnRoomTransitioned -= GameEvents_OnRoomTransitioned;
    }
    private void GameEvents_OnRoomTransitioned(int nextRoomID)
    {
        if (nextRoomID != _checkPointID)
            return;

        Debug.Log("Changed checkpoint!");
        CheckPointManager.Instance.SetCheckPoint(this);
    }
}
