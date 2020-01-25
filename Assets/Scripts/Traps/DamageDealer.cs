using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        try 
        {
            Player player = other.GetComponent<Player>();
            player.Damage();
        }
        catch(System.Exception e)
        {
            //Debug.Log("Not a player.");
        }
    }
}
