using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonKeeper : MonoBehaviour
{
    private GameObject toSpawn;
    private Vector3 spawnLocation;

    [SerializeField] private Button batButton;

    private void Start()
    {
        batButton.onClick.AddListener(ButtonBat);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            //Debug.Log("Mouse down 2!");
            if (toSpawn != null)
            {
                var camera = Camera.main;
                var mousePos = Input.mousePosition;
                mousePos.z = camera.transform.position.z;
                Vector3 worldPos = camera.ScreenToWorldPoint(mousePos);
                //Debug.Log("Mouse pos = " + mousePos);
                //Debug.Log("World pos = " + worldPos);
                worldPos += new Vector3(0.5f, 0.5f, 0.5f);
                if (worldPos.x >= 0 && worldPos.z >= 0 && worldPos.x <= 11 && worldPos.z <= 40)
                {
                    GameManager.Builder.BuildObject(toSpawn, (int)worldPos.x, (int)worldPos.z);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        //Debug.Log("Mouse down!");
        var mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (worldPos.x >= 0 && worldPos.z >= 0 && worldPos.x <= 11 && worldPos.z < 50) 
        {
            GameManager.Builder.BuildObject(toSpawn, (int)worldPos.x, (int)worldPos.z);
        }
    }

    private void ButtonBat() 
    {
        toSpawn = GameManager.Builder.Prefabs[0].Prefab;
    }

}
