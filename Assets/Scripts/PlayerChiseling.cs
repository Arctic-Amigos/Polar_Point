using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChiseling : MonoBehaviour

{
    //check to see if player is currently chiseling
    private bool isChiseling = false;

    //bone currently placed on workbench
    public int currentBoneOnWorkbench;

    //see if workbench has a bone on it
    private bool workBenchFull = false;

    //Set this to Player so that the rays ignore the players model also set this to DirtCoveringBone
    public LayerMask layerMaskToIgnore;

    //Get bone from workbench
    public ObjectChiselable bone;

    Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player interacts with workbench with a bone in their hand allow them to start chiseling
        if (Input.GetKeyDown(KeyCode.E) && inventory.inventory_pos >= 0 && workBenchFull == false)
        {
            currentBoneOnWorkbench = inventory.inventory_pos;
            workBenchFull = true;
            inventory.inventory_pos = -1;
            inventory.SetScrollingNotAllowed();

            bone.gameObject.SetActive(true);
            int chiselValue = bone.boneChiselCounts[currentBoneOnWorkbench];
            for (int i = 0; i <= chiselValue; i++)
            {
                Transform child = bone.transform.GetChild(i);
                child.gameObject.SetActive(false);
            }
        }
        //if player interacts with workbench with a bone on the table remove the bone and set the players inventory position to where the bone was previously
        else if(Input.GetKeyDown(KeyCode.E) && workBenchFull == true)
        {
            inventory.inventory_pos = currentBoneOnWorkbench;
            currentBoneOnWorkbench = -5; //random number not equal to a slot in the inventory
            workBenchFull = false;
            inventory.SetScrollingAllowed();
            bone.gameObject.SetActive(false);
        }

        if(Input.GetMouseButtonDown(0) && workBenchFull && inventory.inventory_pos == -1)
        {
            if (!isChiseling)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitinfo;

                if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, ~layerMaskToIgnore))
                {
                    ObjectChiselable chiselableObject = hitinfo.collider.gameObject.GetComponent<ObjectChiselable>();
                    
                    if (chiselableObject != null)
                    {
                        //do stuff when player clicks chiselable object
                        StartCoroutine(Chiseling(chiselableObject));

                    }
                }
            }
        }
    }
    IEnumerator Chiseling(ObjectChiselable _chiselableObject)
    {
        isChiseling = true;
        yield return new WaitForSeconds(3f);
        //in inventory we need to add a feature which disables switching inventory slots while cleaning
        _chiselableObject.IncrementChiselCount(currentBoneOnWorkbench);
        isChiseling = false;
    }
}
