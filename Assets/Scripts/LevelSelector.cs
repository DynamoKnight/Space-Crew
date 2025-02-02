using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlanetTravel : MonoBehaviour
{
    private GameObject gm;
    [SerializeField] private GameObject modeSelector;
    private Button modeBackBtn;
    private GameObject spaceMode; 
    private GameObject survivalMode;
    

    // Start is called before the first frame update
    void Start(){
        
        gm = GameObject.Find("Game Manager");

        // Sets up the mode selector
        modeBackBtn = modeSelector.transform.GetChild(0).GetComponent<Button>();
        spaceMode = modeSelector.transform.GetChild(1).gameObject;
        survivalMode = modeSelector.transform.GetChild(2).gameObject;
        // Hides the mode selector if back button is clicked
        modeBackBtn.onClick.AddListener(() => modeSelector.SetActive(false));
        
        // Configures what planets are locked and unlocked
        int cur = Stats.GetIdxOfCurrentPlanet();
        // Loops through each planet button
        for (int i = 0; i < transform.childCount; i++){
            Button planetBtn = transform.GetChild(i).GetComponent<Button>();
            // If the planet hasn't been reached yet, the button can't be accessed
            if (i > Stats.GetIdxOfBestPlanet()){
                // Disables button
                planetBtn.onClick.RemoveAllListeners();
                // Shows lock
                planetBtn.transform.GetChild(0).gameObject.SetActive(true);
            }
            // The planet can be accessed
            else{
                // When the planet button is clicked, it will open the mode selector
                string planet = Stats.GetPlanets()[i];
                // Lambda function allows parameter to be passed
                planetBtn.onClick.AddListener(() => OpenModeSelector(planet));
                // Hides lock
                planetBtn.transform.GetChild(0).gameObject.SetActive(false);
            }
            // Hides the spaceship if not on current planet
            if (i != cur){
                planetBtn.transform.GetChild(1).gameObject.SetActive(false);
            }
            // Shows a spaceship to indicate the current planet
            else if (i == cur){
                planetBtn.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    // Shows the mode selector with the proper levels
    void OpenModeSelector(string planet){
        modeSelector.SetActive(true);
        // Sets the planet name and link to level based on the level
        Button spaceBtn = spaceMode.GetComponent<Button>();
        Button survivalBtn = survivalMode.GetComponent<Button>();
        string spaceLevel = planet + "-Space";
        string survivalLevel = planet + "-Survival";
        // Enables the space mode if its been unlocked
        if (Stats.GetIdxOfLevel(spaceLevel) <= Stats.GetIdxOfBestLevel()){
            spaceBtn.onClick.AddListener(() => QueueLevel(planet + "-Space"));
            // Hides lock
            spaceBtn.transform.GetChild(0).gameObject.SetActive(false);
        }
        else{
            // Disables button
            spaceBtn.onClick.RemoveAllListeners();
            // Shows lock
            spaceBtn.transform.GetChild(0).gameObject.SetActive(true);
        }
        // Enables the survival mode if its been unlocked
        if (Stats.GetIdxOfLevel(survivalLevel) <= Stats.GetIdxOfBestLevel()){
            survivalBtn.onClick.AddListener(() => QueueLevel(planet + "-Survival"));
            // Hides lock
            survivalBtn.transform.GetChild(0).gameObject.SetActive(false);
        }
        else{
            // Disables button
            survivalBtn.onClick.RemoveAllListeners();
            // Shows lock
            survivalBtn.transform.GetChild(0).gameObject.SetActive(true);
        }
        
    }

    // Queues up the next level and goes to the Customization Screen
    void QueueLevel(string level){
        // Sets the current level and planet
        Stats.CurrentLevel = level;
        Stats.CurrentPlanet = Stats.GetPlanetFromLevel(level);
        // Goes to Customization Screen
        SceneManager.LoadScene("CustomizationScreen");
       
    }
}
