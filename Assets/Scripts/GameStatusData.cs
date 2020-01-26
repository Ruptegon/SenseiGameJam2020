using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusData : MessageBase
{
    public enum GameStatus { BuildAndConnect, GameplayRun }

    public GameStatus CurrentStatus = GameStatus.BuildAndConnect;


    public override void Deserialize(NetworkReader reader)
    {
        if (reader.ReadBoolean())
            CurrentStatus = GameStatus.GameplayRun;
        else
            CurrentStatus = GameStatus.BuildAndConnect;
    }

    public override void Serialize(NetworkWriter writer)
    {
        if (CurrentStatus == GameStatus.BuildAndConnect)
            writer.WriteBoolean(false);
        else
            writer.WriteBoolean(true);
    }

    public void RegisterHandler()
    {
        if (Net.IsClient)
        {
            if (!NetworkServer.active)
                NetworkClient.RegisterHandler<GameStatusData>(OnGameStatusSync);
        }
    }

    public void OnGameStatusSync(NetworkConnection conn, GameStatusData msg)
    {
        CurrentStatus = msg.CurrentStatus;
        GameManager.Instance.OnGameStatusSynced();
    }
}
