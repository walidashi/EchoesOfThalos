using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;   // Platform stays still at start
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Invoke("DropPlatform", 0.3f);
            Destroy(gameObject, 2f);
        }
    }

    void DropPlatform()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;  // Now gravity affects it
    }
}

