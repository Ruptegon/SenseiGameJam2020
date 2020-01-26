using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player || !player.isLocalPlayer)
            return;

        player?.Damage();
    }
}
