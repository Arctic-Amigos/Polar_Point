using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WorkbenchInteract : MonoBehaviour
{
    private bool WorkbenchInteractable = false;
    private float elapsedTime = 0f;
    private bool isMoving = false;

    public Vector3 startPos;
    public Vector3 endPos;
    public Quaternion startRot;
    public Quaternion endRot;
    public float moveDuration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WorkbenchInteractable)
        {
            if (!isMoving && Input.GetKeyDown(KeyCode.E))
            {
                startPos = gameObject.transform.position;
                startRot = gameObject.transform.rotation;
                WorkbenchFunctionality();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Workbench"))
        {
            WorkbenchInteractable = true;
            endPos = other.transform.position + new Vector3(0, 2, 0);
            Vector3 direction = new Vector3(-1, 0, 0);
            endRot = Quaternion.LookRotation(direction);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Workbench"))
        {
            WorkbenchInteractable = false;
        }
    }

    private void WorkbenchFunctionality()
    {
        Debug.Log("Working on the bench");
        StartCoroutine(MoveCamera());
    }

    IEnumerator MoveCamera()
    {
        isMoving = true;
        elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
        isMoving = false;
    }
}
