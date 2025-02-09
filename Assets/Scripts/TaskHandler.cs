using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TaskHandler : MonoBehaviour
{
    private Slider taskProgressBar;
    private string currentTask;
    private string taskMethod;
    private bool isHolding;
    private float holdTime;
    private bool canPressForTimingTask;

    private float indicatorPosition;
    private bool movingForward = true;

    // assign slider when task starts
    public void AssignSlider(Slider slider)
    {
        taskProgressBar = slider;
        if (taskProgressBar != null)
        {
            taskProgressBar.gameObject.SetActive(true); 
            taskProgressBar.value = 0; //reset bar js in case
        }
    }

    //clearing slider
    public void ClearSlider()
    {
        if (taskProgressBar != null)
        {
            taskProgressBar.gameObject.SetActive(false); 
        }
        taskProgressBar = null;
    }

    public void StartTask(string task, string method)
    {
        currentTask = task;
        taskMethod = method;

        Debug.Log($"Task Available: {task} (Method: {method})");

        if (taskMethod == "hold")
        {
            StartCoroutine(HoldTaskCoroutine());
        }
        else if (taskMethod == "timing")
        {
            StartCoroutine(TimingTaskCoroutine());
        }
    }

    public void StopTask()
    {
        currentTask = null;
        taskMethod = null;
        isHolding = false;
        holdTime = 0;
        canPressForTimingTask = false;

        if (taskProgressBar != null)
        {
            taskProgressBar.gameObject.SetActive(false);
        }

        StopAllCoroutines();
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(currentTask))
        {
            if (taskMethod == "hold")
            {
                HandleHoldTask();
            }
            else if (taskMethod == "timing")
            {
                HandleTimingTask();
            }
        }
    }

    //holding E
    private void HandleHoldTask()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (!isHolding)
            {
                isHolding = true;
                StartCoroutine(HoldTaskCoroutine());
            }
        }
        else
        {
            isHolding = false;
            holdTime = 0;
            if (taskProgressBar != null)
                taskProgressBar.value = 0;
        }
    }

    private IEnumerator HoldTaskCoroutine()
    {
        while (holdTime < 5f)
        {
            if (!Input.GetKey(KeyCode.E)) yield break;

            holdTime += Time.deltaTime;
            if (taskProgressBar != null)
                taskProgressBar.value = holdTime / 5f; // Updates the task's progress bar UI

            yield return null;
        }

        Debug.Log("Hold task completed!");
        CompleteTask();
    }

    //press E at right time
    private IEnumerator TimingTaskCoroutine()
    {
        indicatorPosition = 0f;
        movingForward = true;

        while (true)
        {
            if (indicatorPosition >= 1f)
            {
                movingForward = false;
            }
            if (indicatorPosition <= 0f)
            {
                movingForward = true;
            }

            indicatorPosition += movingForward ? 0.02f : -0.02f;
            if (taskProgressBar != null) { 
                taskProgressBar.value = indicatorPosition;
            }
            //press E from time 40% to 60%
            canPressForTimingTask = indicatorPosition > 0.4f && indicatorPosition < 0.6f;

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void HandleTimingTask()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canPressForTimingTask)
            {
                Debug.Log("timing task successful");
                CompleteTask();
            }
            else
            {
                Debug.Log("missed correct timing!");
            }
        }
    }

    private void CompleteTask()
    {
        Debug.Log("task " + currentTask +  " completed");
        StopTask();
    }
}
