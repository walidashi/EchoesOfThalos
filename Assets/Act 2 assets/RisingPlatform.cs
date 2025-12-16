using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatform : MonoBehaviour
{
 public Transform PointA;
 public Transform PointB;
 public float speed;
 public bool AtoB;
 private Vector3 targetposition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(AtoB==true){
            targetposition=PointA.position;
        }
        else{
             targetposition=PointB.position;
        }
        Vector3 newPosition=transform.position;
        newPosition.y=Mathf.MoveTowards(transform.position.y,targetposition.y,speed*Time.deltaTime);
        transform.position=newPosition;
        if (Mathf.Abs(transform.position.y-targetposition.y)<0.1f)
        {
            AtoB=!AtoB;
        }
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag=="Player"){
            collision.transform.SetParent(transform);
        }
    }
}
