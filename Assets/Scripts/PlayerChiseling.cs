using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChiseling : MonoBehaviour

{
    //Set this to Player so that the rays ignore the players model
    public LayerMask layerMaskToIgnore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;

            if(Physics.Raycast(ray, out hitinfo, Mathf.Infinity, ~layerMaskToIgnore))
            {
                if(hitinfo.collider.CompareTag("Chiselable"))
                {
                    //do stuff when player clicks object
                    Debug.Log(hitinfo.collider.gameObject.name + " Object clicked!");
                }
            }
        }
    }
}
