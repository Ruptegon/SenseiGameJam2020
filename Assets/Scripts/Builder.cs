using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Builder : MonoBehaviour
{
    [Serializable]
    public class SyncPrefab
    {
        public GameObject Prefab;
        public bool Passable;
    }

    public GameObject FloorPrefab;
    public GameObject GoalChestPrefab;
    public List<SyncPrefab> Prefabs;

    public UnityEvent OnWorldRebuild = new UnityEvent();

    private WorldData world; //referencje to main world data, set in Init by GameManager

    private List<GameObject> runtimeWorldAssets = new List<GameObject>();

    public void Init(WorldData worldData)
    {
        world = worldData;
    }
    public void BuildWorld(WorldData data)
    {
        Debug.Log("Clearing world...");
        for (int i = 0; i < runtimeWorldAssets.Count; i++)
        {
            Destroy(runtimeWorldAssets[i]);
        }
        runtimeWorldAssets.Clear();

        Debug.Log("Building floor...");
        for (int x = 0; x < data.SizeX; x++)
        {
            for (int z = 0; z < data.SizeZ; z++)
            {
                runtimeWorldAssets.Add(Instantiate(FloorPrefab, new Vector3(x, -0.5f, z), Quaternion.identity, transform));
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
        runtimeWorldAssets.Add(Instantiate(GoalChestPrefab, new Vector3(xGoalChestPosition, 0f, world.SizeZ), Quaternion.identity, transform));
    }

    private void InstantiateObject(int prefabId, int positionX, int positionZ)
    {
        Debug.Log("SUPER DEBUG!");
        if (prefabId == -1)
            return;



        runtimeWorldAssets.Add(Instantiate(Prefabs[prefabId].Prefab, new Vector3(positionX, 0f, positionZ), Quaternion.identity, transform));
    }

    public bool BuildObject(GameObject prefab, int positionX, int positionZ)
    {
        if (!world.IsEmpty(positionX, positionZ))
            return false;

        runtimeWorldAssets.Add(Instantiate(prefab, new Vector3(positionX, 0, positionZ), Quaternion.identity, transform));
        var prefabId = GetPrefabId(prefab);
        var passable = Prefabs[prefabId].Passable;
        world.AddObject(prefabId, passable, positionX, positionZ);
        return true;
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
