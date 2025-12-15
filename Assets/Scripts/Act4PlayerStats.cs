using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Act4PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI KeyText; 

    private int KeyCount = 0;
    public GameOverScreen GO;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

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

    public void CollectKey()
    {
        KeyCount += 1;
        
        // The core line: change the .text property
        // The .ToString() method converts the number into displayable text.
        KeyText.text = KeyCount.ToString(); 
    }
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
            if (health < 0) health = 0;

            if (lives > 0 && health == 0)
            {
                health = 6; 
                lives--;
                if(lives == 2) heart3.SetActive(false);
                else if(lives == 1) heart2.SetActive(false);
                else if(lives == 0) heart3.SetActive(false);
                FindObjectOfType<LevelManager>().RespawnPlayer();
                FindObjectOfType<LavaScript>().ResetPosition();
            }
            else if (lives == 0 && health == 0)
            {
                GO.Setup();
                Destroy(this.gameObject);
                FindObjectOfType<LavaScript>().growthRate = 0;

            }

            Debug.Log("Player Health:" + health);
            Debug.Log("Player Lives:" + lives);
            setHealth(health);

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
