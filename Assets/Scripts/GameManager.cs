using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Builder Builder = new Builder();

    private WorldData worldData = new WorldData(10, 50);

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        worldData.RegisterHandler();
    }


    internal void SendSyncToClient(NetworkConnection conn)
    {
        Debug.Log($"Try send world data to address: {conn.address}");
        conn.Send(worldData);
    }
}
