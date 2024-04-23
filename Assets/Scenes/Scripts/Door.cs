using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public string newSceneName;
    public int LevelEntryPoint;
    public float LevelRotationX;
    public float LevelRotationY;

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
        LevelRotationX = (PlayerMovement.currentRotation.x + 180) % 360;
        LevelRotationY = PlayerMovement.currentRotation.y;
        PersistentGameManager.SetTargetLevel(newSceneName, LevelEntryPoint, LevelRotationX, LevelRotationY);
        SceneManager.LoadScene(newSceneName);
    }
}
