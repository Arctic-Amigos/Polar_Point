using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "Tutorial1")
        {
            int tutorialNum = 1;

            TextDialogue textDialogue = FindObjectOfType<TextDialogue>();
            textDialogue.DisplayTutorial(tutorialNum);
        }
        if (gameObject.name == "Tutorial2")
        {
            int tutorialNum = 2;

            TextDialogue textDialogue = FindObjectOfType<TextDialogue>();
            textDialogue.DisplayTutorial(tutorialNum);
        }

        if (gameObject.name == "Tutorial3")
        {
            int tutorialNum = 3;

            TextDialogue textDialogue = FindObjectOfType<TextDialogue>();
            textDialogue.DisplayTutorial(tutorialNum);
        }

        if (gameObject.name == "Tutorial4")
        {
            int tutorialNum = 4;

            TextDialogue textDialogue = FindObjectOfType<TextDialogue>();
            textDialogue.DisplayTutorial(tutorialNum);
        }

    }
}
