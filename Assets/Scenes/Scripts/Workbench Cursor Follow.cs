using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchCursorFollow : MonoBehaviour
{
    Inventory inventory;
    public int desired_pos;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory.inventory_pos == 2)
        {
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorPosition.z = 0f;
            transform.position = cursorPosition;
        }
    }
}
