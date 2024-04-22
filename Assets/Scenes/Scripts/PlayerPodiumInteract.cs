using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPodiumInteract : MonoBehaviour
{
    Podium currentPodium = null;

    //Only need these to allow for persistent bone showing
    public Podium carnotaurusPodium;
    public Podium triceratopsPodium;
    public Podium spinosaurusPodium;

    Transform[] carnotaurusParts;
    Transform[] triceratopsParts;
    Transform[] spinosaurusParts;

    Dictionary<int, string> bodyPartIntToString = new Dictionary<int, string>();

    Inventory inventory;
    TextDialogue textDialogue;

    public int numBonesOnCar = 0;
    public int numBonesOnTri = 0;
    public int numBonesOnSpi = 0;

    public bool inBase;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        textDialogue = FindObjectOfType<TextDialogue>();

        numBonesOnCar = PersistentGameManager.carBoneCount;
        numBonesOnTri = PersistentGameManager.triBoneCount;
        numBonesOnSpi = PersistentGameManager.spiBoneCount;

        if (SceneManager.GetActiveScene().name == "Scene1.5-HomeBase")
            inBase = true;
        else
            inBase = false;
        if (inBase)
        {
            carnotaurusParts = new Transform[]
        {
            carnotaurusPodium.transform.GetChild(0),
            carnotaurusPodium.transform.GetChild(1),
            carnotaurusPodium.transform.GetChild(2),
            carnotaurusPodium.transform.GetChild(3),
            carnotaurusPodium.transform.GetChild(4),
            carnotaurusPodium.transform.GetChild(5),
            carnotaurusPodium.transform.GetChild(6)
        };

            triceratopsParts = new Transform[]
            {
            triceratopsPodium.transform.GetChild(0),
            triceratopsPodium.transform.GetChild(1),
            triceratopsPodium.transform.GetChild(2),
            triceratopsPodium.transform.GetChild(3),
            triceratopsPodium.transform.GetChild(4),
            triceratopsPodium.transform.GetChild(5),
            triceratopsPodium.transform.GetChild(6)
            };

            spinosaurusParts = new Transform[]
            {
            spinosaurusPodium.transform.GetChild(0),
            spinosaurusPodium.transform.GetChild(1),
            spinosaurusPodium.transform.GetChild(2),
            spinosaurusPodium.transform.GetChild(3),
            spinosaurusPodium.transform.GetChild(4),
            spinosaurusPodium.transform.GetChild(5),
            spinosaurusPodium.transform.GetChild(6)
            };

            bodyPartIntToString.Add(1, "LeftLeg");
            bodyPartIntToString.Add(2, "RightLeg");
            bodyPartIntToString.Add(3, "Body");
            bodyPartIntToString.Add(4, "LeftArm");
            bodyPartIntToString.Add(5, "RightArm");
            bodyPartIntToString.Add(6, "Tail");
            bodyPartIntToString.Add(7, "Head");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inBase)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                numBonesOnCar++;
                numBonesOnSpi++;
                numBonesOnTri++;
            }
            for (int i = 0; i < carnotaurusParts.Length; i++)
            {
                carnotaurusParts[i].gameObject.SetActive(i < numBonesOnCar);
            }
            for (int i = 0; i < triceratopsParts.Length; i++)
            {
                triceratopsParts[i].gameObject.SetActive(i < numBonesOnTri);
            }
            for (int i = 0; i < spinosaurusParts.Length; i++)
            {
                if (numBonesOnSpi == 3)
                {
                    spinosaurusPodium.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                }
                spinosaurusParts[i].gameObject.SetActive(i < numBonesOnSpi);
            }


            if (currentPodium != null && currentPodium.tag == "CarnotaurusPodium")
            {
                if (Input.GetKeyDown(KeyCode.F) && inventory.inventory_pos >= 0 && inventory.GetInventory(inventory.inventory_pos) == "Carnotaurus")
                {
                    numBonesOnCar++;
                    inventory.SetInventory(inventory.inventory_pos, null);

                    textDialogue.DisplayPodiumFact("Carnotaurus", bodyPartIntToString[numBonesOnCar]);

                    Debug.Log("You placed a Carnotaurus bone");
                }
            }
            else if (currentPodium != null && currentPodium.tag == "TriceratopsPodium")
            {
                if (Input.GetKeyDown(KeyCode.F) && inventory.inventory_pos >= 0 && inventory.GetInventory(inventory.inventory_pos) == "Triceratops")
                {
                    numBonesOnTri++;
                    inventory.SetInventory(inventory.inventory_pos, null);

                    textDialogue.DisplayPodiumFact("Triceratops", bodyPartIntToString[numBonesOnTri]);

                    Debug.Log("You placed a Triceratops bone");
                }
            }
            else if (currentPodium != null && currentPodium.tag == "SpinosaurusPodium")
            {
                if (Input.GetKeyDown(KeyCode.F) && inventory.inventory_pos >= 0 && inventory.GetInventory(inventory.inventory_pos) == "Spinosaurus")
                {
                    numBonesOnSpi++;
                    inventory.SetInventory(inventory.inventory_pos, null);

                    textDialogue.DisplayPodiumFact("Spinosaurus", bodyPartIntToString[numBonesOnSpi]);

                    Debug.Log("You placed a Spinosaurus bone");
                }
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
