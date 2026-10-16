using UnityEngine;

public class InteractableLaser : BaseLaserController
{
    [Header("Identity Settings")]
    [SerializeField] private int _laserID;

    private void OnEnable() => InteractionEvents.OnButtonTriggered += InteractionEvents_OnButtonTriggered;   
    private void OnDisable() => InteractionEvents.OnButtonTriggered -= InteractionEvents_OnButtonTriggered;
    protected override void Start()
    {
        base.Start();
    }
    private void InteractionEvents_OnButtonTriggered(int buttonID, bool isTriggered)
    {
        if (buttonID != _laserID)
            return;

        SetLaserBeamActivateStatus(!isTriggered);
    }
}
