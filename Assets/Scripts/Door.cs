using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public string newSceneName;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider player)
    {
        SceneManager.LoadScene(newSceneName);
    }

    //Just need to get player in scene 1 so can move into door
        //So can set rotation and position when load into new scene (not destroy)
        //And to attach other persistent scripts to

    //also need to figure out how we want door system to work 
}
