using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public GameObject FloorPrefab;
    public GameObject[] Prefabs;


    public void BuildWorld(WorldData data)
    {
        Debug.Log("Building floor...");
        for (int x = 0; x < data.SizeX; x++)
        {
            for (int z = 0; z < data.SizeZ; z++)
            {
                Instantiate(FloorPrefab, new Vector3(x, -1, z), Quaternion.identity, transform);
            }
        }
    }

}
