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

    protected GameObject shirt;
    protected GameObject hat;
    protected SpriteRenderer shirtRenderer;
    protected SpriteRenderer hatRenderer;

    protected GameObject gm;    

    // Sets important properties for the player
    public void ConstructPlayer(PlayerConstructor playerConstructor){
        username = playerConstructor.username;        
        body.GetComponent<SpriteRenderer>().color = playerConstructor.color;
        /*
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = state.normalizedTime % 1f;

        string stateName = GetCurrentStateName(state);

        Sprite[] currentShirtFrames = GetShirtSpritesForState(stateName, playerConstructor);
        Sprite[] currentHatFrames = GetHatSpritesForState(stateName, playerConstructor);

        if (currentShirtFrames != null && currentShirtFrames.Length > 0){
            int frame = Mathf.FloorToInt(normalizedTime * currentShirtFrames.Length);
            frame = Mathf.Clamp(frame, 0, currentShirtFrames.Length - 1);
            shirtRenderer.sprite = currentShirtFrames[frame];
        }

        if (currentHatFrames != null && currentHatFrames.Length > 0){
            int frame = Mathf.FloorToInt(normalizedTime * currentHatFrames.Length);
            frame = Mathf.Clamp(frame, 0, currentHatFrames.Length - 1);
            hatRenderer.sprite = currentHatFrames[frame];
        }*/

    }

    private string GetCurrentStateName(AnimatorStateInfo stateInfo){
        if (stateInfo.IsName("WalkDown")) return "WalkDown";
        if (stateInfo.IsName("WalkUp")) return "WalkUp";
        if (stateInfo.IsName("WalkLeft")) return "WalkLeft";
        if (stateInfo.IsName("WalkRight")) return "WalkRight";
        if (stateInfo.IsName("IdleDown")) return "IdleDown";
        if (stateInfo.IsName("IdleUp")) return "IdleUp";
        if (stateInfo.IsName("IdleLeft")) return "IdleLeft";
        if (stateInfo.IsName("IdleRight")) return "IdleRight";
        return "Unknown";
    }

    private Sprite[] GetShirtSpritesForState(string state, PlayerConstructor constructor){
        return state switch{
            "WalkDown" => constructor.walkDownShirtSprites,
            "WalkUp" => constructor.walkUpShirtSprites,
            "WalkLeft" => constructor.walkLeftShirtSprites,
            "WalkRight" => constructor.walkRightShirtSprites,
            "IdleDown" => constructor.idleDownShirtSprites,
            "IdleUp" => constructor.idleUpShirtSprites,
            "IdleLeft" => constructor.idleLeftShirtSprites,
            "IdleRight" => constructor.idleRightShirtSprites,
            _ => null,
        };
    }

    private Sprite[] GetHatSpritesForState(string state, PlayerConstructor constructor){
        return state switch{
            "WalkDown" => constructor.walkDownHeadSprites,
            "WalkUp" => constructor.walkUpHeadSprites,
            "WalkLeft" => constructor.walkLeftHeadSprites,
            "WalkRight" => constructor.walkRightHeadSprites,
            "IdleDown" => constructor.idleDownHeadSprites,
            "IdleUp" => constructor.idleUpHeadSprites,
            "IdleLeft" => constructor.idleLeftHeadSprites,
            "IdleRight" => constructor.idleRightHeadSprites,
            _ => null,
        };
    }



    protected void Awake(){
        shirt = transform.GetChild(0).gameObject;
        hat = transform.GetChild(1).gameObject;
        // Child 2
        body = transform.GetChild(2).gameObject;
        // Gets the controls of the player
        playerInput = GetComponent<PlayerInput>();
        // Gets the specific move action from the list of actions
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

        shirtRenderer = shirt.GetComponent<SpriteRenderer>();
        hatRenderer = hat.GetComponent<SpriteRenderer>();
    }

    // Gets called every frame
    // All inputs should be initialized here
    protected virtual void Update(){
        // Only receives input when game is functional and unpaused
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
