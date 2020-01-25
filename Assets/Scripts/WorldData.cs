using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MessageBase
{
    public int SizeX;
    public int SizeZ;

    public WorldData()
    {

    }

    public WorldData(int x, int z)
    {
        SizeX = x;
        SizeZ = z;
    }
    public override void Deserialize(NetworkReader reader)
    {
        SizeX = reader.ReadInt32();
        SizeZ = reader.ReadInt32();
    }

    public override void Serialize(NetworkWriter writer)
    {
        writer.WriteInt32(SizeX);
        writer.WriteInt32(SizeZ);
    }

    public void RegisterHandler()
    {
        if(Net.IsServer)
        {
            
        }
        if(Net.IsClient)
        {
            if (!NetworkServer.active)
                NetworkClient.RegisterHandler<WorldData>(OnWorldData);
        }
    }

    public void OnWorldData(NetworkConnection conn, WorldData msg)
    {
        Debug.Log($"World [{msg.SizeX}:{msg.SizeZ}]");
    }

}
