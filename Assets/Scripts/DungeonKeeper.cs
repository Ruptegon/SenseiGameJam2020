using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonKeeper : MonoBehaviour
{
    public static DungeonKeeper Instance;

    private GameObject toSpawn;
    private Vector3 spawnLocation;
    private Plane plane = new Plane(Vector3.up, Camera.main.transform.position.y);

    private void Awake()
    {
        Instance = this;
    }

    public void SetSelectedPrefab(GameObject prefab) => toSpawn = prefab;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Debug.Log("Mouse down 2!");

            if (toSpawn != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float enter = 0.0f;

                if (plane.Raycast(ray, out enter))
                {
                    Vector3 worldPos = ray.GetPoint(enter);
                    worldPos += new Vector3(0.5f, 0.5f, 0.5f);
                    if (worldPos.x >= 0 && worldPos.z >= 0 && worldPos.x < 7 && worldPos.z < 30)
                    {
                        GameManager.Builder.BuildObject(toSpawn, (int)worldPos.x, (int)worldPos.z);
                        Debug.Log("Mouse pos = " + Input.mousePosition);
                        Debug.Log("World pos = " + worldPos);
                    }
                }
            }
        }
    }
}
