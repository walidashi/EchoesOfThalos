using UnityEngine;

public class LavaScript : MonoBehaviour
{
    public int damage = 1;
    public float growthRate = 0.5f;

    void Update()
    {
        float amountToMove = growthRate * Time.deltaTime;
        Vector3 currentPosition = transform.position;
        currentPosition.y += amountToMove;
        transform.position = currentPosition;
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
            Debug.Log("Damaged");
        }
    }
}