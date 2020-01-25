using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerUIController : MonoBehaviour
{
    [SerializeField] RightServerUI rightServerUI;

    private void Awake()
    {
        gameObject.SetActive(Net.IsServer);
        if(Net.IsServer)
        {
            rightServerUI = GetComponentInChildren<RightServerUI>();
        }
    }


}
