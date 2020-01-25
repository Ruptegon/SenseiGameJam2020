using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpikes : MonoBehaviour
{

    public GameObject spikes;
    public float delayTime;
    private bool isUp;
    public float height;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        isUp = false;
        InvokeRepeating("SpikesMoving", delayTime, timer);
    }

    void SpikesMoving()
    {
        if(!isUp)
        {
            iTween.MoveBy(spikes, new Vector3(0, height, 0), 1);
            isUp = true;
        }
        else if (isUp)
        {
            iTween.MoveBy(spikes, new Vector3(0, -height, 0), 1);
            isUp = false;
        }
    }

}
