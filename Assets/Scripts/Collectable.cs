using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AudioClip KeyCollect;
    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            FindObjectOfType<Act4PlayerStats>().CollectKey();
            AudioManager.Instance.PlayMusicSFX(KeyCollect);
            Destroy(this.gameObject);
        }
    }
}