using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WellScript : MonoBehaviour
{
    public Image waterBucket;
    public Image bucket;
    public GameObject dialogueBox;
    public TMPro.TextMeshProUGUI dialogueText;
    bool playerInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
        if (playerInRange && Input.GetKeyDown(KeyCode.E)){
            dialogueBox.SetActive(false);
                   // Swap the sprite
            bucket.sprite = waterBucket.sprite;

            // Mark quest completed
            QuestManager.waterFetched = true;
            QuestManager.CompleteWaterQuest();
            playerInRange = false;  
              }
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && QuestManager.currentQuest == "FetchWater" && !QuestManager.waterFetched){
            playerInRange = true;
             dialogueBox.SetActive(true);
            dialogueText.text = "Press E to fill the bucket with water.";
        }
    }

  void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogueBox.SetActive(false);
        }
    }

}
