using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGameManager : MonoBehaviour
{
    public static string LevelName = "";
    public static int LevelEntryPoint = -1;
    public static float LevelRotationX;
    public static float LevelRotationY;
    
    public static int inventoryPos = -4;
    public static string inventory0 = null;
    public static string inventory1 = null;
    public static string inventory2 = null;
    public static string inventory3 = null;
    public static string inventory4 = null;
    public static int carBoneCount = 0;
    public static int triBoneCount = 0;
    public static int spiBoneCount = 0;
    public static bool[] bones = new bool[21];
    public static bool[] tuts = new bool[4];



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
        Inventory inv = GameObject.FindObjectOfType<Inventory>();
        PlayerPodiumInteract pod = GameObject.FindObjectOfType<PlayerPodiumInteract>();
        CaveController cave = GameObject.FindObjectOfType<CaveController>();
        CallDialogue call = GameObject.FindObjectOfType<CallDialogue>();
        inventoryPos = inv.inventory_pos;
        inventory0 = inv.fossil_inventory[0];
        inventory1 = inv.fossil_inventory[1];
        inventory2 = inv.fossil_inventory[2];
        inventory3 = inv.fossil_inventory[3];
        inventory4 = inv.fossil_inventory[4];
        carBoneCount = pod.numBonesOnCar;
        triBoneCount = pod.numBonesOnTri;
        spiBoneCount = pod.numBonesOnSpi;
        bones = cave.bonesCollected;
        tuts = call.tutorialsTriggered;
    }

    public static void SetTargetLevel(string level, int entrypoint, float rotationX, float rotationY)
    {
        LevelName = level;
        LevelEntryPoint = entrypoint;
        LevelRotationX = rotationX;
        LevelRotationY = rotationY;
    }
}
