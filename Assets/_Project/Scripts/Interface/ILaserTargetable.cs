using UnityEngine;

public interface ILaserTargetable 
{
    void HitByLaser(Vector3 hitPoint, Vector3 hitNormal);
    void LaserLost();
}
