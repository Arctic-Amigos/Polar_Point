using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChiselable : MonoBehaviour
{
    private int chiselCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        UpdateDirtLayers();
    }

    public void IncrementChiselCount()
    {
        chiselCount++;
        UpdateDirtLayers();
    }

    private void UpdateDirtLayers()
    {
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
            gameObject.tag = "Untagged";
        }
    }

}
