
using UnityEngine;

public class PlayerConstructer
{
    // Indicates if the player is playing
    public bool enabled;
    // Name of player
    public string username;
    // Color of player
    public Color color;

    // Constructor of constructor lol
    public PlayerConstructer(bool enabled, string username, Color color){
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