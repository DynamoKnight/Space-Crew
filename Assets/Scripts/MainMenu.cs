using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  
public class MainMenu : MonoBehaviour
{
    // Declare buttons for the UI
    private Button playButton;
    private Button settingsButton;
    private Button instructionsButton;
    private Button adminButton;
    private Button customizationButton;
    private Button statsButton;
    private Button creditsButton;
    private Button exitButton;

    [SerializeField] private GameObject settingsMenu;
    private Button backButton;

    void Start(){
        // Gets the button based on position
        playButton = transform.GetChild(1).GetComponent<Button>();
        settingsButton = transform.GetChild(2).GetComponent<Button>();
        statsButton = transform.GetChild(3).GetComponent<Button>();
        exitButton = transform.GetChild(4).GetComponent<Button>();
        // Settings menu
        backButton = settingsMenu.transform.GetChild(1).GetComponent<Button>();

        // Add listeners to buttons, calling the appropriate methods
        playButton.onClick.AddListener(PlayGame);   
        settingsButton.onClick.AddListener(ToggleSettings);
        statsButton.onClick.AddListener(OpenStats);
        exitButton.onClick.AddListener(ExitGame);
        /*instructionsButton.onClick.AddListener(openInstructions);
        adminButton.onClick.AddListener(openAdmin);
        customizationButton.onClick.AddListener(openCustomization);
        creditsButton.onClick.AddListener(openCredits);*/
        backButton.onClick.AddListener(ToggleSettings);
        
    }

    // Goes to level selection screen
    public void PlayGame(){
        SceneManager.LoadScene("LevelSelection");
    }

    // Function to toggle settings menu
    public void ToggleSettings(){
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    // Function to show instructions
    public void OpenInstructions(){
        SceneManager.LoadScene("InstructionsScene");
    }

    // Function to open admin menu
    public void OpenAdmin()
    {
        SceneManager.LoadScene("AdminScene");
    }

    // Function to open customization options
    public void OpenCustomization()
    {
        SceneManager.LoadScene("CustomizationScene");
    }

    // Function to open stats menu
    public void OpenStats(){
        // Will have to make code
    }

    // Function to open credits menu
    public void OpenCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    // Function to exit the game
    public void ExitGame(){
        Application.Quit();
    }

}