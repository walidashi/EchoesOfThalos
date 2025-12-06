using UnityEngine;

public class LavaScript : MonoBehaviour
{
    [Tooltip("How much to increase the Y position (pivot) by, per second.")]
    public float growthRate = 0.5f;

    void Update()
    {
        // Move the object up by growthRate units per second
        float amountToMove = growthRate * Time.deltaTime;
        Vector3 currentPosition = transform.position;
        currentPosition.y += amountToMove;
        transform.position = currentPosition;
    }
}