using UnityEngine;
public struct LaunchData
{
    public Transform LaunchTransform; //Fırlatan objenin referansı
    public Vector3 targetPosition;
    public float launchPower;



    public LaunchData(Transform launchTransform, Vector3 targetPos, float launchPow)
    {
        LaunchTransform = launchTransform;
        targetPosition = targetPos;
        launchPower = launchPow;
    }

}