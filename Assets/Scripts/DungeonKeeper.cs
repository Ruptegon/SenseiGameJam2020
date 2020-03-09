using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DungeonKeeper : MonoBehaviour
{
    public static DungeonKeeper Instance;

    private GameObject toSpawn;
    private int costOfNext;

    private Vector3 spawnLocation;
    private Plane plane;

    private int pointsMax = 100;
    private int points = 0;
    [SerializeField] private TextMeshProUGUI textMesh;

    private void Awake()
    {
        Instance = this;
        plane = new Plane(Vector3.up, Camera.main.transform.position.y);
    }

    private void Start()
    {
        GameManager.Builder.OnWorldRebuild.AddListener(OnWorldRebuild);
        textMesh.text = $"{points}/{pointsMax}";
    }

    public void SetSelectedPrefab(GameObject prefab) => toSpawn = prefab;

    public void SetCostOfNext(int nextCost) => costOfNext = nextCost;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float enter = 0.0f;

            if (plane.Raycast(ray, out enter))
            {
                Vector3 worldPos = ray.GetPoint(enter);
                worldPos += new Vector3(0.5f, 0.5f, 0.5f);
                if (worldPos.x >= 0 && worldPos.z >= 0 && worldPos.x < 7 && worldPos.z < 30)
                {
                    if (toSpawn != null)
                    {
                        GameManager.Builder.BuildObject(toSpawn, (int)worldPos.x, (int)worldPos.z);
                    }
                    else
                    {
                        GameManager.Builder.RemoveObject((int)worldPos.x, (int)worldPos.z);
                    }
                }
            }
        }
    }

    void UpdateCost()
    {
        textMesh.text = $"{points}/{pointsMax}";
    }

    void OnWorldRebuild()
    {
        points = 0;
        toSpawn = null;
        UpdateCost();
    }
}
