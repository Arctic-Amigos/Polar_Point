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
        spinosaurusFacts["RightLeg"] = new List<string> { "Did you know... \n(Press Enter)", "The Spinosaurus’ snout was similar to one of a crocodile and straight teeth!", "Its features were all adaptations for eating fish!" };
        spinosaurusFacts["Body"] = new List<string> { "Did you know... \n(Press Enter)", "The Spinosaurus is also referred to as the “spined reptile”.", "It got its name through its “sail back” feature, created by tall vertebral spines.", "It’s believed that this feature was utilized for social displays, similar to a peacock!" };
        spinosaurusFacts["LeftArm"] = new List<string> { "Did you know... \n(Press Enter)", "Although the Spinosaurus’ diet consisted mainly of fish, they also ate other dinosaurs." };
        spinosaurusFacts["RightArm"] = new List<string> { "Did you know... \n(Press Enter)", "The Spinosauruses lived in the Cretaceous Period?", "That was over 100 million years ago!" };
        spinosaurusFacts["Head"] = new List<string> { "Did you know... \n(Press Enter)", "The Spinosauruses skull is roughly 6 feet long?", "This must be a baby!" };
        spinosaurusFacts["Tail"] = new List<string> { "Did you know... \n(Press Enter)", "That Spinosauruses are both longer AND heavier than the Tyrannosaurus!?!?", "This makes it the largest carnivorous dinosaur of them all!" };


        //Carnotaurus
        carnotaurusFacts["LeftLeg"] = new List<string> { "Did you know... \n(Press Enter)", "That “Carnotaurus” means “meat-eating bull”?", "Despite this name, many refer to it as resembling a Trex with horns." };     
        carnotaurusFacts["RightLeg"] = new List<string> { "Did you know... \n(Press Enter)", "This dinosaur had lots of scales!" };
        carnotaurusFacts["Body"] = new List<string> { "Did you know... \n(Press Enter)", "That the Carnotaurus could grow up to 25 feet long?", "This makes it as big as a small basketball court!" };
        carnotaurusFacts["LeftArm"] = new List<string> { "Did you know... \n(Press Enter)", "The Carnotaurus has the smallest forelimbs of all theropods!", "Their arms are even smaller than those of the T-rex!" };
        carnotaurusFacts["RightArm"] = new List<string> { "Did you know... \n(Press Enter)", "That this dinosaur lived in the Campanian Age? That’s nearly 66 million years ago!" };
        carnotaurusFacts["Head"] = new List<string> { "Did you know... \n(Press Enter)", "It is thought that Carnotaurus’ horns could be used for fighting rivals, attacking prey.",  "Also helping them find a mate, and identify other Carnotaurus." };
        carnotaurusFacts["Tail"] = new List<string> { "Did you know... \n(Press Enter)", "That no one has ever found the tail or lower legs of a Carnotaurus?", "That means what scientists think it looks like is purely theoretical." };

        //Diplosaurus
        diplosaurusFacts["LeftLeg"] = new List<string> { "Did you know... \n(Press Enter)", "That “Triceratops”’ name comes from the Greek words meaning “three-horned face”?", "This is because of its three horns atop of its head." };
        diplosaurusFacts["RightLeg"] = new List<string> { "Did you know... \n(Press Enter)", "The Triceratops moved in herds in order to avoid getting eaten by predators!" };
        diplosaurusFacts["Body"] = new List<string> { "Did you know... \n(Press Enter)", "That the Triceratops was a herbivore?", "It had a parrot-shaped beat in order to eat plants most efficiently!"};
        diplosaurusFacts["LeftArm"] = new List<string> { "Did you know... \n(Press Enter)", "The Triceratops is currently one of the most-found fossils!"};
        diplosaurusFacts["RightArm"] = new List<string> { "Did you know... \n(Press Enter)", "That the Triceratops’ frill was used to protect its neck?", "Additionally, it was used to attract a mate."};
        diplosaurusFacts["Head"] = new List<string> { "Did you know... \n(Press Enter)", "The Triceratops’ skull is one of the largest of any land animal.", "Their skulls were about 10 feet long!"};
        diplosaurusFacts["Tail"] = new List<string> { "Did you know... \n(Press Enter)", "Did you know that the Triceratops’ main predator was the T-Rex?", " The Triceratops utilized its horn as a defense against the T-Rex!"};

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


