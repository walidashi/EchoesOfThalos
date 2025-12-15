using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            FindObjectOfType<Act4PlayerStats>().TakeDamage(6);
        }
    }
}