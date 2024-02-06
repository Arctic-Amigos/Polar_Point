using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // First 3 spots in inventory will be static for hand, pickaxe, brush. Last 5 spots will be open for fossils/bones
    // Hand, Pickaxe, Brush | Fossil, Fossil, Fossil, Fossil, Fossil
    // -3  , -2     , -1    | 0     , 1     , 2     , 3     , 4
    string[] inventory = new string[5]; // Inventory spots that will be filled up with fossils/bones. Type can be changed to fossil/bone type as needed
    int inventory_pos; // Position in inventory that will be scrolled through
    // Start is called before the first frame update
    void Start()
    {
        inventory_pos = -3;
    }

    // Update is called once per frame
    void Update()
    {
        // Will add scrolling and number button functionality
        if (Input.GetKeyDown(KeyCode.Alpha1))
            inventory_pos = -3;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            inventory_pos = -2;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            inventory_pos = -1;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            inventory_pos = 0;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            inventory_pos = 1;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            inventory_pos = 2;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            inventory_pos = 3;
        if (Input.GetKeyDown(KeyCode.Alpha8))
            inventory_pos = 4;
    }
}
