using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WellScript : MonoBehaviour
{
    public Image waterBucket; // canvas image for water bucket 
    public Image bucket; // canvas image for empty bucket
    public GameObject dialogueBox; //box for indications and dialogues
    public TMPro.TextMeshProUGUI dialogueText; //text to put inside box
    bool playerInRange = false; //boolean value to indicate if player is in range of filling the well
    public GameObject wellArrow; // the arrow that is on top of the well that indicates for the player where the well is.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
        if (playerInRange && Input.GetKeyDown(KeyCode.E)){
            dialogueBox.SetActive(false);
            
            bucket.sprite = waterBucket.sprite; //swapping the sprite of empty bucket to the filled water bucket once flynn fills it
            StartCoroutine(FlashBucket());

            // Mark quest completed
            QuestManager.waterFetched = true;
            QuestManager.CompleteWaterQuest();
            playerInRange = false;  
            wellArrow.SetActive(false);
              }
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && QuestManager.currentQuest == "FetchWater" && !QuestManager.waterFetched){ // if quest is the fetch water one and flynn didnt already do it
            playerInRange = true;
             dialogueBox.SetActive(true);
            dialogueText.text = "Press E to fill the bucket with water.";
        }
    }

  void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && QuestManager.currentQuest == "FetchWater" && !QuestManager.waterFetched)
        {
            playerInRange = false;
            dialogueBox.SetActive(false);
        }
    }


IEnumerator FlashBucket() // helper function to indicate that the bucket has been filled
{
    for (int i = 0; i < 3; i++)
    {
        bucket.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        bucket.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
    }

    bucket.gameObject.SetActive(true); // leave it visible at the end


}
}

