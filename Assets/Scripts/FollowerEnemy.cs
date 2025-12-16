using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemy : EnemyControllerFinal
{
    private SpriteRenderer sr2;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        sr2 = GetComponent<SpriteRenderer>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();
    }

    void FixedUpdate(){
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, maxSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player")
            FindObjectOfType<PlayerStatsFinal>().TakeDamage(damage);
        
    }

    void FacePlayer(){
        
        if (player == null) return;

        bool facingRight = !sr2.flipX;

        if (player.position.x < transform.position.x && facingRight){
            Flip();
        }else if (player.position.x > transform.position.x && !facingRight){
            Flip();
        }
    }
    
}

