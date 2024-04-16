using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPodiumInteract : MonoBehaviour
{
    Podium currentPodium = null;
    GameObject Pedestal;
    Transform LeftLeg;
    Transform RightLeg;
    Transform Body;
    Transform LeftArm;
    Transform RightArm;
    Transform Tail;
    Transform Head;
    // Start is called before the first frame update
    void Start()
    {
        /*LeftLeg = Carnotaurus.transform.GetChild(0).GetChild(0);
        RightLeg = Carnotaurus.transform.GetChild(0).GetChild(1);
        Body = Carnotaurus.transform.GetChild(0).GetChild(2);
        LeftArm = Carnotaurus.transform.GetChild(0).GetChild(3);
        RightArm = Carnotaurus.transform.GetChild(0).GetChild(4);
        Tail = Carnotaurus.transform.GetChild(0).GetChild(5);
        Head = Carnotaurus.transform.GetChild(0).GetChild(6); */
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPodium != null)
        {
            Debug.Log(currentPodium.tag);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Podium obj;
        if (other.TryGetComponent(out obj))
        {
            Debug.Log("called");
            currentPodium = obj;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentPodium = null;
    }
}
