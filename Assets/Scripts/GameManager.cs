using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static float SyncTimer => (float)Instance.syncTimer.TimeValue;
    public static Builder Builder;
    public static WorldData World => Instance.worldData;
    public static GameStatusData.GameStatus GameStatus => Instance.gameStatus.CurrentStatus;
    public static ChestScore GoalChest = new ChestScore();

    private WorldData worldData = new WorldData(7, 30);
    private SyncTimer syncTimer = new SyncTimer();
    private GameStatusData gameStatus = new GameStatusData();

    private void Awake()
    {
        Instance = this;

        Builder = GetComponent<Builder>();
        Builder.Init(worldData);

        worldData.RegisterHandler();
        syncTimer.RegisterHandler();
        gameStatus.RegisterHandler();
    }

    private void Start()
    {
        if(Net.IsServer)
        {
            BuildWorld();
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

    public void OnGameStatusSynced()
    {
        if(GameStatus == GameStatusData.GameStatus.BuildAndConnect)
        {
            if (!Player.LocalPlayer)
            {
                ClientScene.AddPlayer();
            }
        }
        else if(GameStatus == GameStatusData.GameStatus.GameplayRun)
        {
            Player.LocalPlayer.ResetPlayer();
            Player.ResetMatch(); //TODO: To jest złe miejsce na takie rzeczy!
        }
        Debug.Log("GameStatus " + GameStatus);
    }

    //Server
    public void StartGameplayRunStage()
    {
        if (GameStatus == GameStatusData.GameStatus.GameplayRun)
            return;

        gameStatus.CurrentStatus = GameStatusData.GameStatus.GameplayRun;
        NetworkServer.SendToAll(worldData);
        NetworkServer.SendToAll(gameStatus);

        Player.ResetMatch();
        GoalChest.Init(Player.Instances.Count);

        Debug.Log("StartGameplayRunStage");
    }

    //Server
    public void StartBuildAndConnectStage()
    {
        if (GameStatus == GameStatusData.GameStatus.BuildAndConnect)
            return;

        gameStatus.CurrentStatus = GameStatusData.GameStatus.BuildAndConnect;
        Builder.CleanAndBuildWorld(worldData);
        NetworkServer.SendToAll(gameStatus);

        GoalChest.GiveScoreToServer();

        Debug.Log("StartBuildAndConnectStage");
    }

    internal void SendSyncToClient(NetworkConnection conn)
    {
        //conn.Send(worldData);
        conn.Send(gameStatus);
    }
}
