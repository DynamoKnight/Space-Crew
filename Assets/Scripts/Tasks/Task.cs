using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    // Indicates if the task is completed
    public bool isCompleted = false;
    // Indicates if the player has started the task
    public bool isStarted = false;

    public virtual void Start(){
        
    }

    public virtual void Update(){
        
    }

    public virtual void OnTriggerEnter2D(Collider2D other){
        
    }

    public virtual void OnTriggerExit2D(Collider2D other){
        
    }

    // Shows the task
    public virtual void StartTask(){
        
    }
    // Hides the task
    public virtual void EndTask(){
        
    }

    // Progresses the task
    public virtual IEnumerator ProgressTask(){
        yield return null;
        CompleteTask();
    }

    // Marks the task as completed
    public virtual void CompleteTask(){
        Debug.Log("Task completed");
        isCompleted = true;
    }

    // Sets task back to beginning
    public virtual void ResetTask(){
        
    }
}
