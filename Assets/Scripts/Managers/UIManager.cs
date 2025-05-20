using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Pause Menu
    [SerializeField] private GameObject pauseMenu;
    private Button quitBtn;
    private Button homeBtn;
    private Button backBtn;

    [SerializeField] private GameObject startScreen;
    private Image startScreenBG;
    private Image startScreenBorder;
    private GameObject startBtn;
    // Make sure hierarchy is in order!
    private Image image;
    private TMP_Text planet;
    private TMP_Text difficulty;
    private TMP_Text quote;
    private TMP_Text info;
    private TMP_Text enemy_info;
    // The current time in seconds
    public float timer = 0f;

    void Start(){
        // If the current scene is a level, it will have escape buttons
        if(Stats.Levels.Contains(SceneManager.GetActiveScene().name)){
            // Pause menu buttons
            quitBtn = pauseMenu.transform.GetChild(0).GetComponent<Button>();
            homeBtn = pauseMenu.transform.GetChild(1).GetComponent<Button>();
            backBtn = pauseMenu.transform.GetChild(2).GetComponent<Button>();
            // Adds functions to it
            quitBtn.onClick.AddListener(LevelManager.QuitGame);
            homeBtn.onClick.AddListener(LevelManager.ReturnHome);
            backBtn.onClick.AddListener(TogglePauseMenu);
        }
        // Checks if start screen exists
        if (startScreen){
            // Make sure hierarchy is in order!
            startScreenBG = startScreen.GetComponent<Image>();
            startScreenBorder = startScreen.transform.GetChild(0).GetComponent<Image>();
            startBtn = startScreen.transform.GetChild(1).gameObject;
            image = startScreen.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            planet = startScreen.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            difficulty = startScreen.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>();
            quote = startScreen.transform.GetChild(0).GetChild(3).GetComponent<TMP_Text>();
            info = startScreen.transform.GetChild(0).GetChild(4).GetComponent<TMP_Text>();
            enemy_info = startScreen.transform.GetChild(0).GetChild(5).GetComponent<TMP_Text>();
            // Shows start screen
            StartScreen();
        }
    }

    void Update(){
        // Pauses game when escaped
        if (Input.GetKeyDown(KeyCode.Escape)){
            TogglePauseMenu();
        }
        // Skips start screen animation
        if (startScreen && Input.GetKeyDown(KeyCode.Space)){

        }
    }

    // Shows the start screen and initializes values
    public void StartScreen(){
        startScreen.SetActive(true);
        Dictionary<string, string[]> infos = Stats.PlanetInfos;
        // Gets the index of the current planet
        string currentPlanet = Stats.CurrentPlanet;
        int currentPlanetIdx = Stats.GetIdxOfCurrentPlanet();
        // Sets values based on the planet
        image.sprite = Stats.PlanetSprites[currentPlanetIdx];
        planet.text = currentPlanet;
        // Sets the difficulty
        difficulty.text = infos[currentPlanet][0];
        if (difficulty.text == "Easy"){
            difficulty.color = Color.green;
        }
        if (difficulty.text == "Medium"){
            difficulty.color = Color.yellow;
        }
        else if (difficulty.text == "Hard"){
            // Orange
            difficulty.color = new Color(1, 0.4f, 0);
        }
        else if (difficulty.text == "Extreme"){
            difficulty.color = Color.red;
        }
        else if (difficulty.text == "Boss"){
            // Purple
            difficulty.color = new Color(0.76f, 1, 0.4f);
        }
        // Sets the other information
        quote.text = infos[currentPlanet][1];
        info.text = infos[currentPlanet][2];
        enemy_info.text = "Enemies: " + infos[currentPlanet][3];
        
        // The button to escape start screen
        startBtn.GetComponent<Button>().onClick.AddListener(StartLevel);
        // Animation to show start screen
        StartCoroutine(FadeStartScreen());
    }

    // Fades in the start screen
    public IEnumerator FadeStartScreen(){
        Color ssColor = startScreenBG.color;
        Color ssbColor = startScreenBorder.color;
        Color imgColor = image.color;
        Color pColor = planet.color;
        Color dColor = difficulty.color;
        Color qColor = quote.color;
        Color iColor = info.color;
        Color eiCOlor = enemy_info.color;
        // Initializes the list with pairs of objects and colors
        List<KeyValuePair<Graphic, Color>> colorList = new(){
            new(startScreenBG, ssColor),
            new(startScreenBorder, ssbColor),
            new(image, imgColor),
            new(planet, pColor),
            new(difficulty, dColor),
            new(quote, qColor),
            new(info, iColor),
            new(enemy_info, eiCOlor)
        };
        float speed = 0.05f;
        float fade = 0.04f;
        // Starts hidden
        for (int i = 0; i < colorList.Count; i++){
            Color color = colorList[i].Value;
            color.a = 0f;
            colorList[i].Key.color = color;
        }
        startBtn.SetActive(false);
        yield return new WaitForSeconds(1f);
        // Reveals over time
        // Increases the alpha value of the object's Image.
        for (float a = 0f; a < 1f; a += fade){
            for (int i = 0; i < colorList.Count; i++){
                Color color = colorList[i].Value;
                color.a = a;
                colorList[i].Key.color = color;
            }
            yield return new WaitForSeconds(speed);
        }
        // Ensures it is finally revealed
        for (int i = 0; i < colorList.Count; i++){
            Color color = colorList[i].Value;
            color.a = 1f;
            colorList[i].Key.color = color;
        }
        // Start screen is a bit transparent
        ssColor.a = 0.94f;
        startScreenBG.color = ssColor;
        yield return new WaitForSeconds(2f);
        // Shows start button to start the game
        startBtn.SetActive(true);

    }
    
    // Disables the startScreen and enables player
    public void StartLevel(){
        startScreen.SetActive(false);
        LevelManager.isFunctional = true;
        LevelManager.isPaused = false;
    }

    // Shows/hides the pause menu
    public void TogglePauseMenu(){
        LevelManager.TogglePause();
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
    }
}
