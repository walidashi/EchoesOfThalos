using UnityEngine;

public class LavaScript : MonoBehaviour
{
    private Vector3 initialPosition;
    public int damage = 2;
    public float growthRate = 0.5f;
    public SpriteRenderer Flynn;
    void Start(){
        initialPosition = transform.position;
    }
    void Update()
    {
        float amountToMove = growthRate * Time.deltaTime;
        Vector3 currentPosition = transform.position;
        currentPosition.y += amountToMove;
        transform.position = currentPosition;
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            FindObjectOfType<Act4PlayerStats>().TakeDamage(damage);
            Debug.Log("Damaged");
        }
    }
    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}