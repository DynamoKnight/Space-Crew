using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Manages the current level
public class LevelManager : MonoBehaviour
{
    // Indicates the next level after this one
    public string nextLevel;

    // Indicates whether the game is paused
    public static bool isPaused;
    // Indicates whether the User can interact with the player
    public static bool isFunctional;
    // Indicates whether the current level is completed 
    public static bool levelComplete = false;
    // Indicates if enemies can spawn
    public static bool spawnEnemies;

    public GameObject playerPrefab;
    // The parent of spawn points for the players
    public GameObject spawnPoints;

    private GameObject gm;

    // Gets called when loading 
    private void Awake(){
        // Makes sure correct planet
        var currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene: " + currentScene + "\nCurrent Level: " + Stats.CurrentLevel);

        // This should already be set in the LevelSelector,
        // but this will ensure that the current level and planet is set
        if (Stats.Levels.Contains(currentScene)){
            // Sets the current level and planet
            Stats.CurrentLevel = currentScene;
            Stats.CurrentPlanet = Stats.GetPlanetFromLevel(currentScene);
            StartLevel();
        }
        
        // Initial conditions when the level starts
        isPaused = false;
        // Will be enabled after start screen
        isFunctional = false;
        spawnEnemies = true;
        
        gm = gameObject;
    }


    // Stops game and records data
    public static void GameOver(){
        isFunctional = false;
        SceneManager.LoadScene("LevelSelection");
    }

    // Pauses and Unpauses the game
    public static void TogglePause(){
        // Unpause
        if (Time.timeScale == 0){
            isPaused = false;
            Time.timeScale = 1;
        }
        // Pause
        else {
            isPaused = true;
            Time.timeScale = 0;
        }
    }

    // Closes the game
    public static void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();
    }

    // Goes back to the Menu
    public static void ReturnHome(){
        SceneManager.LoadScene("MenuScreen");
    }

    // Spawns the players
    public void StartLevel(){
        if (spawnPoints){        
            for (int i = 0; i < Stats.Players.Count; i++){
                // Creates players based on their playerConstructor
                PlayerConstructor playerConstructor = Stats.Players[i];
                Transform spawnPoint = spawnPoints.transform.GetChild(i);
                if (playerConstructor.enabled){
                    GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
                    // Constructs player
                    player.GetComponent<Player>().ConstructPlayer(playerConstructor);
                    // Sets the action map to the proper player
                    player.GetComponent<Player>().SwitchActionMap(i+1);
                    
                }
            }
        }
    }
    
    // Sets the new best level
    public string UpdateLevel(){
        // If there is no next level, there is no next level duh
        if (nextLevel != null){
            // Sets the new best level if the next level is bester
            if (Stats.GetIdxOfLevel(nextLevel) > Stats.GetIdxOfBestLevel()){
                // Saves new data
                Stats.BestLevel = nextLevel;

                string nextPlanet = Stats.GetPlanetFromLevel(nextLevel);
                // Sets the new best planet is the next planet is bester
                if (Stats.GetIdxOfPlanet(nextPlanet) > Stats.GetIdxOfBestPlanet()){
                    Stats.BestPlanet = nextPlanet;
                    Stats.PlanetsDiscovered += 1;
                    return "New Planet Unlocked!";
                }
                return "New Level Unlocked!";
            }
            return "Level not Updated.";
        }
        return "No new level.";
    }

    // Allows level to be finished in Inspector
    [ContextMenu("Auto-Complete Level")]
    public void FinishLevel(){
        levelComplete = true;
        UpdateLevel();
        GameOver();

    }


}
