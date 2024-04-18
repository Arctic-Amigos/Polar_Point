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

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.Play("Door");
        var pgm = new PersistentGameManager();
        pgm.SaveInfo();
        SceneManager.LoadScene(newSceneName);
    }
}
