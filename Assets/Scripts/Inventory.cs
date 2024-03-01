using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // First 3 spots in inventory will be static for hand, pickaxe, brush. Last 5 spots will be open for fossils/bones
    // Hand, Pickaxe, Brush | Fossil, Fossil, Fossil, Fossil, Fossil
    // -3  , -2     , -1    | 0     , 1     , 2     , 3     , 4
    string[] fossil_inventory = new string[5]; // Inventory spots that will be filled up with fossils/bones. Type can be changed to fossil/bone type as needed
    public int inventory_pos; // Position in inventory that will be scrolled through
    bool scrolling_allowed = true;
    GameObject Bone1;
    GameObject Bone2;
    GameObject Bone3;
    GameObject Bone4;
    GameObject Bone5;
    GameObject Pickaxe;
    GameObject ChiselableBone;
    // Start is called before the first frame update
    void Start()
    {
        inventory_pos = -3;
        StartDisplayBones();
        StartDisplayHeldObject();
    }

    // Update is called once per frame
    void Update()
    {
        // Number button functionality
        if (Input.GetKeyDown(KeyCode.Alpha1) && scrolling_allowed)
            inventory_pos = -3;
        if (Input.GetKeyDown(KeyCode.Alpha2) && scrolling_allowed)
            inventory_pos = -2;
        if (Input.GetKeyDown(KeyCode.Alpha3) && scrolling_allowed)
            inventory_pos = -1;
        if (Input.GetKeyDown(KeyCode.Alpha4) && scrolling_allowed)
            inventory_pos = 0;
        if (Input.GetKeyDown(KeyCode.Alpha5) && scrolling_allowed)
            inventory_pos = 1;
        if (Input.GetKeyDown(KeyCode.Alpha6) && scrolling_allowed)
            inventory_pos = 2;
        if (Input.GetKeyDown(KeyCode.Alpha7) && scrolling_allowed)
            inventory_pos = 3;
        if (Input.GetKeyDown(KeyCode.Alpha8) && scrolling_allowed)
            inventory_pos = 4;
        // Scrolling functionality
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0 && scrolling_allowed)
            inventory_pos--;
        else if (scroll < 0 && scrolling_allowed)
            inventory_pos++;
        if (inventory_pos > 4)
            inventory_pos = -3;
        if (inventory_pos < -3)
            inventory_pos = 4;
        // Other functions
        UpdateDisplayBones();
        UpdateDisplayHeldObject();
    }
    public string GetInventory(int index)
    {
        return fossil_inventory[index];
    }

    public void SetInventory(int index, string item)
    {
        fossil_inventory[index] = item;
    }
    public bool IndexIsEmpty(int index) // Returns whether an index is empty
    {
        if (fossil_inventory[index] == null) 
            return true;
        return false;
    }
    public int FirstEmpty() // Returns the index of the first empty index or 9 if all inventory slots are full
    {
        if (IndexIsEmpty(0))
            return 0;
        else if (IndexIsEmpty(1))
            return 1;
        else if (IndexIsEmpty(2))
            return 2;
        else if (IndexIsEmpty(3))
            return 3;
        else if (IndexIsEmpty(4))
            return 4;
        return 9;
    }
    public void CheckAndSetBone(int x) // Helper function for setting held object
    {
        if (GetInventory(x) == "ChiselableBone")
            ChiselableBone.SetActive(true);
        if (GetInventory(x) == null)
            ChiselableBone.SetActive(false);
    }
    public void StartDisplayBones()
    {
        Bone1 = GameObject.FindWithTag("Bone1Tag");
        Bone2 = GameObject.FindWithTag("Bone2Tag");
        Bone3 = GameObject.FindWithTag("Bone3Tag");
        Bone4 = GameObject.FindWithTag("Bone4Tag");
        Bone5 = GameObject.FindWithTag("Bone5Tag");

        Bone1.SetActive(false);
        Bone2.SetActive(false);
        Bone3.SetActive(false);
        Bone4.SetActive(false);
        Bone5.SetActive(false);
    }
    public void UpdateDisplayBones()
    {
        if (GetInventory(0) == "ChiselableBone")
            Bone1.SetActive(true);
        if (GetInventory(1) == "ChiselableBone")
            Bone2.SetActive(true);
        if (GetInventory(2) == "ChiselableBone")
            Bone3.SetActive(true);
        if (GetInventory(3) == "ChiselableBone")
            Bone4.SetActive(true);
        if (GetInventory(4) == "ChiselableBone")
            Bone5.SetActive(true);

        if (GetInventory(0) == null)
            Bone1.SetActive(false);
        if (GetInventory(1) == null)
            Bone2.SetActive(false);
        if (GetInventory(2) == null)
            Bone3.SetActive(false);
        if (GetInventory(3) == null)
            Bone4.SetActive(false);
        if (GetInventory(4) == null)
            Bone5.SetActive(false);
    }
    public void StartDisplayHeldObject()
    {
        Pickaxe = GameObject.FindWithTag("PickaxeTag");
        ChiselableBone = GameObject.FindWithTag("HeldChiselableBoneTag");

        Pickaxe.SetActive(false);
    }
    public void UpdateDisplayHeldObject()
    {
        if (inventory_pos == -3)
        {
            Pickaxe.SetActive(false);
            ChiselableBone.SetActive(false);
        }
        else if (inventory_pos == -2)
        {
            Pickaxe.SetActive(true);
            ChiselableBone.SetActive(false);
        }
        else if (inventory_pos == -1)
        {
            Pickaxe.SetActive(false);
            ChiselableBone.SetActive(false);
        }
        else if (inventory_pos == 0)
        {
            Pickaxe.SetActive(false);
            CheckAndSetBone(0);
        }
        else if (inventory_pos == 1)
        {
            Pickaxe.SetActive(false);
            CheckAndSetBone(1);
        }
        else if (inventory_pos == 2)
        {
            Pickaxe.SetActive(false);
            CheckAndSetBone(2);
        }
        else if (inventory_pos == 3)
        {
            Pickaxe.SetActive(false);
            CheckAndSetBone(3);
        }
        else if (inventory_pos == 4)
        {
            Pickaxe.SetActive(false);
            CheckAndSetBone(4);
        }
    }
    public void SetScrollingAllowed()
    {
        scrolling_allowed = true;
    }
    public void SetScrollingNotAllowed()
    {
        scrolling_allowed = false;
    }

}
