using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitFire : MonoBehaviour
{
    [SerializeField] GameObject fireball;
    private float delay = 4;
    private float time = 0;
    private float previousTime = 0;

    // Update is called once per frame
    void Update()
    {
        time += ((float)GameManager.SyncTimer - previousTime);
        if (time >= delay) 
        {
            time = 0;
            Instantiate<GameObject>(fireball, this.transform.position + this.transform.forward, Quaternion.identity);
        }
        previousTime = (float)GameManager.SyncTimer;
    }
}
