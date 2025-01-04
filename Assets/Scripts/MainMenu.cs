using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Make sure you include this if you're handling UI buttons

public class MainMenu : MonoBehaviour
{
    // Declare buttons for the UI
    public Button playButton;
    public Button settingsButton;
    public Button instructionsButton;
    public Button adminButton;
    public Button customizationButton;
    public Button statsButton;
    public Button creditsButton;
    public Button exitButton;

    void Start()
    {
        // Add listeners to buttons, calling the appropriate methods
        playButton.onClick.AddListener(playGame);
        settingsButton.onClick.AddListener(openSettings);
        instructionsButton.onClick.AddListener(openInstructions);
        adminButton.onClick.AddListener(openAdmin);
        customizationButton.onClick.AddListener(openCustomization);
        statsButton.onClick.AddListener(openStats);
        creditsButton.onClick.AddListener(openCredits);
        exitButton.onClick.AddListener(exitGame);
    }


   private void playGame()
    {
        // Load your main game scene, assuming it's called "GameScene"
        SceneManager.LoadScene("Level1");
    }

    // Function to open settings
    private void openSettings()
    {
        //scene not availible yet but... we will have to make it.
        SceneManager.LoadScene("SettingsScene");
    }

    // Function to show instructions
    private void openInstructions()
    {
        // Example: SceneManager.LoadScene("InstructionsScene");
    }

    // Function to open admin menu
    private void openAdmin()
    {
        // Logic to open the admin menu
        Debug.Log("Opening Admin Menu...");
        // Example: SceneManager.LoadScene("AdminScene");
    }

    // Function to open customization options
    private void openCustomization()
    {
        SceneManager.LoadScene("CustomizationScene");
    }

    // Function to open stats menu
    private void openStats()
    {
        SceneManager.LoadScene("StatsScene");
    }

    // Function to open credits menu
    private void openCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    // Function to exit the game
    private void exitGame()
    {
        Application.Quit();
    }

}