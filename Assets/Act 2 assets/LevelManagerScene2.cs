using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScene2 : MonoBehaviour
{
    public GameObject CurrentCheckpoint;
    public Transform player;

    void Start()
    {
        CurrentCheckpoint = null;
    }

    void Update()
    {
        
    }
    public void RespawnPlayer(){
        FindObjectOfType<PlayerController>().transform.position = CurrentCheckpoint.transform.position;
    }
    public void RestartLevel()
{
    UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
    );
}
}
