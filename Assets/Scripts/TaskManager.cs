using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    //all gotta be assigned in unity btw
    public string taskType;
    public string taskMethod;
    public Slider taskSlider; 

    private void Start()
    {
        //start slider at beginning of game
        if (taskSlider != null)
        {
            taskSlider.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CoopPlayer player = other.GetComponent<CoopPlayer>();
        if (player != null)
        {
            TaskHandler taskHandler = player.GetComponent<TaskHandler>();
            if (taskHandler != null)
            {
                taskHandler.AssignSlider(taskSlider);
                taskHandler.StartTask(taskType, taskMethod);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CoopPlayer player = other.GetComponent<CoopPlayer>();
        if (player != null)
        {
            TaskHandler taskHandler = player.GetComponent<TaskHandler>();
            if (taskHandler != null)
            {
                taskHandler.StopTask();
                taskHandler.ClearSlider();
            }
        }
    }
}
