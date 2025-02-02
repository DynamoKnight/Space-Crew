using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopPlayer : Player
{

    protected override void Start(){
        // The HP is set based on the number available in the UI
        maxHealth = 5;
        healthPoints = maxHealth;
        UpdateHealth();
        base.Start();
    }

    [ContextMenu("Kill Player")]
    // Instant kills the Player just because...
    public void TakeDamage(){
        healthPoints = 0;
        UpdateHealth();
        Die();
    }
    // Instant kills the Player, knows what did damage to it
    public virtual void TakeDamage(GameObject sender){
        TakeDamage();
    }
    // Remove health points from the Player
    public virtual void TakeDamage(GameObject sender, int damage){
        ApplyAffect("damage");
        // Hurts player
        healthPoints -= damage;
        UpdateHealth();
        // Player dies
        if (healthPoints <= 0){
            TakeDamage();
        }
        // Player is just hurt
        else{
        }
        
    }

    // What happens upon death
    public virtual void Die(){
        // Creates the particle effect, slightly higher
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.rotation);
        // Destroys player
        Destroy(gameObject);
    }

    // Shows the number of hearts
    public virtual void UpdateHealth(){
        /*
        for (int i = 0; i < hearts.Length; i++){
            // Displays regular or black heart based on health
            if (i < healthPoints){
                hearts[i].color = Color.white;
            }
            else{
                hearts[i].color = Color.black;
            }
        }*/
    }

    public virtual void ApplyAffect(string affect){
        if (affect == "freeze"){
            StopAllCoroutines();
            StartCoroutine(Freeze());
        }
        else if (affect == "damage"){
            StartCoroutine(Damage());
        }
    }

    public virtual IEnumerator Freeze(){
        // Blue-ish
        float green = 150f/255f;
        float red = 0f;
        body.GetComponent<SpriteRenderer>().color = new Color(red, green, 1f);
        // Slows down character and halves animation speed
        moveSpeed -= 5;
        animator.speed = .5f;
        // Changes color overtime
        while (green < 1f || red < 1f){
            // Increase color values
            green += 0.05f;
            red += 0.05f;

            // Set the color
            body.GetComponent<SpriteRenderer>().color = new Color(red, green, 1f);
            yield return new WaitForSeconds(0.1f);
        }
        // Ends the frost effect
        moveSpeed += 5;
        animator.speed = 1f;
        body.GetComponent<SpriteRenderer>().color = spriteColor;
    }

    public virtual IEnumerator Damage(){
        // Flashes damage twice
        body.GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f);
        yield return new WaitForSeconds(0.2f);
        body.GetComponent<SpriteRenderer>().color = spriteColor;
        yield return new WaitForSeconds(0.1f);
        body.GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f);
        yield return new WaitForSeconds(0.5f);
        body.GetComponent<SpriteRenderer>().color = spriteColor;
    }

    // Returns the number of health points
    public virtual int GetHearts(){
        return healthPoints;
    }
    // Returns the max health
    public virtual int GetMaxHealth(){
        return maxHealth;
    }
    // Adds hearts
    public virtual void AddHeart(int heart = 1){
        healthPoints += heart;
        UpdateHealth();
    }
}
