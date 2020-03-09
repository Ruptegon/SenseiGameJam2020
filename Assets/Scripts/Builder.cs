using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Builder : MonoBehaviour
{
    [Serializable]
    public class SyncPrefab
    {
        public GameObject Prefab;
        public bool Passable;
        public bool Deadly;
    }

    public GameObject FloorPrefab;
    public GameObject GoalChestPrefab;
    public List<SyncPrefab> Prefabs;

    public UnityEvent OnWorldRebuild = new UnityEvent();

    private WorldData world; //referencje to main world data, set in Init by GameManager

    class WorldObject
    {
        public GameObject GameObject;
        public float PositionX;
        public float PositionZ;

        public WorldObject(GameObject gameObject, float positonX, float positionZ)
        {
            GameObject = gameObject;
            PositionX = positonX;
            PositionZ = positionZ;
        }

    }


    private List<WorldObject> runtimeWorldAssets = new List<WorldObject>();

    public void Init(WorldData worldData)
    {
        world = worldData;
    }

    public void CleanAndBuildWorld(WorldData data)
    {
        data.Reset();
        BuildWorld(data);
        OnWorldRebuild.Invoke();
    }

    public void BuildWorld(WorldData data)
    {
        Debug.Log("Clearing world...");
        for (int i = 0; i < runtimeWorldAssets.Count; i++)
        {
            Destroy(runtimeWorldAssets[i].GameObject);
        }
        runtimeWorldAssets.Clear();

        Debug.Log("Building floor...");
        for (int x = 0; x < data.SizeX; x++)
        {
            for (int z = 0; z < data.SizeZ; z++)
            {
                var istance = Instantiate(FloorPrefab, new Vector3(x, -0.5f, z), Quaternion.identity, transform);
                runtimeWorldAssets.Add(new WorldObject(istance, data.SizeX, data.SizeZ));
            }
        }

        Debug.Log($"Building objects...");
        for (int x = 0; x < world.SizeX; x++)
        {
            for (int z = 0; z < world.SizeZ; z++)
            {
                var prefabId = world.Map[x, z];

                if (prefabId == -1)
                    continue;

                InstantiateObject(prefabId, x, z);
            }
        }

        Debug.Log($"Building goal chest...");
        var xGoalChestPosition = (int)(GameManager.World.SizeX / 2f);
        var instance = Instantiate(GoalChestPrefab, new Vector3(xGoalChestPosition, 0f, world.SizeZ), Quaternion.identity, transform);
        runtimeWorldAssets.Add(new WorldObject(instance, xGoalChestPosition, world.SizeZ));
    }

    private void InstantiateObject(int prefabId, int positionX, int positionZ)
    {
        Debug.Log("SUPER DEBUG!");
        if (prefabId == -1)
            return;


        var instance = Instantiate(Prefabs[prefabId].Prefab, new Vector3(positionX, 0f, positionZ), Quaternion.identity, transform);
        runtimeWorldAssets.Add(new WorldObject(instance, positionX, positionZ));
    }

    public bool BuildObject(GameObject prefab, int positionX, int positionZ)
    {
        if (!world.IsEmpty(positionX, positionZ))
        {
            RemoveObject(positionX, positionZ);
        }

        var instance = Instantiate(prefab, new Vector3(positionX, 0, positionZ), Quaternion.identity, transform);
        runtimeWorldAssets.Add(new WorldObject(instance, positionX, positionZ));
        var prefabId = GetPrefabId(prefab);
        var passable = Prefabs[prefabId].Passable;
        var deadly = Prefabs[prefabId].Deadly;
        world.AddObject(prefabId, passable, deadly, positionX, positionZ);
        return true;
    }

    public bool RemoveObject(int positionX, int positionZ)
    {
        var worldObject = runtimeWorldAssets.FirstOrDefault(x => x.PositionX == positionX && x.PositionZ == positionZ);
        if(worldObject != null && runtimeWorldAssets.Remove(worldObject))
        {
            world.ResetFiled(positionX, positionZ);
            Destroy(worldObject.GameObject);
        }

        return false;
    }

    public int GetPrefabId(GameObject prefab)
    {
        var length = Prefabs.Count;
        for (int i = 0; i < length; i++)
        {
            if (Prefabs[i].Prefab == prefab)
            {
                return i;
            }
        }
        return -1;
    }


}
