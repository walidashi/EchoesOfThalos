using UnityEngine;

public class LavaCamera : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform lavaTransform;

    void FixedUpdate()
    {
        if (cameraFollow != null && lavaTransform != null)
        {
            cameraFollow.minY = lavaTransform.position.y +2.7f;
        }
    }
}
