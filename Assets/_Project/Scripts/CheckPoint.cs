using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("Identity Settings")]
    [SerializeField] private int _checkPointID;


    [Header("Spawn Settings")]
    [SerializeField] private Transform _spawnPosition;

    public int CheckPointID => _checkPointID;
    public Transform SpawnPosition => _spawnPosition;
    private void OnEnable() => GameEvents.OnRoomTransitioned += GameEvents_OnRoomTransitioned;
    private void OnDisable() => GameEvents.OnRoomTransitioned -= GameEvents_OnRoomTransitioned;
    private void GameEvents_OnRoomTransitioned(int nextRoomID)
    {
        if (nextRoomID != _checkPointID)
            return;

        CheckPointManager.Instance.SetCheckPoint(this);
    }
   
}
