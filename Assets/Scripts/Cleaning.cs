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
    private List<string> availableTags = new List<string>();
    private const int NumberOfUniqueTags = 15;

    void Start()
    {
        // Initialize the list with unique tags
        for (int i = 1; i <= NumberOfUniqueTags; i++)
        {
            availableTags.Add("CleanBone" + i);
        }
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
        if (currentStage == cleaningStages.Length - 1 && availableTags.Count > 0)
        {
            int randomNumber = Random.Range(0, availableTags.Count); // Random.Range is inclusive for min, exclusive for max
            this.tag = "CleanBone" + availableTags[randomNumber];
           
            // Remove the selected tag from the list to ensure it's not used again
            availableTags.RemoveAt(randomNumber);

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
