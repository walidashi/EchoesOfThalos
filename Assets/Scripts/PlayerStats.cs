using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static int health = 3; //Max Health is 6 (3 full hearts)
    public static int lives = 3;
    public static int score = 0;
    public static bool hasItem = false;

    public TextMeshProUGUI scoreUI;
    public HeartHealthUI heartUI;  

    private float flickerTime = 0f;
    public float flickerDuration = 0.1f;

    private SpriteRenderer sr;

    public bool isImmune = false;
    private float immunityTime = 0f;
    public float immunityDuration = 1.5f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Update UI on start
        heartUI.UpdateHearts(health);
    }

    public void TakeDamage(int damage)
    {
        if (!isImmune)
        {
            health -= damage;
            if (health < 0) health = 0;

            if (lives > 0 && health == 0)
            {
                health = 6; // reset to full hearts
                lives--;
            }
            else if (lives == 0 && health == 0)
            {
                Debug.Log("Gameover");
                Destroy(this.gameObject);
            }

            // Update heart UI
            heartUI.UpdateHearts(health);

            Debug.Log("Player Health:" + health);
            Debug.Log("Player Lives:" + lives);

            isImmune = true;
            immunityTime = 0f;
        }
    }

    void Update()
    {
        if (isImmune)
        {
            SpriteFlicker();
            immunityTime += Time.deltaTime;

            if (immunityTime >= immunityDuration)
            {
                isImmune = false;
                sr.enabled = true;
            }
        }

        scoreUI.text = score.ToString();
    }

    void SpriteFlicker()
    {
        flickerTime += Time.deltaTime;

        if (flickerTime >= flickerDuration)
        {
            sr.enabled = !sr.enabled;
            flickerTime = 0;
        }
    }
}
