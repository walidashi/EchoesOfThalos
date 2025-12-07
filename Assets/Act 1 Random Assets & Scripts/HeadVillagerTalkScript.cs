using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeadVillagerTalkScript : MonoBehaviour
{
    public GameObject interactUI;  // Press E icon or popup
    public GameObject dialogueBox; // Canvas panel for dialogue text
    public GameObject questArrow;
    public GameObject bucket;
    public TMPro.TextMeshProUGUI dialogueText;

    private bool playerInRange = false;
    private bool questGiven = false;
    private bool questCompleted = false;

    void Start()
    {
        bucket.SetActive(false);
        
        interactUI.SetActive(false);
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            interactUI.SetActive(false);
            TalkToPlayer();
        }
    }

    void TalkToPlayer()
    {
        dialogueBox.SetActive(true);

        if (!questGiven)
        {
            dialogueText.text = "Flynn, the village needs your help. Please bring water from the well.";
            questGiven = true;
            questArrow.SetActive(false);
            StartCoroutine(FlashBucket());
            StartCoroutine(DialogueDelay("Fetch the water and come back."));
            QuestManager.currentQuest = "FetchWater";
        }
        else if (questGiven && !questCompleted)
        {
            dialogueText.text = "Have you fetched the water from the well yet?";
        }
        else if (questCompleted)
        {
            dialogueText.text = "Thank you, Flynn. You have helped the village greatly!";
            questArrow.SetActive(false);
            bucket.SetActive(false);
        }

        // Hide dialogue after 6 seconds 
        Invoke("CloseDialogue", 6f);
    }

    void CloseDialogue()
    {
        dialogueBox.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactUI.SetActive(true); // Show "Press E"
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactUI.SetActive(false); 
            dialogueBox.SetActive(false);
        }
    }

    // Called when quest completes from QuestManager
    public void CompleteQuest()
    {
        questCompleted = true;
        questArrow.SetActive(true);
    }

IEnumerator DialogueDelay(string message){
    yield return new WaitForSeconds(3f);
                dialogueText.text = message;
    yield return new WaitForSeconds(3f);


}

IEnumerator FlashBucket()
{
    for (int i = 0; i < 3; i++)
    {
        bucket.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        bucket.SetActive(false);
        yield return new WaitForSeconds(0.3f);
    }

    bucket.SetActive(true); // leave it visible at the end
}

}
