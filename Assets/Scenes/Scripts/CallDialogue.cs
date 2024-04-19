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
        if (gameObject.name == "Tutorial2")
        {
            int tutorialNum = 2;
            //DisplayTutorial(tutorialNum);

            TextDialogue textDialogue = FindObjectOfType<TextDialogue>();
            textDialogue.DisplayTutorial(tutorialNum);
        }


    }
}
