using UnityEngine;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public Transform flynn; // main character
    public Transform headVillager; //the character that will be included in dialogue

    public GameObject normalBG1; //first Backgrounds that are pre eruption
    public GameObject normalBG2;

    public GameObject orangeBG1; //second backgrounds that appear to indicate something is wrong
    public GameObject orangeBG2;

    public GameObject eruptionBG1; //third backgrounds that appear post eruption or whilst eruption
    public GameObject eruptionBG2;


    public GameObject FlynnCanvas; //the flynn ui image that indicates he is talking in dialogue
    public GameObject HVCanvas; //the headvillager ui image that indicates he is talking in dialogue

    public GameObject dialogueBox; // the box that will include the dialogue
    public TMPro.TextMeshProUGUI dialogueText; // the text that will be included inside dialogue.

    public float walkSpeed = 2f; // the speed that flynn will walk with once the cutscene starts

    public static bool cutscenePlaying = false; // boolean variable to make sure nothing interrupts the cutscene.

    private Rigidbody2D rb; //rigidbody object for freezing gravity while in cutscene
    private float originalGravity; //to re establish gravity after cutscene


    public void StartCutscene()
    {
        
        if (!cutscenePlaying)
            AudioManager.Instance.PlayMusic(AudioManager.Instance.caveMusic);
            InitializeRB();
            StartCoroutine(CutsceneSequence());
    }

    IEnumerator CutsceneSequence()
    {
        PlayerController.inputBlocked = true;
        cutscenePlaying = true;
        DirectionArrowManager.target = null;

        yield return StartCoroutine(ScreenFader.instance.FadeOut(1f)); //indicator that Cutscene is starting
        yield return StartCoroutine(ScreenFader.instance.FadeIn(1f));
                rb.gravityScale = 0;

        //flynn starts walking towards the head villager
        yield return StartCoroutine(AutoWalkTo(flynn, headVillager.position.x+ 3f));

             //fade to black
        yield return StartCoroutine(ScreenFader.instance.FadeOut(1f));

        // switch to orange background
        normalBG1.SetActive(false);
        normalBG2.SetActive(false);
        orangeBG1.SetActive(true);
        orangeBG2.SetActive(true);

        // fade back to show the bg
        yield return StartCoroutine(ScreenFader.instance.FadeIn(1f));



        //first dialogue
        dialogueBox.SetActive(true);
        HVCanvas.SetActive(true);
        dialogueText.text = "Flynn... is this normal?";
        yield return new WaitForSeconds(3f);

        dialogueText.text = "We must prepare.. I fear the worst";
        yield return new WaitForSeconds(3f);
        HVCanvas.SetActive(false);
        dialogueBox.SetActive(false);

        //Fade to black again
        yield return StartCoroutine(ScreenFader.instance.FadeOut(1f));

        //switch to eruption background
        orangeBG1.SetActive(false);
        orangeBG2.SetActive(false);
        eruptionBG1.SetActive(true);
        eruptionBG2.SetActive(true);

        //Fade back to show the bg
        yield return StartCoroutine(ScreenFader.instance.FadeIn(1f));

        //final dialogue
        dialogueBox.SetActive(true);
        HVCanvas.SetActive(true);
        dialogueText.text = "The volcano... it's erupting!";
        yield return new WaitForSeconds(3f);
        dialogueText.text = "To safety, Flynn! We must leave the village at once!";
        yield return new WaitForSeconds(3f);
        HVCanvas.SetActive(false);
        FlynnCanvas.SetActive(true);
        dialogueText.text = "Don't worry, Samuel! I will save everyone!";
        yield return new WaitForSeconds(3f);
        dialogueBox.SetActive(false);
        FlynnCanvas.SetActive(false);

        //flynn auto-walks right to exit level
        yield return StartCoroutine(AutoWalkRight(flynn, 20f)); // move 5 units right

        //level ends here
        Debug.Log("Level 1 Complete!");
        
        // SceneManager.LoadScene("Level2");

        cutscenePlaying = false;
        PlayerController.inputBlocked = false;
        rb.gravityScale = originalGravity;
    }

    IEnumerator AutoWalkTo(Transform character, float targetX)
    {float y = character.position.y; // lock Y

  PlayerController.cutsceneWalking = true;   // allow animation during cutscene

  Animator anim = character.GetComponent<Animator>();
    SpriteRenderer sr = character.GetComponent<SpriteRenderer>();

    // figure out which direction to walk (left = -1, right = +1)
    float dir = Mathf.Sign(targetX - character.position.x);

    // Flip sprite based on direction.
    // flipX = true --> left
    if (sr != null)
        sr.flipX = (dir < 0);

    if (anim != null)
        anim.SetFloat("Speed", walkSpeed); // play walking / running anim

while (Mathf.Abs(character.position.x - targetX) > 0.1f)
{
    character.position = new Vector3(
        character.position.x + walkSpeed * Time.deltaTime,
        y,
        character.position.z
    );
    yield return null;
}
    anim.SetFloat("Speed", 0);  // stop walking animation
    PlayerController.cutsceneWalking = false;
    }

IEnumerator AutoWalkRight(Transform character, float distance)
{
    float startX = character.position.x;
    float y = character.position.y;

    PlayerController.cutsceneWalking = true;

    Animator anim = character.GetComponent<Animator>();
    SpriteRenderer sr = character.GetComponent<SpriteRenderer>();

    float dir = 1f; // always to the right

    if (sr != null)
        sr.flipX = false; //false = facing right

    if (anim != null)
        anim.SetFloat("Speed", walkSpeed);

    while (character.position.x < startX + distance)
    {
        character.position = new Vector3(
            character.position.x + dir * walkSpeed * Time.deltaTime,
            y,
            character.position.z
        );
        yield return null;
    }

    if (anim != null)
        anim.SetFloat("Speed", 0f);

    PlayerController.cutsceneWalking = false;
}



  void InitializeRB() //rigid body object helper function for gravity settings inside cutscene
{
    if (flynn != null)
    {
        rb = flynn.GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
    }
}

}
