using System;
using UnityEngine;

public static class InteractionEvents
{
    public static event Action<int, bool> OnButtonTriggered;
    public static void ButtonTrigger(int id, bool isTrigger) => OnButtonTriggered?.Invoke(id, isTrigger);
}
