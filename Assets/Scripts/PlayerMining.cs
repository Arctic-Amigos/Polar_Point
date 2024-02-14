using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMining : MonoBehaviour
{
    public ObjectMineable currentMine = null;
    Inventory inventory = null;

    public delegate void OnEnterMine(ObjectMineable obj);
    public OnEnterMine onEnterMine;

    public delegate void OnExitMine();
    public OnExitMine onExitMine;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            this.MineCurrent();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
           /* Debug.Log("Inventory slot 1 contains: " + inventory.GetInventory(0));
            Debug.Log("Inventory slot 2 contains: " + inventory.GetInventory(1));
            Debug.Log("Inventory slot 3 contains: " + inventory.GetInventory(2));
            Debug.Log("Inventory slot 4 contains: " + inventory.GetInventory(3));
            Debug.Log("Inventory slot 5 contains: " + inventory.GetInventory(4)); */
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ObjectMineable"))
        {
            ObjectMineable obj;
            if (other.TryGetComponent(out obj))
            {
                Debug.Log("Object Name: " +  obj.name);
                EnterMine(obj);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("ObjectMineable"))
        {
            ExitMine();
        }
    }
    void EnterMine(ObjectMineable obj)
    {
        currentMine = obj;

        currentMine.EnterMine(this);

        //onEnterMine.Invoke(currentMine);
    }
    void ExitMine()
    {
        if(currentMine)
        {
            currentMine.ExitMine();
        }
        currentMine = null;

        onExitMine.Invoke();
    }
    public void MineCurrent()
    {
        if(currentMine)
        {
            currentMine.MineResource(inventory);
        }
    }
    public void ReceiveResource()
    {
        int nextAvaiableSpot = inventory.FirstEmpty();

        if (nextAvaiableSpot <= 4)
        {

            inventory.SetInventory(nextAvaiableSpot, "bone");
        }else
        {
            Debug.Log("Inventory is full!");
        }
    }
   
}
