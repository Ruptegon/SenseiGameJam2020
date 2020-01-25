using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private WorldData worldData = new WorldData(11, 50);
    private Builder builder;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        builder = GetComponent<Builder>();
        builder.Init(worldData);

        worldData.RegisterHandler();
    }

    private void Start()
    {
        if(Net.IsServer)
        {
            BuildWorld();
        }
    }

    public void BuildWorld()
    {
        if (!builder)
            Debug.LogError("Builder is not declared!");
        else
        {
            builder.BuildWorld(worldData);
        }
    }


    internal void SendSyncToClient(NetworkConnection conn)
    {
        Debug.Log($"Try send world data to address: {conn.address}");
        conn.Send(worldData);
    }
}
