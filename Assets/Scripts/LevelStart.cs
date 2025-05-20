using UnityEngine;

public class LevelStart : MonoBehaviour
{
    // LevelStart is necessary so that we can get any variables or 
    // assets from the unity editor so that it is stored in code
    // through the Stats file.

    // Images of each planet
    public Sprite[] planetSprites;

    public Sprite[] shirts;
    public Sprite[] heads;

    void Awake(){
        // Stores the planet images so they can be accessed everywhere
        Stats.PlanetSprites = planetSprites;
    }
}
