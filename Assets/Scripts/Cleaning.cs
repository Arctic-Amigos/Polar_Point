using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaning : MonoBehaviour
{
    public Material[] cleaningStages;
    private Renderer objectRenderer;
    public int currentStage = 0;
    private bool isCleaning = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        UpdateMaterial();
    }

    public void StartCleaning()
    {
        if (!isCleaning && currentStage < cleaningStages.Length - 1)
        {
            StartCoroutine(CleaningProgress());
        }
    }

    IEnumerator CleaningProgress()
    {
        isCleaning = true;
        yield return new WaitForSeconds(0.5f); // Simulate cleaning effort
        if (currentStage < cleaningStages.Length - 1)
        {
            currentStage++;
            UpdateMaterial();
        }
        isCleaning = false;
    }

    void UpdateMaterial()
    {
        objectRenderer.material = cleaningStages[currentStage];
    }
}
