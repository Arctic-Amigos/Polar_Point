using System.Collections;
using UnityEngine;

public class PlayerCleaning : MonoBehaviour
{
    public Material[] cleaningStages; // Assign the layers (dirt) in the inspector
    private Renderer objectRenderer;
    public int currentStage = 0; // Current layer being cleaned
    private bool isCleaning = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        UpdateMaterial();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !isCleaning)
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
