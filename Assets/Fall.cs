using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [SerializeField] AnimationCurve fallDown;
    private Player victim;
    bool playerIsFalling;

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        victim = other.GetComponent<Player>();
        if (!victim || !victim.isLocalPlayer)
            return;

        victim?.Kill();
        playerIsFalling = true;
    }

    private void Update()
    {
        if (playerIsFalling) 
        {
            var time = GameManager.SyncTimer;
            float value = fallDown.Evaluate(time);
            victim.transform.localScale = new Vector3(value, value, value);
            if (value < 0.1) 
            {
                playerIsFalling = false;
            }
        }
    }
}
