using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCleaning : MonoBehaviour
{
    Inventory inventory;
    GameObject currentCleaningObject = null;
    public Animator brushAnimator;
    private bool isBrushing = false;
    public Dictionary<int, int> boneCleaningState = new Dictionary<int, int>();
    public Dictionary<int, string> boneCleaningTag = new Dictionary<int, string>();

    WorkbenchInteract workbenchInteract;
    GameObject workbenchBrush;


    void Start()
    {
        inventory = GetComponent<Inventory>(); // Ensure the player has the Inventory component
        workbenchInteract = GetComponent<WorkbenchInteract>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("x: " + workbenchBrush.transform.position.x);
            Debug.Log("z: " + workbenchBrush.transform.position.z);
        }
        if (inventory.inventory_pos == -1)
        {
            workbenchBrush = workbenchInteract.wbBrush;
        }
        StartCoroutine(BrushAnim());
        inventory.SetScrollingAllowed();
        if (Input.GetMouseButton(0) && inventory.inventory_pos == -1 && WithinBounds(workbenchBrush.transform.position.x, workbenchBrush.transform.position.z))
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
                boneCleaningState[inventory.inventory_pos] = cleanable.currentStage;
                //inventory.UpdateCleaningState(inventory.inventory_pos, cleanable.currentStage);
            }
            currentCleaningObject = null;
            FindObjectOfType<AudioManager>().Stop("Brushing");
            isBrushing = false; 
            brushAnimator.SetBool("brushActive", false);
        }
    }
    public void SaveCleanState(int inventoryPos, int state, string tag)
    {
        boneCleaningState[inventoryPos] = state;
        boneCleaningTag[inventoryPos] = tag;
    }

    public void LoadCleanState(GameObject objectToClean, int inventoryPos)
    {
        if (objectToClean != null && inventory != null && boneCleaningState.ContainsKey(inventoryPos))
        {
            int stage = boneCleaningState[inventoryPos];
            string tag = boneCleaningTag[inventoryPos];

            Cleaning cleaningComponent = objectToClean.GetComponent<Cleaning>();
            if (cleaningComponent != null)
            {
                cleaningComponent.SetCleaningStage(stage);
                objectToClean.tag = tag;
            }
        }
    }
    public IEnumerator BrushAnim()
    {
        // Loop as long as the currentCleaningObject is not null, indicating cleaning is active.
        if (Input.GetMouseButton(0))
        {
            brushAnimator.SetBool("brushActive", true);
            yield return new WaitForSeconds(.3f);
        }
        else
        {
            brushAnimator.SetBool("brushActive", false);
        }
    }

    //HARDCODED TO BONES ON WORKBENCH IN 1.5 HOMEBASE
    bool WithinBounds(float x, float z)
    {
        if (x < 2.2f && x > 1.05f && z < 3.5f && z > 2.6f)
        {
            return true;
        }
        return false;
    }

}


