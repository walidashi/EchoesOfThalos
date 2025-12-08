using UnityEngine;
using UnityEngine.UI;

public class UIFloatArrow : MonoBehaviour //script to have some type of animation for the arrow (to move whilst in position) for better design and the player will see it more clearly
{
    Vector3 startPos;
    public float amplitude = 10f;
    public float speed = 3f;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float y = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = new Vector3(startPos.x, y, 0);
    }
}
