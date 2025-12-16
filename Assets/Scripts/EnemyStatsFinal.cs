using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatsFinal : MonoBehaviour
{
    public int health =3;
    private SpriteRenderer srv;
    public Image healthBar;
    
    
    void Start()
    {
        srv = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        
    }

     public void TakeDamageEnemy(int damage){
            health = health - damage;
            healthBar.fillAmount = this.health/3f;
            if (health <= 0){
                health = 0;
                Debug.Log("You Win!");
                Destroy(this.gameObject);
            }
            Debug.Log("Enemy Health: " + health.ToString());
        }
}
