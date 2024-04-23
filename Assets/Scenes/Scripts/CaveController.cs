using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveController : MonoBehaviour
{
    public bool[] bonesCollected = new bool[21];
    public bool inCave;
    public GameObject[] boneObjects = new GameObject[21];

    // Start is called before the first frame update
    void Start()
    {
        bonesCollected = PersistentGameManager.bones;
        if (SceneManager.GetActiveScene().name == "Scene3-CaveInterior")
            inCave = true;
        else
            inCave = false;
        if (inCave)
        {
            boneObjects[0] = GameObject.Find("ChiselableBone");
            for (int i = 1; i < 21; i++)
            {
                boneObjects[i] = GameObject.Find("ChiselableBone (" + i + ")");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inCave)
        {
            for (int i = 0; i < 21; i++)
            {
                if (!boneObjects[i].transform.GetChild(0).gameObject.activeSelf)
                {
                    bonesCollected[i] = true;
                }
                if (bonesCollected[i])
                {
                    boneObjects[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
}
