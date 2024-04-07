using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTesting : MonoBehaviour
{
    GameObject wbChisel;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        wbChisel = GameObject.FindWithTag("WBChisel");
        startPosition = wbChisel.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = wbChisel.transform.position;
        currentPosition.x += Input.GetAxis("Mouse X") * 0.01f;
        currentPosition.z += Input.GetAxis("Mouse Y") * 0.01f;
        currentPosition.x = Mathf.Clamp(currentPosition.x, startPosition.x - 1, startPosition.x + 1);
        currentPosition.z = Mathf.Clamp(currentPosition.z, startPosition.z - 1, startPosition.z + 1);
        wbChisel.transform.position = currentPosition;
    }

}