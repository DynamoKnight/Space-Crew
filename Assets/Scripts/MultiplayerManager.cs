using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    [SerializeField] private GameObject waveText;
    [SerializeField] private GameObject timerText;
    private int currentWave;
    private float timer;

    // Start is called before the first frame update
    void Start(){
        currentWave = 0;
        timer = 0f;
    }

    void Update(){
        // Updates the time
        if (LevelManager.isFunctional && !LevelManager.isPaused){
            timer += Time.deltaTime;
            float i = timer;
            int minute = Convert.ToInt32(Math.Floor(i/60f));
            int seconds = Convert.ToInt32(i - minute*60);
            // Adds 0 if needed
            if (seconds < 10){
                timerText.GetComponent<TMP_Text>().text = minute + ":0" + seconds;
            }
            else{
                timerText.GetComponent<TMP_Text>().text = minute + ":" + seconds;
            }
        }
    }

    // Starts the next wave
    public void StartWave(int wave){
        currentWave = wave;
        waveText.SetActive(true);
        waveText.GetComponent<TMP_Text>().text = "Wave " + currentWave;
    }

    public void CompleteWave(int wave){
        waveText.GetComponent<TMP_Text>().text = "Wave Completed";
    }
    
}
