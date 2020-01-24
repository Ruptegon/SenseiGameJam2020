using Mirror;
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

        IsServer = buildMode.GameMode == BuildMode.Mode.Server;
        IsClient = buildMode.GameMode == BuildMode.Mode.Client;

        //IsServer = Application.platform == RuntimePlatform.WindowsEditor;
        //IsClient = Application.platform == RuntimePlatform.Android;

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
}
