using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OpenCage : MonoBehaviour
{
    public TextMeshProUGUI DText; 
    public GameObject DialogeBox;
    public GameObject OpenedCage;
    public KeyCode Unlock;
    void Update()
    {
        if(Input.GetKeyDown(Unlock) && FindObjectOfType<Act4PlayerStats>().KeyCount == 1){
                OpenedCage.SetActive(true);
                Destroy(this.gameObject);
                SceneManager.LoadScene("Act 5");
                NextLevel();
        }
    }
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(3.0f);
        Debug.Log("Level Complete!");
        SceneManager.LoadScene("Act 5");
    }

   void OnTriggerEnter2D(Collider2D other){
        if (FindObjectOfType<Act4PlayerStats>().KeyCount == 1)
        {
            DText.text = "Press E to open cage with key!";
            DialogeBox.SetActive(true);
        }
        else
        {
            DText.text = "Get key first to open cage!";
            DialogeBox.SetActive(true);
        }
    }
}
