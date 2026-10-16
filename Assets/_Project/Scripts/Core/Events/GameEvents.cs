using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnPlayerDeadSequenceCompleted;
    public static void TriggerPlayerDeadSequenceCompleted() => OnPlayerDeadSequenceCompleted?.Invoke();


    public static event Action OnCinematicStarted;
    public static void TriggerCinematicStarted() => OnCinematicStarted?.Invoke();

    public static event Action OnCinematicEnded;
    public static void TriggerCinematicEnded() => OnCinematicEnded?.Invoke();
}
