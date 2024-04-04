using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaning : MonoBehaviour
{
    public Material[] cleaningStages;
    private Renderer objectRenderer;
    public int currentStage = 0;
    private bool isCleaning = false;
    public PlayerCleaning player;

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
        yield return new WaitForSeconds(0.3f); // Simulate cleaning effort
        if (currentStage < cleaningStages.Length - 1)
        {
            currentStage++;
            UpdateMaterial();
        }
        if (currentStage == cleaningStages.Length - 1)
        {
            this.tag = "Untagged";
            AudioManager.instance.Stop("Brushing");

            //Trigger Text Dialogue
            TextDialogue textDialogue = FindObjectOfType<TextDialogue>();
            textDialogue.DisplayBoneType(this.tag);
        }
        isCleaning = false;
    }

    void UpdateMaterial()
    {
        objectRenderer.material = cleaningStages[currentStage];
    }
    public void StopCleaning()
    {
        StopAllCoroutines(); // Stops the CleaningProgress coroutine
        isCleaning = false;
        // Add any additional cleanup or reset logic here if needed
    }
}
