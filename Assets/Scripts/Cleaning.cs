using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaning : MonoBehaviour
{
    public Material[] cleaningStages; // Assign the layers (dirt) in the inspector
    private Renderer objectRenderer;
    public int currentStage = 0; // Current layer being cleaned
    private bool isCleaning = false;
    //bone currently placed on workbench
    public int currentBoneOnWorkbench = -5;
    //see if workbench has a bone on it
    private bool workBenchFull = false;
    public ObjectChiselable bone;
    //need to add something for is fully chiseled

    Inventory inventory;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("Inventory component not found on " + gameObject.name);
        }
        UpdateMaterial();
    }

    void Update()
    {

        //bool hasBrush = inventory.inventory_pos == -3; //will change once inventory script works
        if (Input.GetKeyDown(KeyCode.F) && workBenchFull == false)
        {
            /*
           
            //Get the inventory slot of which bone was placed onto the workbench
            currentBoneOnWorkbench = inventory.inventory_pos;
            workBenchFull = true;
            inventory.inventory_pos = -1;

            //remove bone from inventory and disable players ability to scroll
            inventory.SetInventory(currentBoneOnWorkbench, null);
            inventory.SetScrollingNotAllowed();
            */

            bone.gameObject.SetActive(true);
            Transform rockLayer = bone.transform.GetChild(0);
            Transform heavyDirtLayer = bone.transform.GetChild(1);
            Transform lightDirtLayer = bone.transform.GetChild(2);

            if (currentStage == 3)
            {
                //Could set tag to brushable to start the brushing feature
                Debug.Log("Bone is fully cleaned");
            }
        }
        //if player interacts with workbench with a bone on the table remove the bone and set the players inventory position to where the bone was previously

        if (Input.GetMouseButton(0) && !isCleaning )
        {
            StartCoroutine(CleaningProgress());
        }
    }

    IEnumerator CleaningProgress()
    {
        isCleaning = true;
        yield return new WaitForSeconds(0.5f); // Simulate cleaning effort

        if (currentStage < cleaningStages.Length - 1)
        {
            currentStage++;
            UpdateMaterial();
        }

        isCleaning = false;
    }

    void UpdateMaterial()
    {
        objectRenderer.material = cleaningStages[currentStage];
    }
}
