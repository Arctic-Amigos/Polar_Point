using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChiseling : MonoBehaviour

{
    //check to see if player is currently chiseling
    private bool isChiseling = false;

    //Set this to Player so that the rays ignore the players model also set this to DirtCoveringBone
    public LayerMask layerMaskToIgnore;

    Inventory inventory;

    private GameObject currentBoneOnTable;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
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
        _chiselableObject.IncrementChiselCount(this.inventory.inventory_pos);
        isChiseling = false;
    }
}
