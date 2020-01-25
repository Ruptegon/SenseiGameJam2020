using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtlePatrol : MonoBehaviour
{
    [SerializeField] AnimationCurve positionYCurve; //jumps!
    [SerializeField] AnimationCurve rotation; //0 - left or 1 - right
    float lastTime = 0;

    private void Start()
    {
        rotation.postWrapMode = WrapMode.Loop;
        positionYCurve.postWrapMode = WrapMode.Loop;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += rotation.Evaluate((float)GameManager.SyncTimer) * transform.forward * ((float)GameManager.SyncTimer - lastTime);
        transform.position = new Vector3(transform.position.x, positionYCurve.Evaluate((float)GameManager.SyncTimer), transform.position.z);
        lastTime = (float)GameManager.SyncTimer;
    }
}
