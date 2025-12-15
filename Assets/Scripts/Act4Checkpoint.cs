using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act4Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    public CameraFollow cameraFollow;
    public GameObject Lava;

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            if (cameraFollow != null)
            {
              cameraFollow.maxX = 20;
              Destroy(Lava);
            }
            FindObjectOfType<LevelManager>().CurrentCheckpoint=this.gameObject;
        }
    }
}
