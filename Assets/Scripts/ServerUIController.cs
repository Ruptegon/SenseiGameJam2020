using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerUIController : MonoBehaviour
{
    public static int ServerScore = 0;

    [SerializeField] RightServerUI rightServerUI;

    private void Awake()
    {
        gameObject.SetActive(Net.IsServer);
        if(Net.IsServer)
        {
            rightServerUI = GetComponentInChildren<RightServerUI>();
        }
    }

    private void Update()
    {
        rightServerUI.PlayerCountText.text = $"Player Count: {Player.Instances.Count}";
        rightServerUI.ServerPlayerScore.text =  $"Your Score: {ServerScore}";
    }
}
