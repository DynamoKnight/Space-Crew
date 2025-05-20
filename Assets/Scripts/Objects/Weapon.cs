using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // The parent player
    [SerializeField] protected GameObject player;
    // The image
    protected SpriteRenderer spriteRenderer;
    // Indicates if being used
    public bool beingUsed;

    // Rather than children having to call base.Start(), they just call this
    protected void WeaponStart(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    protected void WeaponUpdate(){

    }


    

}
