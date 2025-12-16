using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerStatsFinal : MonoBehaviour
{
    public int health =3;
    public int lives = 3;
    public static int score;
    private float flickerTime = 0f;
    public float flickerDuration = 0.1f;
    private SpriteRenderer sr;
    public bool isImmune = false;
    private float immunityTime = 0f;
    public float immunityDuration = 1.5f;
    public Image healthBar;
    public TextMeshProUGUI LiveCounter;
    public TextMeshProUGUI scoreUI;
    public PlayerController playerController;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }


    void Update()
    {
        scoreUI.text = " " + score;
        if(isImmune == true){
            SpriteFlicker();
            immunityTime = immunityTime + Time.deltaTime;
            if(immunityTime >= immunityDuration){
                isImmune = false;
                sr.enabled = true;
            }
        }
        LiveCounter.text = " " + lives;
    }

    void SpriteFlicker(){
        if(flickerTime < flickerDuration){
            flickerTime = flickerTime + Time.deltaTime;
        }
        else if(flickerTime >= flickerDuration){
            sr.enabled = !(sr.enabled);
            flickerTime = 0;
        }
    }

     public void TakeDamage(int damage){
        if (playerController != null && playerController.isBlocking){
            Debug.Log("Blocked Damage!");
            playerController.anim.SetTrigger("Earth");
            return;
        }
        
        if(isImmune == false){
            health = health - damage;
            healthBar.fillAmount = this.health/5f;
            if (health < 0)
                health = 0;
            if (lives > 0 && health == 0){
                FindObjectOfType<LevelManager>().RespawnPlayer();
                health = 3;
                healthBar.fillAmount = this.health/5f;
                lives--;
            }
            else if(lives == 0 && health == 0){
                Debug.Log("Gameover");
                Destroy(this.gameObject);
            }
            Debug.Log("Player Health: " + health.ToString());
            Debug.Log("Player Lives: " + lives.ToString());
        }
        isImmune = true;
        immunityTime = 0f;
    } 
}
