using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // Change "LevelManager" to "LevelManagerScene2"
            LevelManagerScene2 levelManager = FindObjectOfType<LevelManagerScene2>();
              PlayerStat playerStat = other.GetComponent<PlayerStat>();
            if (levelManager != null)
            {
                playerStat.lives--;
                levelManager.RestartLevel();
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
                );
            }
        }
    }
}