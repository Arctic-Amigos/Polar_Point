using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WorkbenchInteract : MonoBehaviour
{
    private Camera newCamera;
    // FOR BLAKE 4/19
    //Camera toolCamera;
    //GameObject toolCameraObject;
    private GameObject cameraObject;
    private GameObject bigCamera;

    Inventory inventory;
    GameObject pickaxe;

    GameObject workbench;
    GameObject wbChisel;
    GameObject wbBrush;
    Vector3 toolStartPosition;
    Vector3 toolCurrentPosition;

    bool interacting = false;

    GameObject player;
    GameObject crosshair;
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

    //public Animator chiselAnimator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        crosshair = GameObject.FindWithTag("Crosshair");
        playerMovement = player.GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
        pickaxe = GameObject.FindWithTag("PickaxeTag");
        wbChisel = GameObject.FindWithTag("WBChisel");
        wbBrush = GameObject.FindWithTag("WBBrush");
        workbench = GameObject.FindWithTag("Workbench");
        toolStartPosition = workbench.transform.position + new Vector3(1.4f, 2.3f, 0.7f);
        wbChisel.transform.position = toolStartPosition;
        wbBrush.transform.position = toolStartPosition;
        wbChisel.SetActive(false);
        wbBrush.SetActive(false);

        // FOR BLAKE 4/19
        //toolCameraObject = new GameObject("ToolCamera");
        //toolCamera = toolCameraObject.AddComponent<Camera>();
        //toolCamera.targetDisplay = -1;
        //toolCamera.transform.rotation = Quaternion.Euler(90, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(ChiselAnim());
        if (WorkbenchInteractable && !interacting)
        {
            if (!isMoving && Input.GetKeyDown(KeyCode.E))
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                AudioManager.instance.Stop("Walk"); // Might be problematic later?

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


                crosshair.SetActive(false);

                WorkbenchFunctionality();
            }
        }

        if (!isMoving && Input.GetKeyDown(KeyCode.E) && interacting)
        {
            EndWorkbenchFunctionality();
            
        }
        if (interacting)
        {
            toolCurrentPosition = wbChisel.transform.position;
            // FOR BLAKE 4/19
            //toolCamera.transform.position = toolCurrentPosition;
            if (inventory.inventory_pos == -2)
            {
                wbChisel.SetActive(true);
                wbBrush.SetActive(false);
            }
            else if (inventory.inventory_pos == -1)
            {
                wbChisel.SetActive(false);
                wbBrush.SetActive(true);
            }
            else
            {
                wbChisel.SetActive(false);
                wbBrush.SetActive(false);
            }
            toolCurrentPosition.x -= Input.GetAxis("Mouse X") * 0.01f;
            toolCurrentPosition.z -= Input.GetAxis("Mouse Y") * 0.01f;
            toolCurrentPosition.x = Mathf.Clamp(toolCurrentPosition.x, toolStartPosition.x - 1.15f, toolStartPosition.x + 1.05f);
            toolCurrentPosition.z = Mathf.Clamp(toolCurrentPosition.z, toolStartPosition.z - 0.6f, toolStartPosition.z + 0.6f);
            wbChisel.transform.position = toolCurrentPosition;
            wbBrush.transform.position = toolCurrentPosition;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Workbench"))
        {
            WorkbenchInteractable = true;
            endPos = other.transform.position + new Vector3(1.4f, 3.4f, 0.7f);
            Quaternion endRotTemp = other.transform.rotation;
            Quaternion rotation = Quaternion.Euler(90, 180, 0); // Currently gives a top-down view
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

    private void EndWorkbenchFunctionality()
    {
        StartCoroutine(EndMoveCamera());
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

    IEnumerator EndMoveCamera()
    {
        isMoving = true;
        elapsedTime = 0f;
        AudioListener audioListener = newCamera.gameObject.GetComponent<AudioListener>();
        Destroy(audioListener);
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            newCamera.transform.position = Vector3.Lerp(endPos, startPos, t);
            newCamera.transform.rotation = Quaternion.Slerp(endRot, startRot, t);
            yield return null;
        }
        isMoving = false;
        Destroy(cameraObject);
        bigCamera.SetActive(true);
        playerMovement.enabled = true;
        interacting = false;
        rb.constraints = ~(RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ);
        rb.constraints = ~(RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ);
        crosshair.SetActive(true);
    }

    public bool IsWorkbenchInteracting()
    {
        return interacting;
    }
    public Vector3 GetToolCurrentPosition()
    {
        return toolCurrentPosition;
    }
    public Camera GetToolCamera()
    {
        return toolCamera;
    }
    /*
    IEnumerator ChiselAnim()
    {
        if (Input.GetMouseButton(0))
        {
            chiselAnimator.SetBool("chiselingActive", true);
            yield return new WaitForSeconds(.3f);
        }
        else
        {
            chiselAnimator.SetBool("chiselingActive", false);
        }
    }
    */
}
