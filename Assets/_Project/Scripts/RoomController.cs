using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("Room References")]
    [SerializeField] private List<Room> _roomsList;

    private Room _currentRoom;

    private int _roomIndex = 0;
    private void Start()
    {
        if (_roomsList == null || _roomsList.Count == 0) 
            return;

        _currentRoom = _roomsList[_roomIndex];
        _currentRoom.gameObject.SetActive(true);
        _currentRoom.Initialize();

        for (int i = 1; i < _roomsList.Count; i++)
        {
            _roomsList[i].gameObject.SetActive(false);
        }

        SubscribeToCurrentRoom();
    }
    private void OnDisable()
    {
        UnsubscribeFromCurrentRoom();
    }
    private void SubscribeToCurrentRoom()
    {
        if (_currentRoom != null && _currentRoom.PassTrigger != null)
        {
            _currentRoom.PassTrigger.OnPlayerPassedRoom += PlayerPassedRoom;
        }
    }

    private void UnsubscribeFromCurrentRoom()
    {
        if (_currentRoom != null && _currentRoom.PassTrigger != null)
        {
            _currentRoom.PassTrigger.OnPlayerPassedRoom -= PlayerPassedRoom;
        }
    }
    private void PlayerPassedRoom()
    {
        UnsubscribeFromCurrentRoom();

        _roomIndex++;

        if (_roomIndex >= _roomsList.Count)
        {
            Debug.Log("Tüm odalar tamamlandı!");
            return;
        }

        _currentRoom = _roomsList[_roomIndex];
        _currentRoom.gameObject.SetActive(true);
        _currentRoom.Initialize();

        SubscribeToCurrentRoom();
    }

}
