using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MessageBase
{
    [Serializable]
    public struct ObjectData
    {
        public int Id;
        public int X;
        public int Z;
    }


    public int SizeX;
    public int SizeZ;

    public List<ObjectData> ListOfObjects;

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

        ListOfObjects.Clear();
        var count = reader.ReadInt32();
        for (int i = 0; i < count; i++)
        {
            var obj = new ObjectData();
            obj.Id = reader.ReadInt32();
            obj.X = reader.ReadInt32();
            obj.Z = reader.ReadInt32();
            ListOfObjects.Add(obj);
        }
    }

    public override void Serialize(NetworkWriter writer)
    {
        writer.WriteInt32(SizeX);
        writer.WriteInt32(SizeZ);

        var count = ListOfObjects.Count;
        writer.WriteInt32(count);
        for (int i = 0; i < count; i++)
        {
            var obj = ListOfObjects[i];
            writer.WriteInt32(obj.Id);
            writer.WriteInt32(obj.X);
            writer.WriteInt32(obj.Z);
        }
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
        GameManager.Instance.BuildWorld();
    }

    internal void AddObject(int prefabId, Vector3 position)
    {

    }
}
