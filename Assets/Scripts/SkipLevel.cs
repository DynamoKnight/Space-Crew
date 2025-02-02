using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipLevel : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Pluto-Survival"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
