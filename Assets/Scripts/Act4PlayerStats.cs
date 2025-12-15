using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Act4PlayerStats : MonoBehaviour
{
    public GameOverScreen GO;
    public int health = 6;
    public int lives = 3;
    public Slider slider;
    public static int score = 0;
    public static bool hasItem = false;

    private float flickerTime = 0f;
    public float flickerDuration = 0.1f;

    private SpriteRenderer sr;

    public bool isImmune = false;
    private float immunityTime = 0f;
    public float immunityDuration = 1.5f;

    public void setMaxHealth(int Health)
    {
        slider.maxValue = Health;
        slider.value = Health;        
    }

    public void setHealth(int Health)
    {
        slider.value = Health;       
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        setMaxHealth(health);
    }

    public void TakeDamage(int damage)
    {
        if (!isImmune)
        {
            health -= damage;
            setHealth(health);
            if (health < 0) health = 0;

            if (lives > 0 && health == 0)
            {
                health = 6; 
                lives--;
            }
            else if (lives == 0 && health == 0)
            {
                GO.Setup();
                Destroy(this.gameObject);
                FindObjectOfType<LavaScript>().growthRate = 0;

            }

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
