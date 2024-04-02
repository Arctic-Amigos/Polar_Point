using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMining : MonoBehaviour
{
    //Reference to a possible mineable object the player is near
    public ObjectMineable currentMine = null;
    //Reference to the players inventory
    Inventory inventory = null;

    public Animator pickaxeAnimator;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        
    }
    private void Update()
    {
        StartCoroutine(MiningAnim());
        if (Input.GetMouseButton(0) && inventory.inventory_pos == -3)
        {
            //Puts player into a mining animation (think of it as swinging your pickaxe in minecraft)
            //animation will be added later
            FindObjectOfType<AudioManager>().Play("Pickaxe");
            this.MineCurrent();
            
        }
    }
    //Activated by sphere collider on player
    void OnTriggerEnter(Collider other)
    {
        //if player is near obj with mineable tag puts player into state of being able to mine object
        if(other.CompareTag("ObjectMineable"))
        {
            ObjectMineable obj;
            //checks to see if object near player is mineable (prevents players from mining objects without tag by accident)
            if (other.TryGetComponent(out obj))
            {
                EnterMine(obj);
            }

            //Trigger Text Dialogue
            //Set a 15 second timer. Start timer on first collision and only execute function if greater than 15 sec
            //TextDialogue textDialogue = FindObjectOfType<TextDialogue>();
            //textDialogue.DisplayFoundMsg();
        }
    }
    //Removes players ability to mine objects once they are too far away from mineable object
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("ObjectMineable"))
        {
            ExitMine();
        }
    }
    //State of being able to mine object
    void EnterMine(ObjectMineable obj)
    {
        currentMine = obj;

        currentMine.EnterMine(this); //sets  mineable object as ready for mining
    }
    void ExitMine()
    {
        if(currentMine)
        {
            currentMine.ExitMine();
        }
        currentMine = null;
        
    }
    //Called when player tries to mine an object
    public void MineCurrent()
    {
        if(currentMine)
        {
            currentMine.MineResource();
            AudioManager.instance.Stop("Pickaxe");
            ExitMine(); //Removes players ability to mine the same object after it has been destroyed
        }
    }

    public IEnumerator MiningAnim()
    {
        if (Input.GetMouseButton(0))
        {
            pickaxeAnimator.SetBool("miningActive", true);

            yield return new WaitForSeconds(.3f);
        }else
        {
            pickaxeAnimator.SetBool("miningActive", false);
            
        }
    }
    public void ReceiveResource()
    {
        int nextAvaiableSpot = inventory.FirstEmpty();

        if (nextAvaiableSpot <= 4)
        {
            inventory.SetInventory(nextAvaiableSpot, "ChiselableBone");
        }else
        {
            Debug.Log("Inventory is full!");
        }
    }
   
}
