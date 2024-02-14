using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMineable : MonoBehaviour
{
    PlayerMining currentMiner = null;

    public delegate void OnEmpty();
    public OnEmpty onEmpty;
    public void Start()
    {

    }
    public void EnterMine(PlayerMining miner)
    {
        currentMiner = miner;
    }
    public void ExitMine() => currentMiner = null;

    public void MineResource(Inventory inventory)
    {
        int nextAvaiableSpot = inventory.FirstEmpty();
        if(nextAvaiableSpot != 9)
        {
            currentMiner.ReceiveResource();
        }
        //this.SetActive(false);
        onEmpty.Invoke();
    }
}
