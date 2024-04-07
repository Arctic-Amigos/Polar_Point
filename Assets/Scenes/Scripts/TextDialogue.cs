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
        new List<string> {"You found a Untagged bone!", "\nThis is some info about the untagged bone"},
        new List<string> {"You found a dino2 bone!", "\nThis is some info about the dino2 bone"},
        new List<string> {"You found a dino3 bone!", "\nThis is some info about the dino3 bone"}
    };

    string dino1 = "Untagged";
    string dino2 = "dino2";
    string dino3 = "dino3";


    //3
    //T-rex
    //Dinosaur 2
    //Dinosaur 3

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
        if(!isOn)
        {
            dialogueCanvas.SetActive(true);
            if(type == dino1)
            {
                msgNumber = 1;
            }
            else if(type == dino2)
            {
                msgNumber = 2;
            }
            else if (type == dino3)
            {
                msgNumber = 3;
            }
            posToRead = 0;
            dialogueText.text = msgVec[msgNumber][posToRead];
            posToRead += 1;
        }
    }
    //Make other function for other cases where we turn on the dialog
    private void TurnOff()
    {
        dialogueText.text = "";
        dialogueCanvas.SetActive(false);
    }

}
