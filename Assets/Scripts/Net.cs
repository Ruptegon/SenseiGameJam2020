﻿using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : NetworkManager
{
    [SerializeField] BuildMode buildMode;
    public static bool IsServer;
    public static bool IsClient;
    new private void Awake()
    {
        base.Awake();

#if UNITY_EDITOR
        IsServer = buildMode.GameMode == BuildMode.Mode.Server;
        IsClient = buildMode.GameMode == BuildMode.Mode.Client;
#else
        IsServer = Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
        IsClient = Application.platform == RuntimePlatform.Android;
#endif

        Debug.Log($"Net.Awake() status: IsClient: {IsClient}, IsServer: {IsServer}");

        if (!IsClient && !IsServer)
            Debug.LogError("Application is neither a server or a client");
    }

    new private void Start()
    {
        if(Net.IsServer)
        {
            base.StartServer();
        }
        else if(Net.IsClient)
        {
            base.Start();
        }
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (!GameManager.Instance)
        {
            Debug.LogError("GameManager doesn't exist, disconnect client");
            conn.Disconnect();
        }
        else
        {
            GameManager.Instance.SendSyncToClient(conn);
        }
    }
}