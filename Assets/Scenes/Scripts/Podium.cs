using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podium : MonoBehaviour
{
    PlayerPodiumInteract player = null;
    
    public void EnterPodium(PlayerPodiumInteract _player)
    {
        player = _player;
    }
    
    public void ExitPodium() => player = null;

    
}
