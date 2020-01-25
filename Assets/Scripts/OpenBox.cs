using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour
{
    [SerializeField] GameObject UpperPart;
    [SerializeField] float delayTime;
    [SerializeField] AnimationCurve yPositionOfTime;

    private void Update()
    {
        float y = yPositionOfTime.Evaluate(delayTime + (float)GameManager.SyncTimer);
        UpperPart.transform.rotation = Quaternion.Euler(y, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
