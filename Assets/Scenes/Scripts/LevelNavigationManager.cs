using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNavigationManager : MonoBehaviour
{
    public List<GameObject> EntryPoints;
    // Start is called before the first frame update
    void Start()
    {
        if (PersistentGameManager.LevelEntryPoint == -1) return;

        SetPlayerPositionAndRotation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetPlayerPositionAndRotation()
    {
        GameObject player = GameObject.FindObjectOfType<PlayerMovement>().gameObject;
        int index = PersistentGameManager.LevelEntryPoint;
        player.transform.position = EntryPoints[index].transform.position;
        PlayerMovement.currentRotation.x = PersistentGameManager.LevelRotationX;
        PlayerMovement.currentRotation.y = PersistentGameManager.LevelRotationY;
    }
}
