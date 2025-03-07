using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    protected GameObject GameManager;
    // The movement speed
    public float moveSpeed = 5f;
    // The speed at which is rotates toward its target
    public float rotateSpeed = 0.005f;
    protected Rigidbody2D rb;
    protected RaycastHit2D hit;
    protected BoxCollider2D boxcollider;
    // The explosion particle system
    [SerializeField] protected GameObject explosion;

    // The target object variables (player)
    protected GameObject target;
    protected Vector3 targetPosition;

    // Health and damage
    [SerializeField] protected int healthPoints = 1;
    // The damage it deals
    protected int touchDamage = 1;

    // Drops
    public GameObject[] drops;
    // The index of what drops it can drop
    private List<int> availableIndexes;
    // The chance out of 10 to drop an item when killed
    protected int dropChance = 8;
    public bool hasDrops = true;

    // Audio
    public AudioSource hurt;

    protected virtual void Start(){
        rb = GetComponent<Rigidbody2D>();
        // For the magnetic enemy
        if (boxcollider == null){
            boxcollider = GetComponent<BoxCollider2D>();
        }
        // Turns off drops for multiplayer
        GameManager = GameObject.Find("Game Manager");
        // Drops are initialzed
    
    }

    protected virtual void Update(){
        // Only sets target if unpaused and functional
        if(!LevelManager.isPaused && LevelManager.isFunctional){
            // If no target, search for target+
            if (!target){
                SetTarget();
            }
        }
    }

    protected virtual void FixedUpdate(){
        var step = moveSpeed * Time.deltaTime;
        // Game must be unpaused and functional
        if(!LevelManager.isPaused && LevelManager.isFunctional){
            if (target){
                targetPosition = target.GetComponent<Rigidbody2D>().transform.position;
                Vector2 movement = Vector2.MoveTowards(rb.position, targetPosition, step);
                // Move towards the target in y direction
                hit = Physics2D.BoxCast(rb.position, boxcollider.size, 0, new Vector2(0, movement.y), Math.Abs(movement.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
                // If it hit nothing, then move
                if (hit.collider == null || hit.collider.CompareTag("Drop")){
                    Move(movement);
                }
                
                // Move towards the target in X direction
                hit = Physics2D.BoxCast(rb.position, boxcollider.size, 0, new Vector2(movement.x, 0), Math.Abs(movement.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
                if (hit.collider == null || hit.collider.CompareTag("Drop")){
                    Move(movement);
                }
            }
        }

    }

    // Moves the enemy to the position
    protected void Move(Vector2 movement){
        // Moves enemy in a direction
        rb.position = movement;
    }

    // Sets the target to the Player object. Default value is Player
    protected void SetTarget(string tag="Player"){
        GameObject[] tagObjects = GameObject.FindGameObjectsWithTag(tag);
        if (tagObjects != null && tagObjects.Length > 0){
            // Choose
            GameObject tagObject = tagObjects[Random.Range(0, tagObjects.Length)];
            // Checks if the object is not null before setting it as the target
            if (tagObject != null){
                target = tagObject;
                targetPosition = target.GetComponent<Rigidbody2D>().transform.position;
            }
        }
        else{
            LevelManager.GameOver();
        }
        
    }

    // This is automatically called by Unity during collision
    void OnCollisionEnter2D(Collision2D collision){
        OnCollide(collision.collider);
    }

    // Collision events
    protected virtual void OnCollide(Collider2D coll){
        // Gets the Player
        CoopPlayer player = coll.gameObject.GetComponent<CoopPlayer>();
        // If player is null, then coll was not a player object
        if (player != null){
            // Hurts player
            player.TakeDamage(gameObject, touchDamage);
            player.GetComponent<Knockback>().PlayFeedback(gameObject, false);
            target = null;
        }
    }

    // Instant kills the Enemy
    public void TakeDamage(){
        // Drops the drop
        GameObject drop = GetDrop();
        if(drop != null){
            Instantiate(drop, transform.position, transform.rotation);
        }
        // Creates the particle effect, slightly higher
        GameObject particles = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.rotation);
        // Death
        Destroy(gameObject);
        // Destroys particles after 5 seconds
        Destroy(particles, 5f);
    }

    // Remove health points from the Enemy
    public virtual void TakeDamage(GameObject sender, int damage){
        healthPoints -= damage;
        if (healthPoints <= 0){
            TakeDamage();
        }
    }

    // Returns a needed drop
    public GameObject GetDrop(){
        // Checks to see if drops are possible
        if (hasDrops == false || drops == null || drops.Count() == 0){
            return null;
        }
        // The drop gameobject
        GameObject final = null;
        System.Random rnd = new();
        // Has a specific chance out of 10 to drop something
        if (rnd.Next(0, 11) <= dropChance){
            int i = rnd.Next(0, drops.Count());
            // Gets a random drop from the available drops
            final = drops[i];
        }
        return final;
    }

}
