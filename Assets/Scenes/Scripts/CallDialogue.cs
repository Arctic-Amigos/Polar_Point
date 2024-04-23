using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDialogue : MonoBehaviour
{
    public bool[] tutorialsTriggered = new bool[4];
    public GameObject[] tutorials = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        tutorialsTriggered = PersistentGameManager.tuts;
        tutorials[0] = GameObject.Find("Tutorial1");
        tutorials[1] = GameObject.Find("Tutorial2");
        tutorials[2] = GameObject.Find("Tutorial3");
        tutorials[3] = GameObject.Find("Tutorial4");
        
        if(tutorials[2] != null)
            tutorials[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialsTriggered[0] && tutorials[0] != null)
        {
            tutorials[0].SetActive(false);
        }
        if (tutorialsTriggered[1] && tutorials[1] != null)
        {
            tutorials[1].SetActive(false);
        }
        if (tutorialsTriggered[2] && tutorials[2] != null)
        {
            tutorials[2].SetActive(false);
        }
        if (tutorialsTriggered[3] && tutorials[3] != null)
        {
            tutorials[3].SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Tutorial1")
        {
            int tutorialNum = 1;

            TextDialogue textDialogue = FindObjectOfType<TextDialogue>();
            textDialogue.DisplayTutorial(tutorialNum);
            tutorialsTriggered[0] = true;
            tutorials[0].SetActive(false);

            FindObjectOfType<AudioManager>().Play("Marty");
        }
        if (other.name == "Tutorial2")
        {
            int tutorialNum = 2;

            TextDialogue textDialogue = FindObjectOfType<TextDialogue>();
            textDialogue.DisplayTutorial(tutorialNum);
            tutorialsTriggered[1] = true;
            tutorials[1].SetActive(false);
            tutorials[2].SetActive(true);

            FindObjectOfType<AudioManager>().Play("Marty");
        }

        if (other.name == "Tutorial3")
        {
            int tutorialNum = 3;

            TextDialogue textDialogue = FindObjectOfType<TextDialogue>();
            textDialogue.DisplayTutorial(tutorialNum);
            tutorialsTriggered[2] = true;
            tutorials[2].SetActive(false);

            FindObjectOfType<AudioManager>().Play("Marty");
        }

        if (other.name == "Tutorial4")
        {
            int tutorialNum = 4;

            TextDialogue textDialogue = FindObjectOfType<TextDialogue>();
            textDialogue.DisplayTutorial(tutorialNum);
            tutorialsTriggered[3] = true;
            tutorials[3].SetActive(false);

            FindObjectOfType<AudioManager>().Play("Marty");
        }

    }
}
