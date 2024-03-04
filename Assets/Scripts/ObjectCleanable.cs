using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCleanable : MonoBehaviour
{
    // Tracks the cleaning progress
    private int cleaningProgress = 0;

    // Maximum layers of dirt
    public int maxCleaningStages = 3;

    // Optional: Callback for when cleaning is complete
    public delegate void OnCleanComplete();
    public event OnCleanComplete onCleanComplete;

    public void IncrementCleaningProgress()
    {
        cleaningProgress++;
        UpdateCleaningState();

        if (cleaningProgress >= maxCleaningStages)
        {
            // Cleaning complete
            Debug.Log("Object fully cleaned!");
            if (onCleanComplete != null)
                onCleanComplete.Invoke();
        }
    }

    void UpdateCleaningState()
    {
        // Here you can implement visual feedback for cleaning progress
        // For example, activating/deactivating layers, changing material, etc.
        // This example assumes you have child objects representing dirt layers
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i >= cleaningProgress);
        }
    }
}
