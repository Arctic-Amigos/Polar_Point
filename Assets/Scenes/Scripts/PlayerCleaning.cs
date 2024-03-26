using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCleaning : MonoBehaviour
{
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>(); // Ensure the player has the Inventory component
    }

    void Update()
    {
        if (Input.GetMouseButton(0) )
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {

                // Check if the hit object is brushable
                if (hit.collider.CompareTag("Brushable"))
                {
                    Clean(hit.collider.gameObject);
                }
                
            }
        }
    }
    void Clean(GameObject objectToClean)
    {
        
        var cleanable = objectToClean.GetComponent<Cleaning>(); // Ensure you have a Cleanable component or similar
        if (cleanable != null)
        {
            cleanable.StartCleaning(); // Call a method to start the cleaning process
        }
    }
}
