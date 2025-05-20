using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HoldTask : Task
{
    // The time needed to finish the holding task
    public float holdDuration = 5f;
    // Indicates if the player is holding the interact key down
    private bool isHolding;
    // Records the time holding (sec)
    private float holdTime;

    // The slider used for showing task progression
    public Slider taskSlider; 

    public override void Start(){
        // Initializes the slider used for tasks
        taskSlider.gameObject.SetActive(false);
    }

    public override void OnTriggerEnter2D(Collider2D other){
        // Checks if player is within the task zone and the task hasn't been completed
        Player player = other.GetComponent<Player>();
        if (player != null && !isCompleted){
            // Starts the task
            StartTask();
        }
    }

    public override void OnTriggerExit2D(Collider2D other){
        Player player = other.GetComponent<Player>();
        // Checks if player left task zone and the task hasn't been completed
        if (player != null){
            EndTask();
        }
    }

    // Shows the task
    public override void StartTask(){
        taskSlider.gameObject.SetActive(true);
        isStarted = true;
    }
    // Hides the task
    public override void EndTask(){
        isStarted = false;
        if (!isCompleted){
            ResetTask();
        }
        taskSlider.gameObject.SetActive(false);
    }

    public override void Update(){
        if (!isCompleted && isStarted){
            // If interact key is held, the task continues progressing
            if (Input.GetKey(KeyCode.E)){
                // Begins holding process
                if (!isHolding){
                    isHolding = true;
                    StartCoroutine(ProgressTask());
                }
            }
            // If nothing is held, holding progress is reset
            else{
                ResetTask();
            }
        }
    }

    // Progresses the task as long as the player is holding
    public override IEnumerator ProgressTask(){
        while (holdTime < holdDuration){
            // If hold is let go, it ends
            if (!Input.GetKey(KeyCode.E)){
                Debug.Log("You let go!");
                yield break;
            }
            // Updates the tasks progress
            holdTime += Time.deltaTime;
            taskSlider.value = holdTime / holdDuration;
            yield return null;
        }
        // Timer has exceeded duration
        CompleteTask();
    }

    // Marks the task as completed
    public override void CompleteTask(){
        Debug.Log("Holding task completed");
        isCompleted = true;
    }

    // Sets task back to beginning
    public override void ResetTask(){
        isHolding = false;
        // Resets bar value
        holdTime = 0;
        taskSlider.value = 0;
    }
}
