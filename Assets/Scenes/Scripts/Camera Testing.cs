using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosScreen = Input.mousePosition;

        // Convert the mouse position from screen space to world space
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);

        // Ensure the z position is appropriate for your setup
        mousePosWorld.y = 0f;

        // Update the position of the GameObject
        transform.position = mousePosWorld;
        Debug.Log("Mouse: " + mousePosWorld.x + ", " + mousePosWorld.y + ", " + mousePosWorld.z);
        Debug.Log("Chisel: " + transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
    }
}
