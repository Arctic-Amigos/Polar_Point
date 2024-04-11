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
    private List<List<string>> msgVec = new List<List<string>> { 
        new List<string> { "\nWait... What is that??", "\nIt looks like you found a fossil!" },
        new List<string> {"You found a Untagged bone!", "\nThis is some info about the untagged bone"},
        new List<string> {"You found a dino2 bone!", "\nThis is some info about the dino2 bone"},
        new List<string> {"You found a dino3 bone!", "\nThis is some info about the dino3 bone"}
    };

    //ADJUST DATA STRUCTURE TO ACCOUNT FOR BOTH OF THESE INSTANCES.
    //FOR BRUSHING:
        //Just have a string for "you found a " + type + "bone!"
        
    //FOR PODIUM:
        //This is where you have your fact


    //FINISHING BRUSHING
    //change the tag or just a name to me for just the type of dinosaur
    //3 dinosaurs
    //use that for determining name to say after brushing





    //PLACING ON PODIUM
    //passes in dinosaur name 
    //and dinosaur body part
    private string dino1 = "Tric";
    private string dino2 = "dino2";
    private string dino3 = "dino3";


    //3
    //Triceratops
    //Carnotaurus
    //Diplosaurus


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
            //Debug.Log("entered Found Message");
            dialogueCanvas.SetActive(true);
            msgNumber = 0;
            posToRead = 0;

            dialogueText.text = msgVec[msgNumber][posToRead];
            posToRead += 1;
            FindObjectOfType<AudioManager>().Play("WalkieTalkieBeep");
        }
    }
    public void DisplayBoneType(string type)
    {

        //Debug.Log("The tag: " + type);
        //if(!isOn)
        //{
        //    dialogueCanvas.SetActive(true);
        //    if(type == dino1)
        //    {
        //        msgNumber = 1;
        //    }
        //    else if(type == dino2)
        //    {
        //        msgNumber = 2;
        //    }
        //    else if (type == dino3)
        //    {
        //        msgNumber = 3;
        //    }
        //    posToRead = 0;
        //    dialogueText.text = msgVec[msgNumber][posToRead];
        //    posToRead += 1;
        //}


        dialogueCanvas.SetActive(true);
        dialogueText.text = "You found a " + type + " bone!";
        FindObjectOfType<AudioManager>().Play("Marty");

    }
    //Make other function for other cases where we turn on the dialog
    private void TurnOff()
    {
        dialogueText.text = "";
        dialogueCanvas.SetActive(false);
    }

}
