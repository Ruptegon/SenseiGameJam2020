using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusData : MessageBase
{
    public enum GameStatus { none, BuildAndConnect, GameplayRun }

    public GameStatus CurrentStatus = GameStatus.none;


    public override void Deserialize(NetworkReader reader)
    {
        CurrentStatus = (GameStatus)reader.ReadInt16();
    }

    public override void Serialize(NetworkWriter writer)
    {
        writer.WriteInt16((short)CurrentStatus);
    }

    public void RegisterHandler()
    {
        if (Net.IsServer)
        {

        }
        if (Net.IsClient)
        {
            if (!NetworkServer.active)
                NetworkClient.RegisterHandler<GameStatusData>(OnGameStatusSync);
        }
    }

    public void OnGameStatusSync(NetworkConnection conn, GameStatusData msg)
    {
        CurrentStatus = msg.CurrentStatus;
    }
}
