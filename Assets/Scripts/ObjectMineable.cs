using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMineable : MonoBehaviour
{
    PlayerMining currentMiner = null;
    string[] inventory = null;
    public void Start()
    {
        inventory = this.GetComponent<Inventory>().inventory;
    }
    public void EnterMine(PlayerMining miner)
    {
        currentMiner = miner;
    }
    public void ExitMine() => currentMiner = null;

    public void MineResource()
    {

    }
}
