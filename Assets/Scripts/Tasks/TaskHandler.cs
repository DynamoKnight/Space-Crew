using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TaskHandler : MonoBehaviour
{
    // The slider used for showing task progression
    private Slider taskSlider;
    private string currentTask;
    private string taskMethod;
    // Indicates if the player is holding the interact key down
    private bool isHolding;
    // Records the time holding (sec)
    private float holdTime;
    // Indicates if the indicator is on the target
    private bool onTarget;
    // The value position for the slider
    private float indicatorPosition;
    // The direction of the slider's movement
    private bool movingForward = true;

    // Assigns the slider for the given task
    public void AssignSlider(Slider slider){
        taskSlider = slider;
        if (taskSlider != null){
            taskSlider.gameObject.SetActive(true); 
            // Resets bar value
            taskSlider.value = 0;
        }
    }

    // Disables the slider
    public void ClearSlider(){
        if (taskSlider != null){
            taskSlider.gameObject.SetActive(false); 
        }
        taskSlider = null;
    }

    // Starts the task depending on what it is
    public void StartTask(string task, string method){
        currentTask = task;
        taskMethod = method;

        Debug.Log($"Task Available: {task} (Method: {method})");
        // Starts the holding task
        if (taskMethod == "hold"){
            StartCoroutine(HoldTaskCoroutine());
        }
        // Starts the Precisio task
        else if (taskMethod == "Precision"){
            StartCoroutine(PrecisionTaskCoroutine());
        }
    }

    // Completely disables the task
    public void StopTask(){
        // Resets all values
        currentTask = null;
        taskMethod = null;
        isHolding = false;
        holdTime = 0;
        onTarget = false;
        // Hides slider
        if (taskSlider != null){
            taskSlider.gameObject.SetActive(false);
        }
        StopAllCoroutines();
    }

    private void Update(){
        if (!string.IsNullOrEmpty(currentTask)){
            if (taskMethod == "hold"){
                HandleHoldTask();
            }
            else if (taskMethod == "Precision"){
                HandlePrecisionTask();
            }
        }
    }

    //holding E
    private void HandleHoldTask(){
        if (Input.GetKey(KeyCode.E)){
            // Begins holding process
            if (!isHolding){
                isHolding = true;
                StartCoroutine(HoldTaskCoroutine());
            }
        }
        // Holding ends
        else{
            isHolding = false;
            holdTime = 0;
            taskSlider.value = 0;
        }
    }

    //
    private IEnumerator HoldTaskCoroutine(){
        while (holdTime < 5f){
            // If hold is let go, it ends
            if (!Input.GetKey(KeyCode.E)){
                yield break;
            }
            // Updates the tasks progress
            holdTime += Time.deltaTime;
            taskSlider.value = holdTime / 5f;
            yield return null;
        }
        // Timer has exceeded duration
        Debug.Log("Hold task completed!");
        CompleteTask();
    }

    //press E at right time
    private IEnumerator PrecisionTaskCoroutine(){
        indicatorPosition = 0f;
        movingForward = true;

        while (true){
            // Moves in negative direction
            if (indicatorPosition >= 1f){
                movingForward = false;
            }
            // Moves in position direction
            if (indicatorPosition <= 0f){
                movingForward = true;
            }
            // Increments or decrements
            indicatorPosition += movingForward ? 0.02f : -0.02f;
            taskSlider.value = indicatorPosition;
            // Indicates whether the indicator was clicked within the target
            onTarget = indicatorPosition > 0.4f && indicatorPosition < 0.6f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void HandlePrecisionTask(){
        // Finds out if clicked on target
        if (Input.GetKeyDown(KeyCode.E)){
            if (onTarget){
                Debug.Log("Precision task successful");
                CompleteTask();
            }
            else{
                Debug.Log("missed correct timing!");
            }
        }
    }

    private void CompleteTask(){
        Debug.Log("task " + currentTask +  " completed");
        StopTask();
    }
}
