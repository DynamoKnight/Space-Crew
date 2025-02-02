using System.Collections.Generic;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

// Keeps track of all saved variables
public static class Stats
{
    // The totals
    public static int EnemiesDefeated {get; set;} = 0;

    // Global data
    // The current planet that is being played on
    public static string CurrentPlanet {get; set;} = "Pluto";
    // The current level that is being played on
    public static string CurrentLevel {get; set;} = "Pluto-Space";
    // The best planet will indicate which levels should be unlocked
    public static string BestPlanet {get; set;} = "Pluto";
    // The best level will indicate which levels should be unlocked
    public static string BestLevel {get; set;} = "Pluto-Space";
    // The number of planets discovered
    public static int PlanetsDiscovered {get; set;} = 0;

    public static Sprite[] PlanetSprites {get; set;}

    // Resets totals and game data back to start
    public static void ResetStats(){
        EnemiesDefeated = 0;
        PlanetsDiscovered = 0;
        ResetGame();
    }

    // Resets just the game data
    public static void ResetGame(){
        BestPlanet = "Pluto";
        BestLevel = "Pluto-Space";
    }

    // Returns the list of lanets
    public static List<string> GetPlanets(){
        return PlanetInfos.Keys.ToList();
    }

    // Returns the planet from the name of the level
    public static string GetPlanetFromLevel(string level){
        // Splits the string into a list and then gets
        // the name of the planet from the list
        return level.Split('-')[0];
    }

    // Returns the Index of the Planet
    public static int GetIdxOfPlanet(string planet){
        return GetPlanets().IndexOf(planet);
    }
    // Returns the Index of the Level
    public static int GetIdxOfLevel(string level){
        return Levels.IndexOf(level);
    }

    // Returns the Index of the Best Planet
    public static int GetIdxOfBestPlanet(){
        return GetIdxOfPlanet(BestPlanet);
    }
    // Returns the Index of the Best Level
    public static int GetIdxOfBestLevel(){
        return GetIdxOfLevel(BestLevel);
    }

    // Returns the Index of the Current Planet
    public static int GetIdxOfCurrentPlanet(){
        return GetIdxOfPlanet(CurrentPlanet);
    }
    // Returns the Index of the Current Level
    public static int GetIdxOfCurrentLevel(){
        return GetIdxOfLevel(CurrentLevel);
    }

    // List of all levels
    public static List<string> Levels = new(){
        "Pluto-Space","Pluto-Survival",
        "Neptune-Space","Neptune-Survival",
        "Uranus-Space","Uranus-Survival",
        "Saturn-Space","Saturn-Survival",
        "Jupiter-Space","Jupiter-Survival",
        "Mars-Space","Mars-Survival",
        "Moon-Space","Moon-Survival",
        "Earth-Space","Earth-Survival",
        "Venus-Space","Venus-Survival",
        "Mercury-Space","Mercury-Survival"
    };

    // List of players
    public static List<PlayerConstructer> Players = new(){
        {new PlayerConstructer(true, "Player 1", Color.white)},
        {new PlayerConstructer(false, "Player 2", Color.red)},
        {new PlayerConstructer(false, "Player 3", Color.blue)},
        {new PlayerConstructer(false, "Player 4", Color.green)}
    };

    // List of available colors
    public static List<Color> Colors = new(){
        {Color.white},
        {Color.red},
        {Color.blue},
        {Color.green},
        {Color.yellow},
        {Color.gray},
        {Color.magenta},
        {Color.black},
        {Color.cyan},
        {new Color(1, 0.4f, 0)}, // Orange
        {new Color(0.4f, 1, 0)}, // Purple
        {new Color(0.42f, 0.2f, 0)} // Brown
    };

    // Returns the index of the color in the list
    public static int GetIdxOfColor(Color color){
        return Colors.IndexOf(color);
    }

    // Every planet with information about it
    // Array with key-value pairs
    // Planet, [Difficulty, Nickname, Info, Enemies, Temperature]
    public static Dictionary<string, string[]> PlanetInfos = new()
    {
        { "Pluto", new string[] {
            "Easy",
            "The Dwarf Planet",
            "The icy terrain and rocky landscape make it hard to navigate. While it's cold environment provides a challenge, the aliens settlers have made the planet a bit more bearable... ",
            "Touching the evil robots, their attacks, or other hazardous materials deal damage. They have adapted to the cold climate and are not kind to intruders...",
            "-375"
            } },
        { "Neptune", new string[] {
            "Easy",
            "The Blue Giant",
            "Neptune has 14 known moons, the largest of which is Triton, which is believed to be a captured Kuiper Belt object. Using your jetpack, you are able to float above the gas giant...",
            "Due to the harsh climate, they unleash frost bite attacks which will freeze anyone it touches...",
            "-330"
            } },
        { "Uranus", new string[] {
            "Medium",
            "The Bull's Eye Planet",
            "Uranus is the only planet in the solar system that rotates on its side. It has an extremely powerful atmospheric pressure that makes movement laborious...",
            "They have harnessed the gases abundant in the atmosphere to release ionized projectiles....",
            "-320"
            } },
        { "Saturn", new string[] {
            "Medium",
            "The Ringed Planet",
            "Saturn's density is so low that if there were a large enough body of water, it would float in it. Swirling clouds and powerful winds make it a challenging planet to traverse...",
            "The planets strong magnetic fields have been exploited by the enemies...",
            "-220"
            } },
        { "Jupiter", new string[] {
            "Hard",
            "The Gas Giant",
            "Jupiter's Great Red Spot is a massive storm that has been raging for at least 400 years and is large enough to engulf Earth two or three times over.",
            "Big boys",
            "-166"
            } },
        { "Mars", new string[] {
            "Hard",
            "The Red Planet",
            "Mars has the largest volcano and canyon in the solar system, Olympus Mons and Valles Marineris respectively",
            "Sand attacks",
            "-85"
            } },
        { "The Moon", new string[] {
            "Extreme",
            "Luna Selene",
            "One small step for man. One giant leap for mankind.",
            "Everybody",
            "0"
            } },
        { "Earth", new string[] {
            "Boss",
            "The Blue Planet",
            "The home of the advanced species classified as Humans.",
            "Boss is here",
            "59"
            } },
        { "Venus", new string[] {
            "Extreme",
            "The Morning Star",
            "Hot.",
            "Robots",
            "867"
            } },
        { "Mercury", new string[] {
            "Extreme",
            "The Swift Planet",
            "Close to Sun.",
            "Firey",
            "333"
            } }
            
    };

}