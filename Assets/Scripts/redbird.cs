using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redbird : MonoBehaviour
{
    private Rigidbody2D red;
    private CircleCollider2D circleCollider;
    private bool launched;
    private bool faceVelocityDir;
    private void Awake(){
        red=GetComponent<Rigidbody2D>();
        circleCollider=GetComponent<CircleCollider2D>();
        
    }
    private void Start() {
        red.isKinematic=true;
        circleCollider.enabled=false;
    }
    private void FixedUpdate() {
        if (launched && faceVelocityDir){
        transform.right=red.velocity;}
    }
    public void Launch(Vector2 dir, float force){
        red.isKinematic=false;
        circleCollider.enabled=true;
        red.AddForce(dir*force,ForceMode2D.Impulse);
        launched=true;
        faceVelocityDir=true;
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        faceVelocityDir=false;
    }
}
