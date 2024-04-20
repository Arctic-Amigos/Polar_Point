using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // First 3 spots in inventory will be static for hand, pickaxe, brush. Last 5 spots will be open for fossils/bones
    // Hand, Pickaxe, Chisel, Brush | Fossil, Fossil, Fossil, Fossil, Fossil
    // -4  , -3     , -2    , -1    | 0     , 1     , 2     , 3     , 4
    public string[] fossil_inventory = new string[5]; // Inventory spots that will be filled up with fossils/bones. Type can be changed to fossil/bone type as needed
    public int inventory_pos; // Position in inventory that will be scrolled through
    bool scrolling_allowed = true;
    GameObject Box1;
    GameObject Box2;
    GameObject Box3;
    GameObject Box4;
    GameObject Box5;
    GameObject Box6;
    GameObject Box7;
    GameObject Box8;
    GameObject Box9;
    GameObject Bone1;
    GameObject Bone2;
    GameObject Bone3;
    GameObject Bone4;
    GameObject Bone5;
    GameObject Pickaxe;
    GameObject Chisel;
    GameObject ChiselableBone;
    GameObject ChiselableBone1;
    GameObject ChiselableBone2;
    GameObject ChiselableBone3;
    GameObject CleanBone;
    GameObject Brush;
    GameObject Spinosaurus;

    // Start is called before the first frame update
    void Start()
    {
        inventory_pos = -4;
        
        inventory_pos = PersistentGameManager.inventoryPos;
        fossil_inventory[0] = PersistentGameManager.inventory0;
        fossil_inventory[1] = PersistentGameManager.inventory1;
        fossil_inventory[2] = PersistentGameManager.inventory2;
        fossil_inventory[3] = PersistentGameManager.inventory3;
        fossil_inventory[4] = PersistentGameManager.inventory4;

        StartDisplayBones();
        StartDisplayHeldObject();
    }

    // Update is called once per frame
    void Update()
    {
        // Number button functionality
        if (Input.GetKeyDown(KeyCode.Alpha1) && scrolling_allowed)
            inventory_pos = -4;
        if (Input.GetKeyDown(KeyCode.Alpha2) && scrolling_allowed)
            inventory_pos = -3;
        if (Input.GetKeyDown(KeyCode.Alpha3) && scrolling_allowed)
            inventory_pos = -2;
        if (Input.GetKeyDown(KeyCode.Alpha4) && scrolling_allowed)
            inventory_pos = -1;
        if (Input.GetKeyDown(KeyCode.Alpha5) && scrolling_allowed)
            inventory_pos = 0;
        if (Input.GetKeyDown(KeyCode.Alpha6) && scrolling_allowed)
            inventory_pos = 1;
        if (Input.GetKeyDown(KeyCode.Alpha7) && scrolling_allowed)
            inventory_pos = 2;
        if (Input.GetKeyDown(KeyCode.Alpha8) && scrolling_allowed)
            inventory_pos = 3;
        if (Input.GetKeyDown(KeyCode.Alpha9) && scrolling_allowed)
            inventory_pos = 4;
        // Scrolling functionality
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0 && scrolling_allowed)
            inventory_pos--;
        else if (scroll < 0 && scrolling_allowed)
            inventory_pos++;
        if (inventory_pos > 4)
            inventory_pos = -4;
        if (inventory_pos < -4)
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
        {
            ChiselableBone.SetActive(true);
            ChiselableBone1.SetActive(false);
            ChiselableBone2.SetActive(false);
            ChiselableBone3.SetActive(false);
            CleanBone.SetActive(false);
            Spinosaurus.SetActive(false);
        }
            
        if (GetInventory(x) == "ChiselableBone1")
        {
            ChiselableBone.SetActive(false);
            ChiselableBone1.SetActive(true);
            ChiselableBone2.SetActive(false);
            ChiselableBone3.SetActive(false);
            CleanBone.SetActive(false);
            Spinosaurus.SetActive(false);
        }
        if (GetInventory(x) == "ChiselableBone2")
        {
            ChiselableBone.SetActive(false);
            ChiselableBone1.SetActive(false);
            ChiselableBone2.SetActive(true);
            ChiselableBone3.SetActive(false);
            CleanBone.SetActive(false);
            Spinosaurus.SetActive(false);
        }
        if (GetInventory(x) == "ChiselableBone3")
        {
            ChiselableBone.SetActive(false);
            ChiselableBone1.SetActive(false);
            ChiselableBone2.SetActive(false);
            ChiselableBone3.SetActive(true);
            CleanBone.SetActive(false);
            Spinosaurus.SetActive(false);
        }
        if (GetInventory(x) == "CleanBone")
        {
            ChiselableBone.SetActive(false);
            ChiselableBone1.SetActive(false);
            ChiselableBone2.SetActive(false);
            ChiselableBone3.SetActive(false);
            CleanBone.SetActive(true);
            Spinosaurus.SetActive(false);
        }
        if(GetInventory(x) == "Spinosaurus")
        {
            ChiselableBone.SetActive(true);
            ChiselableBone1.SetActive(false);
            ChiselableBone2.SetActive(false);
            ChiselableBone3.SetActive(false);
            CleanBone.SetActive(false);
            Spinosaurus.SetActive(true);

        }
        if (GetInventory(x) == null)
        {
            ChiselableBone.SetActive(false);
            ChiselableBone1.SetActive(false);
            ChiselableBone2.SetActive(false);
            ChiselableBone3.SetActive(false);
            CleanBone.SetActive(false);
            Spinosaurus.SetActive(false);
        }
            
    }
    public void StartDisplayBones()
    {
        Bone1 = GameObject.FindWithTag("Bone1Tag");
        Bone2 = GameObject.FindWithTag("Bone2Tag");
        Bone3 = GameObject.FindWithTag("Bone3Tag");
        Bone4 = GameObject.FindWithTag("Bone4Tag");
        Bone5 = GameObject.FindWithTag("Bone5Tag");

        Box1 = GameObject.FindWithTag("HighlightBox1");
        Box2 = GameObject.FindWithTag("HighlightBox2");
        Box3 = GameObject.FindWithTag("HighlightBox3");
        Box4 = GameObject.FindWithTag("HighlightBox4");
        Box5 = GameObject.FindWithTag("HighlightBox5");
        Box6 = GameObject.FindWithTag("HighlightBox6");
        Box7 = GameObject.FindWithTag("HighlightBox7");
        Box8 = GameObject.FindWithTag("HighlightBox8");
        Box9 = GameObject.FindWithTag("HighlightBox9");

        Bone1.SetActive(false);
        Bone2.SetActive(false);
        Bone3.SetActive(false);
        Bone4.SetActive(false);
        Bone5.SetActive(false);

        Box1.SetActive(false);
        Box2.SetActive(false);
        Box3.SetActive(false);
        Box4.SetActive(false);
        Box5.SetActive(false);
        Box6.SetActive(false);
        Box7.SetActive(false);
        Box8.SetActive(false);
        Box9.SetActive(false);
    }
    public void UpdateDisplayBones()
    {
        if (GetInventory(0) != null)
            Bone1.SetActive(true);
        if (GetInventory(1) != null)
            Bone2.SetActive(true);
        if (GetInventory(2) != null)
            Bone3.SetActive(true);
        if (GetInventory(3) != null)
            Bone4.SetActive(true);
        if (GetInventory(4) != null)
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
        Chisel = GameObject.FindWithTag("HeldChiselTag");
        ChiselableBone = GameObject.FindWithTag("HeldChiselableBoneTag");
        ChiselableBone1 = GameObject.FindWithTag("HeldCB1Tag");
        ChiselableBone2 = GameObject.FindWithTag("HeldCB2Tag");
        ChiselableBone3 = GameObject.FindWithTag("HeldCB3Tag");
        CleanBone = GameObject.FindWithTag("HeldCleanBoneTag");
        Brush = GameObject.FindWithTag("BrushTag");
        Spinosaurus = GameObject.FindWithTag("Spinosaurus");

    }
    public void UpdateDisplayHeldObject()
    {
        if (inventory_pos == -4)
        {
            Box1.SetActive(true);
            Box2.SetActive(false);
            Box3.SetActive(false);
            Box4.SetActive(false);
            Box5.SetActive(false);
            Box6.SetActive(false);
            Box7.SetActive(false);
            Box8.SetActive(false);
            Box9.SetActive(false);
            Pickaxe.SetActive(false);
            Chisel.SetActive(false);
            Brush.SetActive(false);
            ChiselableBone.SetActive(false);
            ChiselableBone1.SetActive(false);
            ChiselableBone2.SetActive(false);
            ChiselableBone3.SetActive(false);
            CleanBone.SetActive(false);
        }
        else if (inventory_pos == -3)
        {
            Box1.SetActive(false);
            Box2.SetActive(true);
            Box3.SetActive(false);
            Box4.SetActive(false);
            Box5.SetActive(false);
            Box6.SetActive(false);
            Box7.SetActive(false);
            Box8.SetActive(false);
            Box9.SetActive(false);
            Pickaxe.SetActive(true);
            Chisel.SetActive(false);
            Brush.SetActive(false);
            ChiselableBone.SetActive(false);
            ChiselableBone1.SetActive(false);
            ChiselableBone2.SetActive(false);
            ChiselableBone3.SetActive(false);
            CleanBone.SetActive(false);
        }
        else if (inventory_pos == -2)
        {
            Box1.SetActive(false);
            Box2.SetActive(false);
            Box3.SetActive(true);
            Box4.SetActive(false);
            Box5.SetActive(false);
            Box6.SetActive(false);
            Box7.SetActive(false);
            Box8.SetActive(false);
            Box9.SetActive(false);
            Pickaxe.SetActive(false);
            Chisel.SetActive(true);
            Brush.SetActive(false);
            ChiselableBone.SetActive(false);
            ChiselableBone1.SetActive(false);
            ChiselableBone2.SetActive(false);
            ChiselableBone3.SetActive(false);
            CleanBone.SetActive(false);
        }
        else if (inventory_pos == -1)
        {
            Box1.SetActive(false);
            Box2.SetActive(false);
            Box3.SetActive(false);
            Box4.SetActive(true);
            Box5.SetActive(false);
            Box6.SetActive(false);
            Box7.SetActive(false);
            Box8.SetActive(false);
            Box9.SetActive(false);
            Pickaxe.SetActive(false);
            Chisel.SetActive(false);
            Brush.SetActive(true);
            ChiselableBone.SetActive(false);
            ChiselableBone1.SetActive(false);
            ChiselableBone2.SetActive(false);
            ChiselableBone3.SetActive(false);
            CleanBone.SetActive(false);
        }
        else if (inventory_pos == 0)
        {
            Box1.SetActive(false);
            Box2.SetActive(false);
            Box3.SetActive(false);
            Box4.SetActive(false);
            Box5.SetActive(true);
            Box6.SetActive(false);
            Box7.SetActive(false);
            Box8.SetActive(false);
            Box9.SetActive(false);
            Pickaxe.SetActive(false);
            Chisel.SetActive(false);
            Brush.SetActive(false);
            CheckAndSetBone(0);
        }
        else if (inventory_pos == 1)
        {
            
            Box1.SetActive(false);
            Box2.SetActive(false);
            Box3.SetActive(false);
            Box4.SetActive(false);
            Box5.SetActive(false);
            Box6.SetActive(true);
            Box7.SetActive(false);
            Box8.SetActive(false);
            Box9.SetActive(false);
            Pickaxe.SetActive(false);
            Chisel.SetActive(false);
            Brush.SetActive(false);
            CheckAndSetBone(1);
        }
        else if (inventory_pos == 2)
        {
            Box1.SetActive(false);
            Box2.SetActive(false);
            Box3.SetActive(false);
            Box4.SetActive(false);
            Box5.SetActive(false);
            Box6.SetActive(false);
            Box7.SetActive(true);
            Box8.SetActive(false);
            Box9.SetActive(false);
            Pickaxe.SetActive(false);
            Chisel.SetActive(false);
            Brush.SetActive(false);
            CheckAndSetBone(2);
        }
        else if (inventory_pos == 3)
        {
            Box1.SetActive(false);
            Box2.SetActive(false);
            Box3.SetActive(false);
            Box4.SetActive(false);
            Box5.SetActive(false);
            Box6.SetActive(false);
            Box7.SetActive(false);
            Box8.SetActive(true);
            Box9.SetActive(false);
            Pickaxe.SetActive(false);
            Chisel.SetActive(false);
            Brush.SetActive(false);
            CheckAndSetBone(3);
        }
        else if (inventory_pos == 4)
        {
            Box1.SetActive(false);
            Box2.SetActive(false);
            Box3.SetActive(false);
            Box4.SetActive(false);
            Box5.SetActive(false);
            Box6.SetActive(false);
            Box7.SetActive(false);
            Box8.SetActive(false);
            Box9.SetActive(true);
            Pickaxe.SetActive(false);
            Chisel.SetActive(false);
            Brush.SetActive(false);
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
