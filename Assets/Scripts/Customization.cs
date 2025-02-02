using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button backButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        // Goes to the selected level
        startButton.onClick.AddListener(() => SceneManager.LoadScene(Stats.CurrentLevel));
        // Goes back to level selection
        backButton.onClick.AddListener(() => SceneManager.LoadScene("LevelSelection"));

        for (int i = 0; i < transform.childCount; i++){
            GameObject playerSlot = transform.GetChild(i).GetChild(0).gameObject;
            GameObject emptySlot = transform.GetChild(i).GetChild(1).gameObject;
            // The constructor for the players info
            PlayerConstructer playerConstructer = Stats.Players[i];
            // The Customization objects
            TMP_Text playerTag = playerSlot.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
            Image playerImage = playerSlot.transform.GetChild(2).GetComponent<Image>();
            Image colorImage = playerSlot.transform.GetChild(3).GetChild(0).GetComponent<Image>();
            Button colorToggle = playerSlot.transform.GetChild(3).GetChild(0).GetComponent<Button>();            
            TMP_Text playerName = playerSlot.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            TMP_InputField nameInput = playerSlot.transform.GetChild(4).GetComponent<TMP_InputField>();           
            Button addPlayerBtn = emptySlot.transform.GetChild(0).GetComponent<Button>();
            // Makes sure it is visible if player is enabled
            if (playerConstructer.enabled == true){
                AddPlayer(playerConstructer, playerSlot, emptySlot);
                playerName.text = playerConstructer.username;
            }

            // When clicked, Removes the player
            GameObject clearSlot = playerSlot.transform.GetChild(0).gameObject;
            // 1st player cant be removed
            if (i == 0){
                clearSlot.SetActive(false);
            }
            else{
                Button clearSlotBtn = playerSlot.transform.GetChild(0).GetComponent<Button>();
                clearSlotBtn.onClick.AddListener(() => RemovePlayer(playerConstructer, playerSlot, emptySlot));
            }
            // Sets the Player tag
            playerTag.text = "Player " + (i + 1);
            // Sets the color of the player and the color toggle
            playerImage.color = playerConstructer.color;
            colorImage.color = playerConstructer.color;
            // Color toggler button
            colorToggle.onClick.AddListener(() => ToggleColor(playerConstructer, playerImage, colorImage));
            // Sets the username
            playerName.text = playerConstructer.username;
            // Username input field
            // onEndEdit must have the string parameter that represents what was entered
            nameInput.onEndEdit.AddListener((string username) => UpdateName(playerConstructer, playerName, username));
            // When clicked, Adds the player
            addPlayerBtn.onClick.AddListener(() => AddPlayer(playerConstructer, playerSlot, emptySlot));
            
        }
    }

    // Update is called once per frame
    void Update(){
    }

    void AddPlayer(PlayerConstructer playerConstructer, GameObject playerSlot, GameObject emptySlot){
        playerConstructer.enabled = true;
        emptySlot.SetActive(false);
        playerSlot.SetActive(true);
    }

    void RemovePlayer(PlayerConstructer playerConstructer, GameObject playerSlot, GameObject emptySlot){
        playerConstructer.enabled = false;
        emptySlot.SetActive(true);
        playerSlot.SetActive(false);
    }

    // Sets the next color of the player
    void ToggleColor(PlayerConstructer playerConstructer, Image playerImage, Image colorImage){
        Color currentColor = playerConstructer.color;
        int colorIndex = Stats.GetIdxOfColor(currentColor);
        // The next color
        colorIndex = (colorIndex + 1) % Stats.Colors.Count;
        // Saves the color
        playerConstructer.color = Stats.Colors[colorIndex];
        playerImage.color = playerConstructer.color;
        colorImage.color = playerConstructer.color;
    }

    // Sets the username of the player
    void UpdateName(PlayerConstructer playerConstructer, TMP_Text playerName, string username){
        playerConstructer.username = username;
        playerName.text = playerConstructer.username;
    }

}
