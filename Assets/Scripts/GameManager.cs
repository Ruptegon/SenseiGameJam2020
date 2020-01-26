﻿using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static float SyncTimer => (float)Instance.syncTimer.TimeValue;
    public static Builder Builder;

    private WorldData worldData = new WorldData(7, 30);
    private SyncTimer syncTimer = new SyncTimer();


    private void Awake()
    {
        Instance = this;

        Builder = GetComponent<Builder>();
        Builder.Init(worldData);

        worldData.RegisterHandler();
        syncTimer.RegisterHandler();
    }

    private void Start()
    {
        if(Net.IsServer)
        {
            BuildWorld();

            Builder.BuildObject(Builder.Prefabs[0], 2, 2);
            Builder.BuildObject(Builder.Prefabs[1], 3, 3);
            Builder.BuildObject(Builder.Prefabs[2], 4, 4);
            Builder.BuildObject(Builder.Prefabs[3], 5, 5);
            Builder.BuildObject(Builder.Prefabs[4], 1, 6);
            Builder.BuildObject(Builder.Prefabs[5], 2, 7);
            Builder.BuildObject(Builder.Prefabs[6], 3, 8);
            Builder.BuildObject(Builder.Prefabs[7], 4, 9);
            Builder.BuildObject(Builder.Prefabs[8], 5, 10);
            Builder.BuildObject(Builder.Prefabs[9], 6, 10);

        }
    }

    private void Update()
    {
        syncTimer.Update();
    }

    public void BuildWorld()
    {
        if (!Builder)
            Debug.LogError("Builder is not declared!");
        else
        {
            Builder.BuildWorld(worldData);
        }
    }


    internal void SendSyncToClient(NetworkConnection conn)
    {
        Debug.Log($"Try send world data to address: {conn.address}");
        conn.Send(worldData);
    }
}
