using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingQuestArrow : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float height = 0.25f;

    private float startY;

    void Start()
    {
        startY = transform.localPosition.y;
    }

    void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * floatSpeed) * height;
        transform.localPosition = new Vector3(transform.localPosition.x, newY, 0);
    }
}
