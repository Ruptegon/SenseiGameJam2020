using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpikes : MonoBehaviour
{

    public GameObject spikes;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpikesMoving", 2.0f, 0.1f);
    }

    void SpikesMoving()
    {
        iTween.MoveBy(spikes, new Vector3(0, 5, 0), 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
