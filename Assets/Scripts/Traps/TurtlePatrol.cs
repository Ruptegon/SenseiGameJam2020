using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtlePatrol : MonoBehaviour
{
    [SerializeField] Transform character;

    [SerializeField] AnimationCurve positionX;
    [SerializeField] AnimationCurve positionY;
    [SerializeField] AnimationCurve positionZ;
    [SerializeField] AnimationCurve rotationY;

    Vector3 startPosition;

    void Update()
    {
        var time = GameManager.SyncTimer;
        character.rotation = Quaternion.Euler(0f, rotationY.Evaluate(time), 0f);
        character.localPosition = new Vector3(positionX.Evaluate(time), positionY.Evaluate(time), positionZ.Evaluate(time));
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player || !player.isLocalPlayer)
            return;

        player?.Damage();
    }
}
