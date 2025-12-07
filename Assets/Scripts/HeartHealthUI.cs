using UnityEngine;
using UnityEngine.UI;

public class HeartHealthUI : MonoBehaviour
{
    public Image[] hearts;           // assign Heart1, Heart2, Heart3
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;


    void Start()
    {
    
    }
public void UpdateHearts(int health)
{
    for (int i = 0; i < hearts.Length; i++)
    {
        int heartHealth = Mathf.Clamp(health - (i * 2), 0, 2);

        if (heartHealth == 2)
            hearts[i].sprite = fullHeart;
        else if (heartHealth == 1)
            hearts[i].sprite = halfHeart;
        else
            hearts[i].sprite = emptyHeart;
    }
}

}
