using UnityEngine;

public class LevelStart : MonoBehaviour
{
    // LevelStart will record any necessary variables for the start.
    // Images of each planet
    public Sprite[] planetSprites;

    void Awake(){
        // Stores the planet images so they can be accessed everywhere
        Stats.PlanetSprites = planetSprites;
    }
}
