using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : Weapon
{

    protected Rigidbody2D rb;
    // How fast the gun rotates
    private float rotationSpeed = 200f;
    // The bullet
    [SerializeField] protected GameObject bulletPrefab;
    // The place where the bullet comes from
    protected Transform firingPoint;
    [SerializeField] protected float fireRate = 0.5f;
    // Keeps track of time
    protected float fireTimer;

    // Input actions for rotation and shooting
    private InputAction interactLeftAction;
    private InputAction interactRightAction;
    private InputAction attackAction;


    protected virtual void Start(){
        WeaponStart();
        firingPoint = transform.GetChild(0);
        // Unique name
        gameObject.name = "Laser Blaster";
        rb = gameObject.GetComponent<Rigidbody2D>();

        // Get input actions from PlayerInput
        interactLeftAction = player.GetComponent<PlayerInput>().actions["Interact Left"];
        interactRightAction = player.GetComponent<PlayerInput>().actions["Interact Right"];
        attackAction = player.GetComponent<PlayerInput>().actions["Attack"];
    }

    // Update is called once per frame
    protected virtual void Update(){
        WeaponUpdate();
        // Only collects input if game is unpaused and functional
        if (!LevelManager.isPaused && LevelManager.isFunctional){
            RotateGun();
            ShootGun();
            
        }
    
    }
    // Spawns a bullet
    protected virtual void Shoot(){
        // Keeps track of the bullet object shot
        Bullet bulletFired = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation).GetComponent<Bullet>();
        // Tells the bullet that this is the sender
        bulletFired.SetSender(player, gameObject);
    }

    // Rotates the gun
    private void RotateGun(){
        // Check if the left or right rotation action is performed
        if (interactLeftAction.ReadValue<float>() > 0){
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else if (interactRightAction.ReadValue<float>() > 0){
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
        if (rb.rotation > 90 || rb.rotation < -90){
            spriteRenderer.flipY = true;
        }
        else{
            spriteRenderer.flipY = false;
        }
    }

    // Checks if shoot key is pressed
    private void ShootGun(){
        // beingUsed is determine by WeaponSwitcher
        if(beingUsed){
            // Left Mouse Button
            // Check if the shoot action is triggered
            if (attackAction.triggered && fireTimer >= fireRate){
                Shoot();
                // Resets timer
                fireTimer = 0f;
            }
            else{
                // Increases time until it reaches fireRate
                fireTimer += Time.deltaTime;
            }
        }
    }
}
