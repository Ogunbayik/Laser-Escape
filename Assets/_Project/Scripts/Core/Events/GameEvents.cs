using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnPlayerDeadSequenceCompleted;
    public static event Action<int> OnRoomTransitioned;

    public static void TriggerPlayerDeadSequenceCompleted() => OnPlayerDeadSequenceCompleted?.Invoke();
    public static void TriggerRoomTransition(int roomID) => OnRoomTransitioned?.Invoke(roomID);
}
