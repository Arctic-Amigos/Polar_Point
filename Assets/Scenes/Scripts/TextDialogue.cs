using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows;
using System.ComponentModel;
using UnityEngine.ProBuilder.MeshOperations;
using System;

public class TextDialogue : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public TextMeshProUGUI dialogueText;

    private bool isOn = false;
    //private int msgNumber = 0;
    private int posToRead = 0;
    bool hadFirstCaveCollision = false;

    public string type = "";
    public int tutorialNumber = 0;
    private string dinosaur = "";
    private string bodyPart = "";
    Dictionary<string, List<string>> spinosaurusFacts = new Dictionary<string, List<string>>();
    Dictionary<string, List<string>> carnotaurusFacts = new Dictionary<string, List<string>>();
    Dictionary<string, List<string>> diplosaurusFacts = new Dictionary<string, List<string>>();

    
    List<string> tutorial1 = new List<string> { "Hi im Marty im going to be helping you through your journey. Press enter to continue", "We’re so excited that you’re here as our archeologist!", "We’ve found an area full of dinosaur bones for you to harvest!!", "Press the WASD buttons to walk. Follow the red poles to get to the cave." };
    List<string> tutorial2 = new List<string> { "Wow! There’s a dinosaur bone right there!", "Scroll on the scroll wheel to select different items in your inventory.", "Select the pickaxe", "Press left click on the mouse to mine the bone." };
    List<string> tutorial3 = new List<string> { "Awesome! Let’s bring the bone to the base and clean it up!", "Follow the red poles outside to walk to the archeology Unit." };
    List<string> tutorial4 = new List<string> { "Welcome to the archeology unit!", "Walk over to the workbench.", "Scroll to hold the bone, then press F to place it on the workbench.", "Press E to access workbench mode.", "Hold the left mouse button on the rock to chisel it off", "Brush the bone back and forth to get rid of all the dirt." };
    bool hasShownTutorial1 = false;
    bool hasShownTutorial2 = false;
    bool hasShownTutorial3 = false;
    bool hasShownTutorial4 = false;



    //map inside map then an array for each thing
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




    /* Populate Map */
    void Start()
    {
        //Spinosaurus
        spinosaurusFacts["LeftLeg"] = new List<string> { "Did you know... ", "The Spinosaurus had a denser and more compact bone structure compared to other species of its kind", "This allowed it to have better control over its buoyancy when submerged underwater!" };
        spinosaurusFacts["RightLeg"] = new List<string> { "Did you know...", "The Spinosaurus’ snout was similar to one of a crocodile and straight teeth!", "Its features were all adaptations for eating fish!" };
        spinosaurusFacts["Body"] = new List<string> { "Did you know...", "The Spinosaurus is also referred to as the “spined reptile”.", "It got its name through its “sail back” feature, created by tall vertebral spines.", "It’s believed that this feature was utilized for social displays, similar to a peacock!" };
        spinosaurusFacts["LeftArm"] = new List<string> { "Did you know...", "Although the Spinosaurus’ diet consisted mainly of fish, they also ate other dinosaurs." };
        spinosaurusFacts["RightArm"] = new List<string> { "Did you know...", "The Spinosauruses lived in the Cretaceous Period?", "That was over 100 million years ago!" };
        spinosaurusFacts["Head"] = new List<string> { "Did you know... ", "The Spinosauruses skull is roughly 6 feet long?", "This must be a baby!" };
        spinosaurusFacts["Tail"] = new List<string> { "Did you know...", "That Spinosauruses are both longer AND heavier than the Tyrannosaurus!?!?", "This makes it the largest carnivorous dinosaur of them all!" };

        //Carnotaurus
        carnotaurusFacts["LeftLeg"] = new List<string> { "Did you know...", "That “Carnotaurus” means “meat-eating bull”?", "Despite this name, many refer to it as resembling a Trex with horns." };
        carnotaurusFacts["RightLeg"] = new List<string> { "Did you know...", "This dinosaur had lots of scales!" };
        carnotaurusFacts["Body"] = new List<string> { "Did you know...", "That the Carnotaurus could grow up to 25 feet long?", "This makes it as big as a small basketball court!" };
        carnotaurusFacts["LeftArm"] = new List<string> { "Did you know...", "The Carnotaurus has the smallest forelimbs of all theropods!", "Their arms are even smaller than those of the T-rex!" };
        carnotaurusFacts["RightArm"] = new List<string> { "Did you know...", "That this dinosaur lived in the Campanian Age? That’s nearly 66 million years ago!" };
        carnotaurusFacts["Head"] = new List<string> { "Did you know...", "It is thought that Carnotaurus’ horns could be used for fighting rivals, attacking prey.", "Also helping them find a mate, and identify other Carnotaurus." };
        carnotaurusFacts["Tail"] = new List<string> { "Did you know...", "That no one has ever found the tail or lower legs of a Carnotaurus?", "That means what scientists think it looks like is purely theoretical." };

        //Diplosaurus
        diplosaurusFacts["LeftLeg"] = new List<string> { "Did you know...", "That “Triceratops”’ name comes from the Greek words meaning “three-horned face”?", "This is because of its three horns atop of its head." };
        diplosaurusFacts["RightLeg"] = new List<string> { "Did you know...", "The Triceratops moved in herds in order to avoid getting eaten by predators!" };
        diplosaurusFacts["Body"] = new List<string> { "Did you know...", "That the Triceratops was a herbivore?", "It had a parrot-shaped beat in order to eat plants most efficiently!" };
        diplosaurusFacts["LeftArm"] = new List<string> { "Did you know...", "The Triceratops is currently one of the most-found fossils!" };
        diplosaurusFacts["RightArm"] = new List<string> { "Did you know...", "That the Triceratops’ frill was used to protect its neck?", "Additionally, it was used to attract a mate." };
        diplosaurusFacts["Head"] = new List<string> { "Did you know...", "The Triceratops’ skull is one of the largest of any land animal.", "Their skulls were about 10 feet long!" };
        diplosaurusFacts["Tail"] = new List<string> { "Did you know...", "That the Triceratops’ main predator was the T-Rex?", " The Triceratops utilized its horn as a defense against the T-Rex!" };

        //tutorial1 = new List<string> { "Welcome to Antarctica!!", "We’re so excited that you’re here as our archeologist!", "We’ve found an area full of dinosaur bones for you to harvest!!", "Press the WASD buttons to walk. Follow the red poles to get to the cave." };
        //tutorial3 = new List<string> { "Awesome! Let’s bring the bone to the base and clean it up!", "Follow the red poles outside to walk to the archeology Unit." };
        //tutorial4 = new List<string> { "Welcome to the archeology unit!", "Walk over to the workbench.", "Scroll to hold the bone, then press e to place it on the workbench.", "Press f to access workbench mode.", "Hold the left mouse button on the rock to chisel it off", "Brush the bone back and forth to get rid of all the dirt." };
        //tutorial5 = new List<string> { "Line 1", "Line 2" };
    }
    void Update()
    {

        if(UnityEngine.Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(type); //Changed type in function call but doesnt get updated here, didnt get updated..... bruh
            //type = "Tutorial";
            //tutorialNumber = 2;
            //Once we get these to actually update we are chilling


            //If Found Msg or postClean msg, its just one msg so just turn off on enter
            if(type == "Found" || type == "PostClean")
            {
                Debug.Log("entered found");
                isOn = false;
                TurnOff();
            }
            else if(type == "PodiumFact")
            {
                Debug.Log("podium");
                //For each dinosaur type go through the facts
                if (dinosaur == "Spinosaurus")
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
            else if (type.Equals("Tutorial"))
            {
                Debug.Log("Entered Tutorial");
                //Tutorial Stuff 
                if(tutorialNumber == 1 && !hasShownTutorial1)
                {
                    if(posToRead > tutorial1.Count - 1){
                        isOn = false;
                        hasShownTutorial1 = true;
                        TurnOff();
                    }
                    else{
                        dialogueText.text = tutorial1[posToRead];
                        posToRead += 1;
                    }
                }else if(tutorialNumber == 2 && !hasShownTutorial2)
                {
                    Debug.Log("In tutorial 2 update");
                    if (posToRead > tutorial2.Count - 1)
                    {
                        isOn = false;
                        hasShownTutorial2 = true;
                        TurnOff();
                    }
                    else
                    {
                        dialogueText.text = tutorial2[posToRead];
                        posToRead += 1;
                    }
                }
                else if(tutorialNumber == 3 && !hasShownTutorial3){
                    if (posToRead > tutorial3.Count - 1)
                    {
                        hasShownTutorial3 = true;
                        isOn = false;
                        TurnOff();
                    }
                    else
                    {
                        dialogueText.text = tutorial3[posToRead];
                        posToRead += 1;
                    }
                }
                else if(tutorialNumber == 4 && !hasShownTutorial4)
                {
                    if (posToRead > tutorial4.Count - 1)
                    {
                        isOn = false;
                        hasShownTutorial4 = true;
                        TurnOff();
                    }
                    else
                    {
                        dialogueText.text = tutorial4[posToRead];
                        posToRead += 1;
                    }
                }
            }
            Debug.Log("exited");
        }
    }

    /* In Mine when collide with a mineable bone -> just says "It looks like you found a fossil!) */
    public void DisplayFoundMsg()
    {
        //So this wont do anything on the first cave collision
        if (!isOn && hadFirstCaveCollision)
        {
            dialogueCanvas.SetActive(true);
            dialogueText.text = "Hold it right there! It looks like you found a fossil!";
            type = "Found";
            FindObjectOfType<AudioManager>().Play("WalkieTalkieBeep");
            FindObjectOfType<AudioManager>().Play("Marty");
        }
    }

    /* After finishing cleaning the bone only -> just says "You found a ___ bone" */
    public void DisplayBoneType(string tag)
    {
        dialogueCanvas.SetActive(true);
        dialogueText.text = "You found a " + tag + " bone! Lets put that bone on the pedestal";
        type = "PostClean";
        FindObjectOfType<AudioManager>().Play("Marty");
    }

    public void DisplayPodiumFact(string _dinosaur, string _bodyPart)
    {
        dialogueCanvas.SetActive(true);
        type = "PodiumFact";
        dinosaur = _dinosaur;
        bodyPart = _bodyPart; 
        posToRead = 0;

        
        if (dinosaur == "Spinosaurus")
        {
            dialogueText.text = spinosaurusFacts[bodyPart][posToRead];
           
        }
        else if (dinosaur == "Carnotaurus")
        {
            dialogueText.text = carnotaurusFacts[bodyPart][posToRead];
            
        }
        else if (dinosaur == "Dipolosaurus")
        {
            dialogueText.text = diplosaurusFacts[bodyPart][posToRead];
            
        }
        posToRead += 1;
    }
    public void DisplayTutorial(int _tutorialNumber)
    {
        dialogueCanvas.SetActive(true);
        Debug.Log("Entered Tutorial FUnction");
        posToRead = 0;
        tutorialNumber = _tutorialNumber;
    
        if (tutorialNumber == 1 && !hasShownTutorial1) {
            dialogueText.text = tutorial1[posToRead];
        }
        else if (tutorialNumber == 2 && !hasShownTutorial2)
        {
            hadFirstCaveCollision = true;
            dialogueText.text = tutorial2[posToRead];
        }
        else if (tutorialNumber == 3 && !hasShownTutorial3)
        {
            dialogueText.text = tutorial3[posToRead];
            
        }
        else if (tutorialNumber == 4 && !hasShownTutorial4)
        {
            dialogueText.text = tutorial4[posToRead];
            
            
        }
        else
        {
            dialogueCanvas.SetActive(false);
        }
        posToRead += 1;
        type = "Tutorial";
        Debug.Log(type);
    }
    private void TurnOff()
    {
        dialogueText.text = "";
        dialogueCanvas.SetActive(false);
    }

}


