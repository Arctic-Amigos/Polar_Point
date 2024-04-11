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
    //private int msgNumber = 0;
    private int posToRead = 0;

    string type = "";
    string dinosaur = "";
    string bodyPart = "";
    Dictionary<string, List<string>> spinosaurusFacts = new Dictionary<string, List<string>>();
    Dictionary<string, List<string>> carnotaurusFacts = new Dictionary<string, List<string>>();
    Dictionary<string, List<string>> diplosaurusFacts = new Dictionary<string, List<string>>();
  

    //map inside map then an array for each thingk
    //key for dinosaur, (inside do a key for body part) as value of that do an array of all the strings

    //ADJUST DATA STRUCTURE TO ACCOUNT FOR BOTH OF THESE INSTANCES.
    //FOR BRUSHING:
        //Just have a string for "you found a " + type + "bone!"
        
    //FOR PODIUM:
        //This is where you have your fact


    //FINISHING BRUSHING
    //change the tag or just a name to me for just the type of dinosaur
    //3 dinosaurs
    //use that for determining name to say after brushing



    //3
    //Spinosaurus
    //Carnotaurus
    //Diplosaurus


    /* Populate Map */
    void Start()
    {
        //Spinosaurus
        spinosaurusFacts["LeftLeg"] = new List<string> { "Did you know... \n(Press Enter)", "The Spinosaurus had a denser and more compact bone structure compared to other species of its kind", "This allowed it to have better control over its buoyancy when submerged underwater!" };
        spinosaurusFacts["RightLeg"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        spinosaurusFacts["Body"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        spinosaurusFacts["LeftArm"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        spinosaurusFacts["RightArm"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        spinosaurusFacts["Head"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        spinosaurusFacts["Tail"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };


        //Carnotaurus
        carnotaurusFacts["LeftLeg"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };     
        carnotaurusFacts["RightLeg"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        carnotaurusFacts["Body"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        carnotaurusFacts["LeftArm"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        carnotaurusFacts["RightArm"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        carnotaurusFacts["Head"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        carnotaurusFacts["Tail"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };

        //Diplosaurus
        diplosaurusFacts["LeftLeg"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        diplosaurusFacts["RightLeg"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        diplosaurusFacts["Body"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        diplosaurusFacts["LeftArm"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        diplosaurusFacts["RightArm"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        diplosaurusFacts["Head"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };
        diplosaurusFacts["Tail"] = new List<string> { "Did you know... \n(Press Enter)", "Line 2" };

    }
    void Update()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.Return))
        {
            //If Found Msg or postClean msg, its just one msg so just turn off on enter
            if(type == "Found" || type == "PostClean")
            {
                isOn = false;
                TurnOff();
            }
            else if(type == "PodiumFact")
            {

                //For each dinosaur type go through the facts
                if(dinosaur == "Spinosaurus")
                {
                    if(posToRead > spinosaurusFacts[bodyPart].Count - 1)
                    {
                        //if no more blocks to read, turn off
                        isOn = false;
                        TurnOff();
                    }
                    else
                    {
                        //otherwise read next message
                        dialogueText.text = spinosaurusFacts[bodyPart][posToRead];
                        posToRead += 1;
                    }
                    

                }else if (dinosaur == "Carnotaurus")
                {
                    if (posToRead > carnotaurusFacts[bodyPart].Count - 1)
                    {
                        //if no more blocks to read, turn off
                        isOn = false;
                        TurnOff();
                    }
                    else
                    {
                        //otherwise read next message
                        dialogueText.text = carnotaurusFacts[bodyPart][posToRead];
                        posToRead += 1;
                    }
                }
                else if (dinosaur == "Dipolosaurus")
                {
                    if (posToRead > diplosaurusFacts[bodyPart].Count - 1)
                    {
                        //if no more blocks to read, turn off
                        isOn = false;
                        TurnOff();
                    }
                    else
                    {
                        //otherwise read next message
                        dialogueText.text = diplosaurusFacts[bodyPart][posToRead];
                        posToRead += 1;
                    }
                }
            }
        }
    }

    /* In Mine when collide with a mineable bone -> just says "It looks like you found a fossil!) */
    public void DisplayFoundMsg()
    {
        
        if (!isOn)
        {
            dialogueCanvas.SetActive(true);
            dialogueText.text = "Hold it right there! It looks like you found a fossil!";
            type = "Found";
            FindObjectOfType<AudioManager>().Play("WalkieTalkieBeep");
        }
    }

    /* After finishing cleaning the bone only -> just says "You found a ___ bone" */
    public void DisplayBoneType(string tag)
    {
        dialogueCanvas.SetActive(true);
        dialogueText.text = "You found a " + tag + " bone!";
        type = "PostClean";
        FindObjectOfType<AudioManager>().Play("Marty");
    }

    public void DisplayPodiumFact(string _dinosaur, string _bodyPart)
    {
        type = "PodiumFact";
        dinosaur = _dinosaur;
        bodyPart = _bodyPart; 
        posToRead = 0;
    }
    private void TurnOff()
    {
        dialogueText.text = "";
        dialogueCanvas.SetActive(false);
    }

}


