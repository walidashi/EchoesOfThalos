using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermillScript : MonoBehaviour
{
    public GameObject HUD; // the HUD object that has the health bar, etc that will disappear in cutscene.
    public GameObject interactUI; //interactive UI that tells the player some information
    public GameObject dialogueBox; //box that appears in dialogues 
    public TMPro.TextMeshProUGUI interactText; //the text that appears in interactive UI
    public TMPro.TextMeshProUGUI dialogueText; //the text that appears in dialogue UI box
    private bool checker = false; // a checker boolean variable that helps identify if everything is done so the cutscene can start.
    bool playerInRange = false; // boolean to identify if player is in range of the watermill or not
    public CutsceneManager cutscene; // the cutscene object to start the first cutscene
    public GameObject watermillArrow; // arrow that indicates to the player where the watermill is
    public GameObject SwordUI; // image of sword that appears when flynn takes it from the watermill
    public GameObject AbilityUIBox; //the box that appears to tell flynn more information about the ability he took (sword)
    public TMPro.TextMeshProUGUI AbilityText; //text that appears inside that box
    void Update()
    {
        if(checker){
        interactText.text = "Press C to try your new sword!";
        interactUI.SetActive(true);
        }

        if (playerInRange && QuestManager.currentQuest == "Watermill Reward")
        {
            // When player presses E inside trigger
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactUI.SetActive(false);
                dialogueBox.SetActive(true);
                PlayerController.inputBlocked = true;
                StartCoroutine(DialogueSequence());
                interactText.text = "Press E to interact.";
                GameObject.Find("watermillArrow").SetActive(false);
                DirectionArrowManager.target = null;
                StartCoroutine(AbilityUI());
            }
        }
        if (QuestManager.currentQuest == "Completed" && Input.GetKeyDown(KeyCode.C) && checker){
            interactUI.SetActive(false);
            interactText.text = "Press E to interact.";
            checker = false;
            StartCoroutine(CutsceneDelay());
            


        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && QuestManager.currentQuest == "Watermill Reward")
        {
            playerInRange = true;

            interactText.text = "Press E to receive your reward.";
            interactUI.SetActive(true);
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


    IEnumerator CutsceneDelay(){
        interactUI.SetActive(false);
        yield return new WaitForSeconds(3f);
        cutscene.StartCutscene();
        HUD.SetActive(false);
        PlayerController.inputBlocked = true;
        interactUI.SetActive(false);

    }

    IEnumerator DialogueSequence()
    {
        dialogueText.text = "Note says: 'Thank you Flynn! Here's a reward for your bravery.'";
        yield return new WaitForSeconds(3f);

        dialogueText.text = "You received the Sword of Thalos!";
        yield return new WaitForSeconds(3f);

        // Ability message
        dialogueText.text = "You can use the sword by pressing 'C' to attack enemies.";
        yield return new WaitForSeconds(3f);

        // Hide after sequence
        dialogueBox.SetActive(false);
        PlayerController.inputBlocked = false;

        // Complete quest
        QuestManager.currentQuest = "Completed";
        interactText.text = "Press C to try your new sword!";
        checker = true;
        interactUI.SetActive(true);
        
    }

    IEnumerator AbilityUI(){
        AbilityText.text = "Ability Unlocked: Sword of Thalos! Any other abilities will appear at the top-right.";
        AbilityUIBox.SetActive(true);
        StartCoroutine(FlashSwordUI());
        yield return new WaitForSeconds(10f);
        AbilityUIBox.SetActive(false);
        
    }

    IEnumerator FlashSwordUI() // for the sword in canvas to flash for a few seconds
    {
        for (int i = 0; i < 3; i++)
        {
            SwordUI.SetActive(true);
            yield return new WaitForSeconds(0.3f);

            SwordUI.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }

        SwordUI.SetActive(true); // leave it visible at the end
}
}
