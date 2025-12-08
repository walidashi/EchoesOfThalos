using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeadVillagerTalkScript : MonoBehaviour
{
    public GameObject interactUI;  // popup that appears before interacting
    public GameObject dialogueBox; // popup box that apperas while in dialogue
    public GameObject questArrow; // arrow that is on top of the headvillager that indicates that the player needs to talk to him
    public GameObject bucket; // canvas bucket image that the head villager will give flynn in order for flynn to fill the bucket with water
    public TMPro.TextMeshProUGUI dialogueText; // the text that apperas inside dialogue box
    public GameObject wellArrow; // the arrow that indicates flynn where the well is
    public GameObject watermillArrow; // the arrow that indicates for flynn where the watermill is
    public GameObject SwordUI; // the ability icon that apperas once flynn acquires the sword ability
    public GameObject AbilityUIBox; //a box that appears beside abilities when gained that is a brief description of the ability
    public GameObject AbilityText; // the text that appears inside the box
    public GameObject FlynnCanvas; //Canvas image of flynn that appears when he is talking in dialogue
    public GameObject HVCanvas; //Canvas image of head villager samuel that appears when he is talking in dialogue

    private bool playerInRange = false; // boolean indicator that player is in Range of talking to the headvillager
    private bool questGiven = false; // boolean indicator if the quest was given by head villager samuel or not
    private bool questCompleted = false; // boolean indicator if flynn already completed the quest or not

    void Start()
    {
        StartCoroutine(FirstInteractionTutorial()); 
        DirectionArrowManager.target = this.transform; // first quest , this is to indicate that the arrow will point towards the head villager 
        //a few start commands that hide unwanted objects yet to later make them appear in the right time.
        bucket.SetActive(false); 
        interactUI.SetActive(false);
        GameObject.Find("OrangeBG1").SetActive(false);
        GameObject.Find("OrangeBG2").SetActive(false);
        GameObject.Find("eruptionBG1").SetActive(false);
        GameObject.Find("eruptionBG2").SetActive(false);
        SwordUI.SetActive(false);
        AbilityUIBox.SetActive(false);
        FlynnCanvas.SetActive(false);
        HVCanvas.SetActive(false);

    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            interactUI.SetActive(false);
            TalkToPlayer();
        }
    }

    void TalkToPlayer() // function that has first dialogue of the game
    {
        dialogueBox.SetActive(true);

        if (!questGiven && !questCompleted) // if quest is not given
        {   
            PlayerController.inputBlocked = true;
            HVCanvas.SetActive(true);
            dialogueText.text = "Flynn, the village needs your help. Please bring water from the well.";
            questGiven = true;
            questArrow.SetActive(false);
            StartCoroutine(FlashBucket());
            StartCoroutine(DialogueDelay("Fetch the water and come back."));
            QuestManager.currentQuest = "FetchWater";
            DirectionArrowManager.target = GameObject.Find("well").transform;
            wellArrow.SetActive(true);


        }
        else if (questGiven && !questCompleted) // if quest has been given but not yet completed and flynn went to talk to the headvillager again
        {
            HVCanvas.SetActive(true);
            dialogueText.text = "Have you fetched the water from the well yet?";
            HVCanvas.SetActive(false);
        }
        else if (questCompleted && QuestManager.currentQuest != "Completed" && QuestManager.currentQuest != "Watermill Reward") //if first quest is completed, give second quest.
        {
            PlayerController.inputBlocked = true;
            HVCanvas.SetActive(true);
            dialogueText.text = "Thank you, Flynn. You have helped the village greatly!";
            StartCoroutine(DialogueDelay("Now go to the watermill. You'll find a small reward for all your efforts!"));
            QuestManager.currentQuest = "Watermill Reward";


            DirectionArrowManager.target = watermillArrow.transform;
            questArrow.SetActive(false);
            bucket.SetActive(false);
            watermillArrow.SetActive(true);
            }
            else{
                HVCanvas.SetActive(true);
            dialogueText.text = "Looking sharp today, Flynn!";
            }

        // Hide dialogue after 6 seconds 
        Invoke("CloseDialogue", 6f);
    }

    void CloseDialogue()
    {
        dialogueBox.SetActive(false);
            HVCanvas.SetActive(false);

    }

    void OnTriggerEnter2D(Collider2D other) //function that shows the player a UI with text that tells him how to talk to the head villager
    {
        if (other.CompareTag("Player") && QuestManager.currentQuest != "Tutorial") // so the player while in the first interactive tutorial doesn't conflict with this interactUI
        {
            if (QuestManager.currentQuest != "Completed"){
            playerInRange = true;
            interactUI.SetActive(true); // Show "Press E"
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && QuestManager.currentQuest != "Tutorial") // same as above
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

IEnumerator FirstInteractionTutorial(){ //introductary tutorial to tell the player how to move and what to do.
            dialogueBox.SetActive(true);
            dialogueText.text = "Use arrows to move around.";
    yield return new WaitForSeconds(2f);
            dialogueText.text = "Press Space to jump.";
    yield return new WaitForSeconds(2f);
            dialogueText.text = "Follow the arrows on your screen to navigate through \nAct I.";
    yield return new WaitForSeconds(2f);
            dialogueText.text = "Good luck on your journey!";
    yield return new WaitForSeconds(2f);

    dialogueBox.SetActive(false);
    QuestManager.currentQuest = "";
}

IEnumerator DialogueDelay(string message){ // a helper function for including delays in dialogues
    yield return new WaitForSeconds(3f);
                dialogueText.text = message;
    yield return new WaitForSeconds(3f);
            PlayerController.inputBlocked = false;
            HVCanvas.SetActive(false);


}

IEnumerator FlashBucket() // a helper function that makes the bucket flash when flynn takes it from the head villager samuel
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
