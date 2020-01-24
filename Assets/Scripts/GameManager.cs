using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private WorldData worldData;
    private void Awake()
    {
        worldData = new WorldData();

        worldData.SizeX = 10;
        worldData.SizeZ = 50;

        worldData.RegisterHandler();
    }

    private void Start()
    {
        if(Net.IsServer)
        {
            Debug.Log("Try to send world to all...");
            NetworkServer.SendToAll(worldData);
        }
    }
}
