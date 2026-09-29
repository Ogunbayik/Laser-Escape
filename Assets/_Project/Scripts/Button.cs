using UnityEngine;

public class Button : MonoBehaviour, ILaserTargetable
{
    private bool _isOpened;

    public void HitByLaser()
    {
        if (_isOpened)
            return;

        _isOpened = true;
        Debug.Log("Opened Door!");
    }
    public void LaserLost()
    {
        if (!_isOpened)
            return;

        _isOpened = false;
        Debug.Log("Closed Door!");
    }
}
