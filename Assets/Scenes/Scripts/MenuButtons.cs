using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public string newSceneName;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void StartBtn()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        SceneManager.LoadScene(newSceneName);
    }
    public void QuitBtn()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        Application.Quit();
    }

}
