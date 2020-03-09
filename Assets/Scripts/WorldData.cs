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
    public bool[,] MapPassable; //not synced
    public bool[,] MapDeadly; //not synced

    public WorldData()
    {

    }

    public WorldData(int sizeX, int sizeZ)
    {
        if (Net.IsClient)
            return;

        SizeX = sizeX;
        SizeZ = sizeZ;
        Map = new int[SizeX, SizeZ];
        MapPassable = new bool[SizeX, SizeZ];
        MapDeadly = new bool[SizeX, SizeZ];

        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                Map[x, z] = -1;
                MapPassable[x, z] = true;
                MapDeadly[x, z] = true;
            }
        }
    }

    public void Reset()
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                Map[x, z] = -1;
                MapPassable[x, z] = true;
                MapDeadly[x, z] = true;
            }
        }
    }

    public override void Deserialize(NetworkReader reader)
    {
        //Debug.Log("Deserializacja");
        SizeX = reader.ReadInt32();
        SizeZ = reader.ReadInt32();

        Map = new int[SizeX, SizeZ];
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                Map[x, z] = reader.ReadInt32();
                //MapPassable[x, z] = reader.ReadBoolean();
                //Debug.Log($"[{x},{z}] = {Map[x,z]}");
            }
        }

    }

    public override void Serialize(NetworkWriter writer)
    {
        //Debug.Log("Serializacja");

        writer.WriteInt32(SizeX);
        writer.WriteInt32(SizeZ);

        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                writer.WriteInt32(Map[x, z]);
                //writer.WriteBoolean(MapPassable[x, z]);
                //Debug.Log($"[{x},{z}] = {Map[x, z]}");
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

        SizeX = msg.SizeX;
        SizeZ = msg.SizeZ;
        Map = msg.Map;

        Debug.Log($"World [{SizeX}:{SizeZ}]");
        GameManager.Instance.BuildWorld();
    }

    public void ResetFiled(int positionX, int positionZ)
    {
        Map[positionX, positionZ] = -1;
        MapPassable[positionX, positionZ] = false;
        MapDeadly[positionX, positionZ] = true;
    }

    public bool AddObject(int prefabId, bool Passable, bool deadly, int positionX, int positionZ)
    {
        if (Map[positionX, positionZ] != -1)
            return false;

        Map[positionX, positionZ] = prefabId;
        MapPassable[positionX, positionZ] = Passable;
        MapDeadly[positionX, positionZ] = deadly;
        return true;
    }

    public bool IsEmpty(int positionX, int positionZ)
    {
        if (Map[positionX, positionZ] == -1)
            return true;

        return false;
    }

    public bool IsPassable(int positionX, int positionZ)
    {
        if (positionX < 0 || positionZ < 0)
            return false;

        if (positionX >= SizeX)
            return false;

        if (positionZ >= SizeZ)
            return false;

        return MapPassable[positionX, positionZ];
    }
}
