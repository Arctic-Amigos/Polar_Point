using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPodiumInteract : MonoBehaviour
{
    Podium currentPodium = null;
    GameObject Pedestal;
    Transform LeftLeg;
    Transform RightLeg;
    Transform Body;
    Transform LeftArm;
    Transform RightArm;
    Transform Tail;
    Transform Head;

    Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the bones on the specific podium
        if (currentPodium != null)
        {
            LeftLeg = currentPodium.transform.GetChild(0).GetChild(0);
            RightLeg = currentPodium.transform.GetChild(0).GetChild(1);
            Body = currentPodium.transform.GetChild(0).GetChild(2);
            LeftArm = currentPodium.transform.GetChild(0).GetChild(3);
            RightArm = currentPodium.transform.GetChild(0).GetChild(4);
            Tail = currentPodium.transform.GetChild(0).GetChild(5);
            Head = currentPodium.transform.GetChild(0).GetChild(6);
            Debug.Log("You are near the " + currentPodium.tag);
        }
        if(currentPodium != null && currentPodium.tag == "CarnotaurusPodium")
        {
            if (Input.GetKeyDown(KeyCode.F) && inventory.inventory_pos >= 0 && inventory.GetInventory(inventory.inventory_pos) == "Carnotaurus")
            {
                Debug.Log("You placed a Carnotaurus bone");
            }
        }else if(currentPodium.tag == "TriceratopsPodium")
        {
            if (Input.GetKeyDown(KeyCode.F) && inventory.inventory_pos >= 0 && inventory.GetInventory(inventory.inventory_pos) == "Triceratops")
            {
                Debug.Log("You placed a Triceratops bone");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Podium obj;
        if (other.TryGetComponent(out obj))
        {
            Debug.Log("called");
            currentPodium = obj;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentPodium = null;
    }
}
