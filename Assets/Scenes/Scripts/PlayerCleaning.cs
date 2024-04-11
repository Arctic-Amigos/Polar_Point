using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCleaning : MonoBehaviour
{
    Inventory inventory;
    GameObject currentCleaningObject = null;
    public Animator brushAnimator;
    private bool isBrushing = false;


    void Start()
    {
        inventory = GetComponent<Inventory>(); // Ensure the player has the Inventory component
    }

    void Update()
    {
       
        inventory.SetScrollingAllowed();
        if (Input.GetMouseButton(0) && inventory.inventory_pos == -1)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                // Start cleaning if the hit object is brushable and not currently cleaning
                if (hit.collider.CompareTag("Brushable") && currentCleaningObject == null)
                {
                    currentCleaningObject = hit.collider.gameObject;
                    Clean(currentCleaningObject);
                    FindObjectOfType<AudioManager>().Play("Brushing");
                    if (!isBrushing) // Check if the coroutine is not already running
                    {
                        StartCoroutine(BrushAnim());
                    }
                }
                else if (hit.collider.gameObject != currentCleaningObject)
                {
                    // If we hit a different object, stop cleaning the current one
                    StopCleaning();
                }
            }
            else
            {
                // If we didn't hit anything, stop cleaning
                StopCleaning();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // When releasing the button, stop cleaning
            StopCleaning();
        }
    }

    void Clean(GameObject objectToClean)
    {
        var cleanable = objectToClean.GetComponent<Cleaning>();
        if (cleanable != null)
        {
            cleanable.StartCleaning(); // Start the cleaning process
            isBrushing = true;
        }
    }

    void StopCleaning()
    {
        if (currentCleaningObject != null)
        {
            var cleanable = currentCleaningObject.GetComponent<Cleaning>();
            if (cleanable != null)
            {
                cleanable.StopCleaning();
            }
            currentCleaningObject = null;
            FindObjectOfType<AudioManager>().Stop("Brushing");
            isBrushing = false; 
            brushAnimator.SetBool("brushActive", false);
        }
    }
    public IEnumerator BrushAnim()
    {
        // Loop as long as the currentCleaningObject is not null, indicating cleaning is active.
        while (currentCleaningObject != null)
        {
            brushAnimator.SetBool("brushActive", true);
            yield return null; // This makes the coroutine wait until the next frame before continuing.
        }
        
    }
    

}


