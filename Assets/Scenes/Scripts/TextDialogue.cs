using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows;
using System.ComponentModel;

public class TextDialogue : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public TextMeshProUGUI dialogueText;

    private bool isOn = false;
    private int msgNumber = 0;
    private int posToRead = 0;

    //Text Vectors
    List<List<string>> msgVec = new List<List<string>> { 
        new List<string> { "\nWait... What is that??", "\nIt looks like you found a fossil!" },
        new List<string> {"You found a <insert dynosaur name> bone" }
    };


    void Start()
    {
      
    }
    void Update()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.Return))
        {
            if(posToRead > msgVec[msgNumber].Count - 1)
            {
                //If no more blocks to read, turnoff
                isOn = false;
                TurnOff();
            }
            else 
            {
                //Otherwise, read next msg
                dialogueText.text = msgVec[msgNumber][posToRead];
                posToRead += 1;
            }
        }
    }
    public void DisplayFoundMsg()
    {
        if (!isOn)
        {
            dialogueCanvas.SetActive(true);
            msgNumber = 0;
            posToRead = 0;

            dialogueText.text = msgVec[msgNumber][posToRead];
            posToRead += 1;
        }
    }
    public void DisplayBoneType(string type)
    {
        
    }
    //Make other function for other cases where we turn on the dialog
    private void TurnOff()
    {
        dialogueText.text = "";
        dialogueCanvas.SetActive(false);
    }

}
