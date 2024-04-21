using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cleaning : MonoBehaviour
{
    public Material[] cleaningStages;
    private Renderer objectRenderer;
    public int currentStage = 0;
    private bool isCleaning = false;
    public PlayerCleaning player;
    private List<string> availableTags = new List<string>();
    private const int NumberOfUniqueTags = 22;

    public bool finishedBrushing = false;

    private string currentDinosaurName;
    public Inventory inventory;
    public PlayerChiseling playerChiseling;

    ObjectChiselable objectChiselable;
    


    void Start()
    {
        objectChiselable = GetComponent<ObjectChiselable>();
        finishedBrushing = false;

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
        yield return new WaitForSeconds(0.6f); // Simulate cleaning effort
        if (currentStage < cleaningStages.Length - 1)
        {
            currentStage++;
            UpdateMaterial();
        }
        if (currentStage == cleaningStages.Length - 1 && availableTags.Count > 0)
        {
            
            int randomNumber =  UnityEngine.Random.Range(0, availableTags.Count); // Random.Range is inclusive for min, exclusive for max

            if (randomNumber <= 7)
            {
                currentDinosaurName = "Spinosaurus";
                
            }
            else if (randomNumber <= 14)
            {
                currentDinosaurName = "Carnotaurus";
            }
            else
            {
                currentDinosaurName = "Triceratops";
            }
            playerChiseling.doneCleaning = true;
            Debug.Log("prechiselcount " + objectChiselable.boneChiselCounts[playerChiseling.currentBoneOnWorkbench]);
            objectChiselable.boneChiselCounts[playerChiseling.currentBoneOnWorkbench] = 0;
            Debug.Log("postchiselcount " + objectChiselable.boneChiselCounts[playerChiseling.currentBoneOnWorkbench]);


            // Remove the selected tag from the list to ensure it's not used again
            availableTags.RemoveAt(randomNumber);
            

            AudioManager.instance.Stop("Brushing");

            //Trigger Text Dialogue
            TextDialogue textDialogue = FindObjectOfType<TextDialogue>();
            
            textDialogue.DisplayBoneType(currentDinosaurName);

            
            gameObject.tag = currentDinosaurName;
            currentStage = 0;
            finishedBrushing = true;
            /*
            if (player != null)
                player.SaveCleanState(inventory.inventory_pos, currentStage, currentDinosaurName);
            */
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
    public void SetCleaningStage(int stage)
    {
        currentStage = stage;
        UpdateMaterial();
    }

    public string getDinosaurName()
    {
        return currentDinosaurName;
    }
    

  
}
