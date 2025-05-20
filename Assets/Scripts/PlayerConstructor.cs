
using UnityEngine;

public class PlayerConstructor
{
    // Indicates if the player is playing
    public bool enabled;
    // Name of player
    public string username;
    // Color of player
    public Color color;
    // Shirt of player
    public Sprite Shirt;
    // Face of player
    public Sprite Head;

    public Sprite[] idleUpHeadSprites;
    public Sprite[] idleDownHeadSprites;
    public Sprite[] idleLeftHeadSprites;
    public Sprite[] idleRightHeadSprites;
    public Sprite[] walkUpHeadSprites;
    public Sprite[] walkDownHeadSprites;
    public Sprite[] walkRightHeadSprites;
    public Sprite[] walkLeftHeadSprites;

    public Sprite[] idleUpShirtSprites;
    public Sprite[] idleDownShirtSprites;
    public Sprite[] idleLeftShirtSprites;
    public Sprite[] idleRightShirtSprites;
    public Sprite[] walkUpShirtSprites;
    public Sprite[] walkDownShirtSprites;
    public Sprite[] walkRightShirtSprites;
    public Sprite[] walkLeftShirtSprites;
    

    // Constructor of constructor lol
    public PlayerConstructor(bool enabled, string username, Color color){
        this.enabled = enabled;
        this.username = username;
        this.color = color;
    }

    public void SetEnabled(bool enabled){
        this.enabled = enabled;
    }
    public void SetUsername(string username){
        this.username = username;
    }
    public void SetColor(Color color){
        this.color = color;
    }
}