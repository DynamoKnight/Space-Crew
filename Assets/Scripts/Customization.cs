using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    // Button to go to the selected level
    [SerializeField] private Button startBtn;
    // Button to go back to level selection
    [SerializeField] private Button backBtn;
    // List of available shirts
    public List<Sprite> Shirts;
    // List of available heads
    public List<Sprite> Heads;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        startBtn.onClick.AddListener(() => SceneManager.LoadScene(Stats.CurrentLevel));
        backBtn.onClick.AddListener(() => SceneManager.LoadScene("LevelSelection"));
        // Initializes all components for each player slot
        for (int i = 0; i < transform.childCount; i++){
            GameObject playerSlot = transform.GetChild(i).GetChild(0).gameObject;
            GameObject emptySlot = transform.GetChild(i).GetChild(1).gameObject;
            // The constructor for the players info
            PlayerConstructor playerConstructor = Stats.Players[i];
            // The Customization objects
            TMP_Text playerTag = playerSlot.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
            Image playerImage = playerSlot.transform.GetChild(2).GetComponent<Image>();
            Image shirtImage = playerSlot.transform.GetChild(2).GetChild(0).GetComponent<Image>();
            Image headImage = playerSlot.transform.GetChild(2).GetChild(1).GetComponent<Image>();
            Image colorImage = playerSlot.transform.GetChild(3).GetChild(0).GetComponent<Image>();
            Button colorToggle = playerSlot.transform.GetChild(3).GetChild(0).GetComponent<Button>();            
            TMP_Text playerName = playerSlot.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            TMP_InputField nameInput = playerSlot.transform.GetChild(4).GetComponent<TMP_InputField>();
            Button nextHeadBtn = playerSlot.transform.GetChild(5).GetComponent<Button>();
            Button prevHeadBtn = playerSlot.transform.GetChild(6).GetComponent<Button>();
            Button nextShirtBtn = playerSlot.transform.GetChild(7).GetComponent<Button>();
            Button prevShirtBtn = playerSlot.transform.GetChild(8).GetComponent<Button>();
            Button addPlayerBtn = emptySlot.transform.GetChild(0).GetComponent<Button>();
            // Makes sure the slot is visible if player is enabled
            if (playerConstructor.enabled == true){
                // Adds the player to their designated slot
                AddPlayer(playerConstructor, playerSlot, emptySlot);
                // Sets the name of the player
                playerName.text = playerConstructor.username;
            }

            // When clicked, removes the player and the player slot
            GameObject clearSlot = playerSlot.transform.GetChild(0).gameObject;
            // 1st player cant be removed
            if (i == 0){
                clearSlot.SetActive(false);
            }
            else{
                // Initializes button to remove player
                Button clearSlotBtn = playerSlot.transform.GetChild(0).GetComponent<Button>();
                clearSlotBtn.onClick.AddListener(() => RemovePlayer(playerConstructor, playerSlot, emptySlot));
            }
            // Sets the Player tag
            playerTag.text = "Player " + (i + 1);
            // Sets the color of the player and the color toggle
            playerImage.color = playerConstructor.color;
            colorImage.color = playerConstructor.color;
            // Color toggler button
            colorToggle.onClick.AddListener(() => ToggleColor(playerConstructor, playerImage, colorImage));
            
            // Initializes buttons to customize head
            nextHeadBtn.onClick.AddListener(() => ToggleHead(playerConstructor, headImage, true));
            prevHeadBtn.onClick.AddListener(() => ToggleHead(playerConstructor, headImage, false));
            // Initializes buttons to customize shirt
            nextShirtBtn.onClick.AddListener(() => ToggleShirt(playerConstructor, shirtImage, true));
            prevShirtBtn.onClick.AddListener(() => ToggleShirt(playerConstructor, shirtImage, false));
            // Sets the username
            playerName.text = playerConstructor.username;
            // When the username input field is finished editing, the player's name will be updated with username
            nameInput.onEndEdit.AddListener((string username) => UpdateName(playerConstructor, playerName, username));
            // Initializes the button to create a new player
            addPlayerBtn.onClick.AddListener(() => AddPlayer(playerConstructor, playerSlot, emptySlot));
            
        }
    }

    // Enables the player slot
    void AddPlayer(PlayerConstructor playerConstructor, GameObject playerSlot, GameObject emptySlot){
        playerConstructor.enabled = true;
        emptySlot.SetActive(false);
        playerSlot.SetActive(true);
    }

    // Disables the player slot
    void RemovePlayer(PlayerConstructor playerConstructor, GameObject playerSlot, GameObject emptySlot){
        playerConstructor.enabled = false;
        emptySlot.SetActive(true);
        playerSlot.SetActive(false);
    }

    // Sets the next color of the player
    void ToggleColor(PlayerConstructor playerConstructor, Image playerImage, Image colorImage){
        Color currentColor = playerConstructor.color;
        int colorIndex = Stats.GetIdxOfColor(currentColor);
        // The next color, makes sure the index is in bounds
        colorIndex = (colorIndex + 1) % Stats.Colors.Count;
        // Saves the color
        playerConstructor.color = Stats.Colors[colorIndex];
        playerImage.color = playerConstructor.color;
        colorImage.color = playerConstructor.color;
    }

    // Sets the next shirt of the player
    void ToggleShirt(PlayerConstructor playerConstructor, Image shirtImage, bool next){
        Sprite currentShirt = playerConstructor.Shirt;
        int shirtIdx = Shirts.IndexOf(currentShirt);
        if (next){
            // Sets the index to the next shirt, makes sure the index is in bounds
            shirtIdx = (shirtIdx + 1) % Shirts.Count;
        }
        else{
            // Sets the index to the next shirt, makes sure the index is in bounds
            shirtIdx = (shirtIdx - 1) % Shirts.Count;
        }
        playerConstructor.Shirt = Shirts[shirtIdx];
        shirtImage.sprite = Shirts[shirtIdx];

    }

    // Sets the next head of the player
    void ToggleHead(PlayerConstructor playerConstructor, Image headImage, bool next){
        Sprite currentHead = playerConstructor.Head;
        int headIdx = Heads.IndexOf(currentHead);
        if (next){
            // Sets the index to the next shirt, makes sure the index is in bounds
            headIdx = (headIdx + 1) % Shirts.Count;
        }
        else{
            // Sets the index to the next shirt, makes sure the index is in bounds
            headIdx = (headIdx - 1) % Shirts.Count;
        }
        playerConstructor.Shirt = Shirts[headIdx];
        headImage.sprite = Shirts[headIdx];

    }

    // Sets the username of the player
    void UpdateName(PlayerConstructor playerConstructor, TMP_Text playerName, string username){
        playerConstructor.username = username;
        playerName.text = playerConstructor.username;
    }

}
