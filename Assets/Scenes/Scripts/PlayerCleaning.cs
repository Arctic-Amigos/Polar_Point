using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCleaning : MonoBehaviour
{
    Inventory inventory;
    //public Animator brushAnimator;

    void Start()
    {
        inventory = GetComponent<Inventory>(); // Ensure the player has the Inventory component
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && inventory.inventory_pos == -3)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                
                // Check if the hit object is brushable
                
                if (hit.collider.CompareTag("Brushable"))
                {
                    Clean(hit.collider.gameObject);
                    FindObjectOfType<AudioManager>().Play("Brushing");
                }
                
            }
        }
    }
    void Clean(GameObject objectToClean)
    {
        // Assuming the object to clean has a component that manages cleaning stages
        var cleanable = objectToClean.GetComponent<Cleaning>(); // Ensure you have a Cleanable component or similar
        if (cleanable != null)
        {
            cleanable.StartCleaning(); // Call a method to start the cleaning process
            
        }
    }
}
