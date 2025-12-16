using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerFinal : MonoBehaviour
{
    public float maxSpeed;
    public int health;
    public int damage;
    public float jumpHeight;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    public Animator anim;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    public enum AbilityType
    {
        Attack,
        Attack2
    }

    public AbilityType currentAbility;

    
    public float idleTime;
    public float attackIntervalMin;
    public float attackIntervalMax;

    private int direction = 1;
    private bool isMoving = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.Play("Alan_Walk");

        StartCoroutine(AttackRoutine());
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    
        if (isMoving)
        {
            rb.velocity = new Vector2(direction * maxSpeed, rb.velocity.y);
            sr.flipX = direction < 0;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("Height", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
    }


    IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(attackIntervalMin, attackIntervalMax)
            );

            RandomAttack();
        }
    }

    void RandomAttack()
    {
        currentAbility = (AbilityType)Random.Range(0, 2);

        switch (currentAbility)
        {
            case AbilityType.Attack:
                anim.SetTrigger("AttackTrigger");
                break;

            case AbilityType.Attack2:
                anim.SetTrigger("AttackTrigger2");
                break;

        }
    }

    public void Flip(){
        sr.flipX =!sr.flipX;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            FindObjectOfType<PlayerStatsFinal>().TakeDamage(damage);
        }
    }


}
