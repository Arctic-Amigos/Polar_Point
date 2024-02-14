using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMining : MonoBehaviour
{
    string[] inventory = null;
    ObjectMineable currentMine = null;

    public delegate void OnEnterMine(ObjectMineable obj);
    public OnEnterMine onEnterMine;

    public delegate void OnExitMine();
    public OnExitMine onExitMine;

    // Start is called before the first frame update
    void Start()
    {
        inventory = this.GetComponent<Inventory>().inventory;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ObjectMineable"))
        {
            ObjectMineable obj;
            if (other.TryGetComponent(out obj))
            {
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

        onEnterMine.Invoke(currentMine);
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

    public void Mine()
    {

    }
   
}
