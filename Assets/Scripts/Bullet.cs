using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class Bullet : MonoBehaviour
{
    // Restricts the value of speed (Makes into a Slider)
    [Range(1, 50)]
    // Serialzed means that the value is written and stored in Unity's scene file
    // Non-serialized variables have dependant values
    [SerializeField] public float speed = 50f;
    [Range(1, 10)]
    [SerializeField] private float lifeTime = 2f;
    private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D cc; // Needs to be initialized before Start
    [SerializeField] private int bulletDamage = 1;
    // Keeps track of who the bullet belongs to
    public GameObject sender;
    public GameObject weapon;

    void Start(){
        rb = GetComponent<Rigidbody2D>();      
        // Kills itself after lifeTime
        Destroy(gameObject, lifeTime);
    }
    
    private void FixedUpdate(){
        // Moves forever right
        rb.linearVelocity = transform.right * speed;
    }

    // This is automatically called by Unity during collision
    void OnCollisionEnter2D(Collision2D collision){
        OnCollide(collision.collider);
    }

    // Collision with object
    public void OnCollide(Collider2D coll){
        // Hits player
        if(gameObject.CompareTag("EnemyBullet")){
            CoopPlayer player = coll.gameObject.GetComponent<CoopPlayer>();
            if (player){
                player.TakeDamage(gameObject, bulletDamage);
                // Does knockback to player
                 player.GetComponent<Knockback>().PlayFeedback(sender, false);
            }
        }
        // Hits enemy
        if(gameObject.CompareTag("Bullet")){
            Enemy enemy = coll.gameObject.GetComponent<Enemy>();
            if (enemy){
                enemy.TakeDamage(gameObject, bulletDamage);
                // Default knockback
                enemy.GetComponent<Knockback>().PlayFeedback(sender, false);
            }
        }
        Destroy(gameObject);
    }
    
    // Sets the sender of the bullet
    public void SetSender(GameObject sender, GameObject weapon){
        this.sender = sender;
        this.weapon = weapon;
        // Ignores collision with the object shooting the bullet
        Physics2D.IgnoreCollision(this.sender.GetComponent<BoxCollider2D>(), cc);
    }

}
