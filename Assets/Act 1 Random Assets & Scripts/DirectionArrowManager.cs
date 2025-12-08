using UnityEngine;
using UnityEngine.UI;

public class DirectionArrowManager : MonoBehaviour
{
    public Transform player;       // Flynn
    public static Transform target;       // Objective target that the arrows will point towards
    public Image leftArrow;        // UI Image of left arrow
    public Image rightArrow;       // UI Image of right arrow
    public GameObject wellArrow;

    float hideDistance = 3f; // distance in which arrows hide

    void Start()
    {
        HideAll();
        wellArrow.SetActive(false);
        GameObject.Find("watermillArrow").SetActive(false);
    }

    void Update()
    {
        if (player == null || target == null) return;

        float diff = player.position.x - target.position.x;

        // Close to target then hide arrows
        if (Mathf.Abs(diff) < hideDistance)
        {
            HideAll();
            return;
        }

        // Player left of target then point right
        if (player.position.x < target.position.x)
        {
            ShowRight();
        }
        // Player right of target then point left
        else
        {
            ShowLeft();
        }
    }

    void ShowLeft() //helper function
    {
        leftArrow.gameObject.SetActive(true);
        rightArrow.gameObject.SetActive(false);
    }

    void ShowRight() //helper function
    {
        rightArrow.gameObject.SetActive(true);
        leftArrow.gameObject.SetActive(false);
    }

    public void HideAll() //helper function
    {
        leftArrow.gameObject.SetActive(false);
        rightArrow.gameObject.SetActive(false);
    }
}
