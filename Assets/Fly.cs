using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private float time = 0;
    private float previousTime = 0;
    private float lifeTime = 0;

    // Update is called once per frame

    private void Start()
    {
        previousTime = (float)GameManager.SyncTimer;
    }

    void Update()
    {
        time = ((float)GameManager.SyncTimer - previousTime);
        lifeTime += time;
        Debug.Log("syncTime = " + (float)GameManager.SyncTimer);
        Debug.Log("time = " + time);
        Debug.Log("lifeTime = " + lifeTime);
        transform.position += transform.forward * time;
        previousTime = (float)GameManager.SyncTimer;
        if (lifeTime >= 20) 
        {
            Destroy(gameObject);
        }
    }
}
