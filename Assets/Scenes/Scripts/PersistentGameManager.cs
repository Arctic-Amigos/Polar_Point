using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGameManager : MonoBehaviour
{
    public static int inventoryPos = -4;
    public static string inventory0 = null;
    public static string inventory1 = null;
    public static string inventory2 = null;
    public static string inventory3 = null;
    public static string inventory4 = null;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SaveInfo()
    {
        Inventory inventory = new Inventory();
        inventoryPos = inventory.inventory_pos;
        inventory0 = inventory.fossil_inventory[0];
        inventory1 = inventory.fossil_inventory[1];
        inventory2 = inventory.fossil_inventory[2];
        inventory3 = inventory.fossil_inventory[3];
        inventory4 = inventory.fossil_inventory[4];
    }
}
