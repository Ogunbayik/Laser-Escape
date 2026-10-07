using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnPlayerDeadSequenceCompleted;

    public static void TriggerPlayerDeadSequenceCompleted() => OnPlayerDeadSequenceCompleted?.Invoke();
}
