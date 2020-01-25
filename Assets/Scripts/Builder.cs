using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public GameObject FloorPrefab;
    public List<GameObject> Prefabs;

    public List<GameObject> runtimeWorldAssets;


    private WorldData world;

    public void Init(WorldData worldData)
    {
        world = worldData;
    }
    public void BuildWorld(WorldData data)
    {
        Debug.Log("Building floor...");
        for (int x = 0; x < data.SizeX; x++)
        {
            for (int z = 0; z < data.SizeZ; z++)
            {
                runtimeWorldAssets.Add(Instantiate(FloorPrefab, new Vector3(x, -1, z), Quaternion.identity, transform));
            }
        }
    }

    public void BuildObject(GameObject prefab, Vector3 position)
    {
        runtimeWorldAssets.Add(Instantiate(prefab, position, Quaternion.identity, transform));
        var prefabId = Prefabs.IndexOf(prefab);
        world.AddObject(prefabId, position);
    }

}
