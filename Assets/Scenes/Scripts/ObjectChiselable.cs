using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChiselable : MonoBehaviour
{
    private int chiselCount = 0;
    //allows us to just have one Chiselable object and have it keep track of individual bones values, keys range from 1-5
    public Dictionary<int, int> boneChiselCounts = new Dictionary<int, int>();

    public PlayerChiseling player;

    // Start is called before the first frame update
    void Start()
    {
        //UpdateDirtLayers();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PrintChiselStats();
        }
    }
    public void PrintChiselStats()
    {
        foreach(var pair in boneChiselCounts)
        {
            Debug.Log("bone " + pair.Key + " has been chiseled " + pair.Value);
        }
    }
    
    public void IncrementChiselCount(int boneTag)
    {
        if(boneChiselCounts.ContainsKey(boneTag))
        {
            boneChiselCounts[boneTag]++;
        }else
        {
            boneChiselCounts[boneTag] = 1;
        }
        UpdateDirtLayers(boneTag);
    }
   

    private void UpdateDirtLayers(int boneTag)
    {
        int specificBoneIdentifier = boneTag;
        if(boneChiselCounts.ContainsKey(specificBoneIdentifier)) 
        {
            chiselCount = boneChiselCounts[specificBoneIdentifier];
        }else
        {
            chiselCount = 0;
        }

        for(int i = 0; i < transform.childCount; i++)
        {
            if(i < chiselCount)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }else
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        if(chiselCount >= 3)
        {
            this.tag = "Brushable";
            //same functionality as written below but now changed it to brushable so might change later
        }
    }
    

}
