using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public KeyCode Spacebar;
    public KeyCode L;
    public KeyCode R;
    public KeyCode Attack;

    public static bool inputBlocked = false;
    public static bool cutsceneWalking = false;


    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {



        

if (inputBlocked) {
    
        if (!cutsceneWalking){
        // Stop all movement
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // FORCE idle animation
        anim.SetFloat("Speed", 0);
        anim.SetFloat("Height", 0);
        anim.SetBool("Grounded", true);
        }
return;
        
}


else{
        if(Input.GetKeyDown(Attack)){
            anim.SetTrigger("AttackTrigger");
        }
        
        if(Input.GetKeyDown(Spacebar) && grounded){
            Jump();
        }

        if(Input.GetKey(L)){
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if(GetComponent<SpriteRenderer>()!= null){
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }


        if(Input.GetKey(R)){
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if(GetComponent<SpriteRenderer>()!= null){
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        anim.SetFloat("Speed",Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetFloat("Height", GetComponent<Rigidbody2D>().velocity.y);
        anim.SetBool("Grounded", grounded);


        void Jump(){
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        }
}
    }

    void FixedUpdate(){
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    }
}
