using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerUIController : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(Net.IsServer);
    }
}
