using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrecisionTask : Task
{
    // The speed of the indicator
    public float indicatorSpeed = 0.03f;
    // The lower bound for the target
    public float targetLow = 0.45f;
    // The upper bound for the target
    public float targetHigh = 0.55f;
    // Indicates if the indicator is on the target
    private bool onTarget;
    // The value position for the slider
    private float indicatorPosition = 0f;
    // The direction of the slider's movement
    private bool movingForward = true;

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
        // Starts moving indicator
        StartCoroutine(ProgressTask());
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
            // Finds out if clicked on target
            if (Input.GetKeyDown(KeyCode.E)){
                if (onTarget){
                    CompleteTask();
                }
                else{
                    Debug.Log("You missed!");
                }
            }
        }
    }

    // Progresses the task as long as the player is holding
    public override IEnumerator ProgressTask(){
        while (!isCompleted && isStarted){
            // Moves in negative direction
            if (indicatorPosition >= 1f){
                movingForward = false;
            }
            // Moves in position direction
            if (indicatorPosition <= 0f){
                movingForward = true;
            }
            // Increments or decrements
            indicatorPosition += movingForward ? indicatorSpeed : -indicatorSpeed;
            taskSlider.value = indicatorPosition;
            // Indicates whether the indicator was clicked within the target
            onTarget = indicatorPosition > targetLow && indicatorPosition < targetHigh;
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Marks the task as completed
    public override void CompleteTask(){
        Debug.Log("Precision task completed");
        isCompleted = true;
    }

    // Sets task back to beginning
    public override void ResetTask(){
        indicatorPosition = 0f;
        movingForward = true;
        taskSlider.value = 0;
    }
}
