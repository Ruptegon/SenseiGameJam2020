using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpikes : MonoBehaviour
{
    [SerializeField] GameObject spikes;
    [SerializeField] float delayTime;
    [SerializeField] AnimationCurve yPositionOfTime;

    private void Update()
    {
        float y = yPositionOfTime.Evaluate(delayTime + (float)GameManager.SyncTimer);
        spikes.transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

}
