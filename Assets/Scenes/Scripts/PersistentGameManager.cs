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
    GameObject player;
    Inventory inventory;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveInfo()
    {
        var i = new Inventory();
        inventoryPos = i.inventory_pos;
        inventory0 = i.fossil_inventory[0];
        inventory1 = i.fossil_inventory[1];
        inventory2 = i.fossil_inventory[2];
        inventory3 = i.fossil_inventory[3];
        inventory4 = i.fossil_inventory[4];
    }
}
