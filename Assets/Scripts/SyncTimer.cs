using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTimer : MessageBase
{
    public double TimeValue = 0f;
    public override void Deserialize(NetworkReader reader)
    {
        TimeValue = reader.ReadDouble();

    }

    public override void Serialize(NetworkWriter writer)
    {
        writer.WriteDouble(TimeValue);
    }

    public void RegisterHandler()
    {
        if (Net.IsServer)
        {

        }
        if (Net.IsClient)
        {
            if (!NetworkServer.active)
                NetworkClient.RegisterHandler<SyncTimer>(OnWorldData);
        }
    }

    public void OnWorldData(NetworkConnection conn, SyncTimer msg)
    {
        TimeValue = msg.TimeValue;
    }

    public void Update()
    {
        TimeValue += Time.deltaTime;
        NetworkServer.SendToAll(this);
    }
}
