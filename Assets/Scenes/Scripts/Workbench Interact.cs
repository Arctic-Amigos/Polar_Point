using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WorkbenchInteract : MonoBehaviour
{
    private Camera newCamera;
    private GameObject cameraObject;
    private GameObject bigCamera;

    Inventory inventory;
    GameObject pickaxe;
    GameObject wbChisel;

    bool interacting = false;

    GameObject player;
    PlayerMovement playerMovement;
    Rigidbody rb;

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
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
        pickaxe = GameObject.FindWithTag("PickaxeTag");
        wbChisel = GameObject.FindWithTag("WBChisel");
    }

    // Update is called once per frame
    void Update()
    {
        if (WorkbenchInteractable && !interacting)
        {
            if (!isMoving && Input.GetKeyDown(KeyCode.E))
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

                playerMovement.enabled = false;
                interacting = true;

                cameraObject = new GameObject("NewCamera");
                newCamera = cameraObject.AddComponent<Camera>();
                newCamera.gameObject.AddComponent<AudioListener>();
                newCamera.tag = "MainCamera";
                newCamera.transform.position = Camera.main.transform.position;
                newCamera.transform.rotation = Camera.main.transform.rotation;

                bigCamera = GameObject.FindWithTag("MainCamera");
                bigCamera.SetActive(false);

                newCamera.enabled = true;

                startPos = Camera.main.transform.position;
                startRot = Camera.main.transform.rotation;

                WorkbenchFunctionality();
            }
        }

        if (!isMoving && Input.GetKeyDown(KeyCode.Escape))
        {
            bigCamera.SetActive(true);
            playerMovement.enabled = true;
            interacting = false;
            Destroy(cameraObject);
            rb.constraints = ~(RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ);
        }
        if (interacting)
        {
            Vector3 mousePos = Input.mousePosition;

            // Convert the screen coordinates to world coordinates
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // Keep the y position of the object unchanged
            mousePos.y = wbChisel.transform.position.y;

            // Update the position of the GameObject to match the mouse position
            wbChisel.transform.position = mousePos;
            Debug.Log("Mouse: " + mousePos.x + ", "  + mousePos.y + ", " + mousePos.z);
            Debug.Log("Chisel: " + wbChisel.transform.position.x + ", " + wbChisel.transform.position.y + ", " + wbChisel.transform.position.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Workbench"))
        {
            WorkbenchInteractable = true;
            endPos = other.transform.position + new Vector3(0, 2, 0);
            Quaternion endRotTemp = other.transform.rotation;
            Quaternion rotation = Quaternion.Euler(90, 0, 0); // Currently gives a top-down view
            endRot = endRotTemp * rotation;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Workbench"))
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

    public bool IsWorkbenchInteracting()
    {
        return interacting;
    }
}
