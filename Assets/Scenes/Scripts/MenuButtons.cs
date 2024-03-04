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
        SceneManager.LoadScene(newSceneName);
    }
    public void QuitBtn()
    {
        Application.Quit();
    }

}
