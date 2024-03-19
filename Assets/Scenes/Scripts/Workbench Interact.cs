using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WorkbenchInteract : MonoBehaviour
{
    private Camera newCamera;
    private GameObject cameraObject;

    GameObject player;
    PlayerMovement playerMovement;

    private bool WorkbenchInteractable = false;
    private float elapsedTime = 0f;
    private bool isMoving = false;

    public Vector3 startPos;
    public Vector3 endPos;
    public Quaternion startRot;
    public Quaternion endRot;
    public float moveDuration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WorkbenchInteractable)
        {
            if (!isMoving && Input.GetKeyDown(KeyCode.E))
            {
                cameraObject = new GameObject("NewCamera");
                newCamera = cameraObject.AddComponent<Camera>();
                newCamera.transform.position = Camera.main.transform.position;
                newCamera.transform.rotation = Camera.main.transform.rotation;
                newCamera.enabled = true;

                startPos = Camera.main.transform.position;
                startRot = Camera.main.transform.rotation;

                playerMovement.enabled = false;

                WorkbenchFunctionality();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerMovement.enabled = true;
            Destroy(cameraObject);
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Workbench"))
        {
            WorkbenchInteractable = true;
            endPos = other.transform.position + new Vector3(0, 2, 0);
            Vector3 downD = new Vector3(1, 0, 0);
            Quaternion otherRot = other.transform.rotation;
            Vector3 forwardDirection = otherRot * Vector3.forward;
            Vector3 upDirection = otherRot * Vector3.up;
            Vector3 rightDirection = otherRot * Vector3.right;
            Debug.Log(forwardDirection.x);
            Debug.Log(forwardDirection.y);
            Debug.Log(forwardDirection.z);
            Debug.Log(upDirection.x);
            Debug.Log(upDirection.y);
            Debug.Log(upDirection.z);
            Debug.Log(rightDirection.x);
            Debug.Log(rightDirection.y);
            Debug.Log(rightDirection.z);
            Vector3 combined = forwardDirection + upDirection + rightDirection;
            Vector3 direction = combined;// + downD;
            Debug.Log(combined.x);
            Debug.Log(combined.y);
            Debug.Log(combined.z);
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
            newCamera.transform.position = Vector3.Lerp(startPos, endPos, t);
            newCamera.transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
        isMoving = false;
    }
}
