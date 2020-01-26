using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtlePatrol : MonoBehaviour
{
    [SerializeField] AnimationCurve positionX;
    [SerializeField] AnimationCurve positionZ;
    [SerializeField] AnimationCurve positionY;
    [SerializeField] AnimationCurve rotationY;

    Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        var time = GameManager.SyncTimer;
        transform.rotation = Quaternion.Euler(0f, rotationY.Evaluate(time), 0f);
        transform.position = startPosition + new Vector3(positionX.Evaluate(time), positionY.Evaluate(time), positionZ.Evaluate(time));
    }
}
