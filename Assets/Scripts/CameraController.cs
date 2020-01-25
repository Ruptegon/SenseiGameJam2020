﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float xPosition = 5;
    [SerializeField] float yPlayerOffset = 15;
    [SerializeField] float zPlayerOffset = -10;

    private void Update()
    {
        if (!Player.LocalPlayer)
        {
            return;
        }

        var playerTransform = Player.LocalPlayer.transform;
        var playerPosition = playerTransform.position;

        transform.position = new Vector3(xPosition, playerPosition.y + yPlayerOffset, playerPosition.z + zPlayerOffset);
        transform.LookAt(new Vector3(xPosition, playerPosition.y, playerPosition.z), Vector3.up);
    }
}
