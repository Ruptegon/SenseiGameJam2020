using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private WorldData worldData;
    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        worldData = new WorldData();

        worldData.SizeX = 10;
        worldData.SizeZ = 50;

        worldData.RegisterHandler();
    }



    internal void SendSyncToClient(NetworkConnection conn)
    {
        Debug.Log($"Try send world data to address: {conn.address}");
        conn.Send(worldData);
    }
}
