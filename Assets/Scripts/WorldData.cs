using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MessageBase
{
    public int SizeX;
    public int SizeZ;

    public int[,] Map;

    public WorldData()
    {

    }

    public WorldData(int sizeX, int sizeZ)
    {
        SizeX = sizeX;
        SizeZ = sizeZ;
        Map = new int[SizeX, SizeZ];
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                Map[x, z] = -1;
            }
        }
    }

    public override void Deserialize(NetworkReader reader)
    {
        Debug.Log("Deserializacja");
        SizeX = reader.ReadInt32();
        SizeZ = reader.ReadInt32();

        Map = new int[SizeX, SizeZ];
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                Map[x, z] = reader.ReadInt32();
                Debug.Log($"[{x},{z}] = {Map[x,z]}");
            }
        }

    }

    public override void Serialize(NetworkWriter writer)
    {
        Debug.Log("Serializacja");

        writer.WriteInt32(SizeX);
        writer.WriteInt32(SizeZ);

        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                writer.WriteInt32(Map[x, z]);
                Debug.Log($"[{x},{z}] = {Map[x, z]}");
            }
        }
    }

    public void RegisterHandler()
    {
        if (Net.IsServer)
        {

        }
        if (Net.IsClient)
        {
            if (!NetworkServer.active)
                NetworkClient.RegisterHandler<WorldData>(OnWorldData);
        }
    }

    public void OnWorldData(NetworkConnection conn, WorldData msg)
    {
        Debug.Log($"World [{msg.SizeX}:{msg.SizeZ}]");
        GameManager.Instance.BuildWorld();
    }

    public bool AddObject(int prefabId, int positionX, int positionZ)
    {
        if (Map[positionX, positionZ] != -1)
            return false;

        Map[positionX, positionZ] = prefabId;
        return true;
    }

    public bool IsEmpty(int positionX, int positionZ)
    {
        if (Map[positionX, positionZ] == -1)
            return true;

        return false;
    }
}
