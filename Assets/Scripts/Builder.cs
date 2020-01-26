using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [Serializable]
    public class SyncPrefab
    {
        public GameObject Prefab;
        public bool Passable;
    }


    public GameObject FloorPrefab;
    public List<SyncPrefab> Prefabs;

    private WorldData world; //referencje to main world data, set in Init by GameManager

    public List<GameObject> runtimeWorldAssets = new List<GameObject>();

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
        world.AddObject(prefabId, positionX, positionZ);
        return true;
    }

    public int GetPrefabId(GameObject prefab)
    {
        var length = Prefabs.Count;
        for (int i = 0; i < length; i++)
        {
            if(Prefabs[i].Prefab == prefab)
            {
                return i;
            }
        }
        return -1;
    }


}
