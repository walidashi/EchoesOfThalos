using UnityEngine;

public class Collectable1 : MonoBehaviour
{
    public AudioClip Key;
    void OnTriggerEnter2D(Collider2D other){

        if (other.tag == "Player"){
                  PlayerStat.score++;
                  AudioManager.Instance.PlayRandomSFX(Key);
                  Debug.Log("Score"+PlayerStat.score);
                  Destroy(gameObject);
        }
    }
    
}