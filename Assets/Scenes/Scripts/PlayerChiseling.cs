using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerChiseling : MonoBehaviour

{
    //check to see if player is currently chiseling
    private bool isChiseling = false;

    //bone currently placed on workbench
    public int currentBoneOnWorkbench = -5;

    //see if workbench has a bone on it
    private bool workBenchFull = false;

    //Set this to Player so that the rays ignore the players model also set this to DirtCoveringBone
    public LayerMask layerMaskToIgnore;

    //Get bone from workbench
    public ObjectChiselable bone;

    public Animator chiselAnimator;

    public int chiselValue = 0;

    Inventory inventory;

    WorkbenchInteract workbench;

    public Dictionary<int, int> boneChiselCount = new Dictionary<int, int>();

    void Start()
    {
        inventory = GetComponent<Inventory>();
        workbench = GetComponent<WorkbenchInteract>();

        boneChiselCount.Add(0, 0);
        boneChiselCount.Add(1, 0);
        boneChiselCount.Add(2, 0);
        boneChiselCount.Add(3, 0);
        boneChiselCount.Add(4, 0);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ChiselAnim());
        //if player interacts with workbench with a bone in their hand allow them to start chiseling
        if (Input.GetKeyDown(KeyCode.F) && inventory.inventory_pos >= 0 && workBenchFull == false) //&& workbench.IsWorkbenchInteracting() add this back
        {
            //Get the inventory slot of which bone was placed onto the workbench
            currentBoneOnWorkbench = inventory.inventory_pos;
            workBenchFull = true;
            inventory.inventory_pos = -2;

            //remove bone from inventory and disable players ability to scroll
            inventory.SetInventory(currentBoneOnWorkbench, null);
            inventory.SetScrollingNotAllowed();

            bone.gameObject.SetActive(true);

            chiselValue = boneChiselCount[currentBoneOnWorkbench]; //get the chisel state of the bone currently placed on workbench
            Transform topLeft = bone.transform.GetChild(0);
            Transform topRight = bone.transform.GetChild(1);
            Transform bottomLeft = bone.transform.GetChild(2);
            Transform bottomRight = bone.transform.GetChild(3);
            if (chiselValue == 1)
            {
                topLeft.gameObject.SetActive(false);
                topRight.gameObject.SetActive(true);
                bottomLeft.gameObject.SetActive(true);
                bottomRight.gameObject.SetActive(true);
            }else if (chiselValue == 2)
            {
                topLeft.gameObject.SetActive(false);
                topRight.gameObject.SetActive(false);
                bottomLeft.gameObject.SetActive(true);
                bottomRight.gameObject.SetActive(true);
            }
            else if (chiselValue == 3) {
                topLeft.gameObject.SetActive(false);
                topRight.gameObject.SetActive(false);
                bottomLeft.gameObject.SetActive(false);
                bottomRight.gameObject.SetActive(true);
            }else if (chiselValue == 4)
            {
                //Could set tag to brushable to start the brushing feature
                Debug.Log("Bone is fully chiseled");
                bone.gameObject.SetActive(false);
            }
            else
            {
                topLeft.gameObject.SetActive(true);
                topRight.gameObject.SetActive(true);
                bottomLeft.gameObject.SetActive(true);
                bottomRight.gameObject.SetActive(true);
            }
        }
        //if player interacts with workbench with a bone on the table remove the bone and set the players inventory position to where the bone was previously
        else if(Input.GetKeyDown(KeyCode.F) && workBenchFull == true) //&& workbench.IsWorkbenchInteracting() add this back
        {
            chiselValue = boneChiselCount[currentBoneOnWorkbench]; //get the chisel state of the bone currently placed on workbench
            inventory.inventory_pos = currentBoneOnWorkbench;
            currentBoneOnWorkbench = -5; //random number not equal to a slot in the inventory
            workBenchFull = false;

            //set the bone back into the inventory and reenable players ability to scroll
            if (chiselValue == 1)
            {
                //chiseled once
                inventory.SetInventory(inventory.inventory_pos, "ChiselableBone1");
            }
            else if (chiselValue == 2)
            {
                //chiseled twice
                inventory.SetInventory(inventory.inventory_pos, "ChiselableBone2");
            }
            else if (chiselValue == 3)
            {
                //chiseled three times
                inventory.SetInventory(inventory.inventory_pos, "ChiselableBone3");
            }
            else if (chiselValue == 4) 
            {
                //completely chiseled
                inventory.SetInventory(inventory.inventory_pos, "CleanBone");
            }
            else
            {
                //completely unchiseled
                inventory.SetInventory(inventory.inventory_pos, "ChiselableBone");
            }
            inventory.SetScrollingAllowed();

            bone.gameObject.SetActive(false);
        }

        if(Input.GetMouseButtonDown(0) && workBenchFull && inventory.inventory_pos == -2) //&& workbench.IsWorkbenchInteracting() add this back
        {
            if(!isChiseling)
            {
                /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitinfo;

                if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, ~layerMaskToIgnore))
                {
                    ObjectChiselable chiselableObject = hitinfo.collider.gameObject.GetComponent<ObjectChiselable>();
                    
                    if (chiselableObject != null)
                    {
                        //do stuff when player clicks chiselable object
                        StartCoroutine(Chiseling(chiselableObject));
                        AudioManager.instance.Play("Chisel");

                    }
                }*/
                
                AudioManager.instance.Play("Chisel");
                StartCoroutine(Chiseling(bone));
                
            }
        }
    }

    IEnumerator ChiselAnim()
    {
        if (Input.GetMouseButton(0))
        {
            chiselAnimator.SetBool("chiselingActive", true);
            yield return new WaitForSeconds(.3f);
        }
        else
        {
            chiselAnimator.SetBool("chiselingActive", false);
        }
    }

    IEnumerator Chiseling(ObjectChiselable _chiselableObject)
    {
    isChiseling = true;
    float startTime = 0f;
    float holdTime = 3.0f;

        

    while(Input.GetMouseButton(0))
    {
        startTime = Time.time;
        if(startTime + holdTime >= Time.time)
        {
            yield return null;
        }
    }
  
    if (boneChiselCount.ContainsKey(currentBoneOnWorkbench))
    {
        boneChiselCount[currentBoneOnWorkbench]++;
    }
    else
    {
        boneChiselCount[currentBoneOnWorkbench] = 1;
    }
    _chiselableObject.IncrementChiselCount(currentBoneOnWorkbench);
        
    isChiseling = false;
    AudioManager.instance.Stop("Chisel");
    }
}
