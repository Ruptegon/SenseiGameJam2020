using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour
{
    [SerializeField] GameObject UpperPart;
    [SerializeField] AnimationCurve yPositionOfTime;

    public enum ChestType { none, Chest, Mimic };
    [SerializeField] ChestType type = ChestType.none;
    [SerializeField] int scoreToAdd = 16;

    bool goOpen = false;
    float startOpenTime;

    private void Update()
    {
        if (goOpen)
        {
            float y = yPositionOfTime.Evaluate(startOpenTime + GameManager.SyncTimer);
            UpperPart.transform.rotation = Quaternion.Euler(y, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player || !player.isLocalPlayer)
            return;

        startOpenTime = GameManager.SyncTimer;
        goOpen = true;

        if (type == ChestType.Chest)
        {
            player.Score += scoreToAdd;
            scoreToAdd = 0;
        }

        if (type == ChestType.Mimic)
            player.Damage();
    }
}
