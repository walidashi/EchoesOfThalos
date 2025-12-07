using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    public static string currentQuest = "";
    public static bool waterFetched = false;

    public static void CompleteWaterQuest()
    {
        waterFetched = true;
        FindObjectOfType<HeadVillagerTalkScript>().CompleteQuest();
    }
}
