using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMineable : MonoBehaviour
{
    //Reference to player object
    PlayerMining currentMiner = null;

    public void EnterMine(PlayerMining miner)
    {
        currentMiner = miner;
    }
    public void ExitMine() => currentMiner = null;

    public void MineResource()
    {
        currentMiner.ReceiveResource();
        this.gameObject.SetActive(false); //Deactivate mineable object once player, allows us to reactivate it in the future, i.e. on a new day cycle
        
    }
}
