using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCleaning : MonoBehaviour
{
    Inventory inventory;
    GameObject currentCleaningObject = null;
    public Animator brushAnimator;
    private bool isBrushing = false;
    public Dictionary<int, int> boneCleaningState = new Dictionary<int, int>();
    public Dictionary<int, string> boneCleaningTag = new Dictionary<int, string>();

    public GameObject bone;

    WorkbenchInteract workbenchInteract;
    GameObject workbenchBrush;


    void Start()
    {
        inventory = GetComponent<Inventory>(); // Ensure the player has the Inventory component
        workbenchInteract = GetComponent<WorkbenchInteract>();
    }

    void Update()
    {
        if (inventory.inventory_pos == -1)
        {
            workbenchBrush = workbenchInteract.wbBrush;
        }
        StartCoroutine(BrushAnim());
        inventory.SetScrollingAllowed();

        // Check if the mouse button is pressed and the workbench brush is within the bounds of the bone's capsule collider
        if (Input.GetMouseButton(0) && inventory.inventory_pos == -1 && WithinBounds(workbenchBrush.transform.position, bone) && !bone.GetComponent<Cleaning>().finishedBrushing)
        {
            // Start cleaning if the workbench brush is within bounds and not currently cleaning
                // No need for raycasting, since we're already checking bounds
                // Just start cleaning the current object
                Clean(bone);
                FindObjectOfType<AudioManager>().Play("Brushing");
                if (!isBrushing) // Check if the coroutine is not already running
                {
                    StartCoroutine(BrushAnim());
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
    bool WithinBounds(Vector3 position, GameObject obj)
    {
        SphereCollider sphereCollider = obj.GetComponent<SphereCollider>();
        if (sphereCollider != null)
        {
            Vector3 colliderCenter = obj.transform.TransformPoint(sphereCollider.center);
            float radius = sphereCollider.radius * obj.transform.lossyScale.x;
            Vector2 pointOnPlane = new Vector2(position.x, position.z);
            Vector2 colliderCenterOnPlane = new Vector2(colliderCenter.x, colliderCenter.z);

            if (Vector2.Distance(pointOnPlane, colliderCenterOnPlane) <= radius)
            {
                return true;
            }
        }
        return false;
    }




}


