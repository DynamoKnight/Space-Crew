using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Input system
    protected Vector2 movement;
    private PlayerInput playerInput;
    private InputAction moveAction;

    protected BoxCollider2D boxcollider;
    protected Rigidbody2D rb;
    protected RaycastHit2D hit;
    protected Animator animator;

    // Movement speed
    [SerializeField] protected float moveSpeed;
    protected bool isSprinting = false;

    [SerializeField] protected int healthPoints;
    protected int maxHealth;
    // The explosion particle system
    [SerializeField] protected GameObject explosion;

    // Default color
    protected GameObject body;
    protected string username = "Player";
    protected Color spriteColor = Color.white;
    protected Color dressColor;

    protected GameObject gm;    

    // Sets important properties for the player
    public void ConstructPlayer(PlayerConstructer playerConstructer){
        username = playerConstructer.username;        
        body.GetComponent<SpriteRenderer>().color = playerConstructer.color;
    }

    protected void Awake(){
        // Child 2
        body = transform.GetChild(2).gameObject;
        // Gets the Controls
        playerInput = GetComponent<PlayerInput>();
        // Gets the specific action from the list
        moveAction = playerInput.actions["Move"];
    }

    // Start is called before the first frame update
    protected virtual void Start(){
        // Game Manager controls all
        gm = GameObject.Find("Game Manager");
        // Gets important components
        boxcollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        // Gets the animator from the Body
        spriteColor = body.GetComponent<SpriteRenderer>().color;
        animator = body.GetComponent<Animator>();
    }

    // Gets called every frame
    // All inputs should be initialized here
    protected virtual void Update(){
        // Only recieves input when game is functional and unpaused
        if (LevelManager.isFunctional && !LevelManager.isPaused){
            // Gets and sets the Vector2 value from the move action
            movement = moveAction.ReadValue<Vector2>();
            // Signals the animations based on movement
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            if (movement != Vector2.zero){
                animator.SetFloat("LastVertical", movement.y);
                animator.SetFloat("LastHorizontal", movement.x);
            }
            // Magnitude squared is so that value is positive
            // The speed is used to indicate if there is movement or not
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else{
            // Player cant move
            movement = Vector2.zero;
        }
    
    }

    // Gets called every frame at a interval of time
    protected virtual void FixedUpdate(){
        // Casts a box around character, and returns the collider that the target vector will contact
        // It will only collide with objects in a specific layer, either Actor or Blocking.
        // Y
        hit = Physics2D.BoxCast(rb.position, boxcollider.size, 0, new Vector2(0, movement.y), Math.Abs(movement.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        // If nothing was hit, then movement is possible 
        if (hit.collider == null){
            // Moves player in the y direction
            Move();
        }
        // X
        hit = Physics2D.BoxCast(rb.position, boxcollider.size, 0, new Vector2(movement.x, 0), Math.Abs(movement.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null){
            // Moves player in the x direction
            Move();
        }
    
    }

    // Moves the player based on the given input from Update
    protected virtual void Move(){
        // Transform.position bypasses physics, causing a weird movement glitch. Thats why we must use Rigidbody.MovePosition.
        rb.MovePosition(rb.position + (moveSpeed * Time.deltaTime * movement));
    }

    // Switches the action map based on the player ID
    public void SwitchActionMap(int playerID){
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Player " + playerID);
        moveAction = playerInput.actions["Move"];
    }

}
