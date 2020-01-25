using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float xPosition = 5;
    [SerializeField] float yPlayerOffset = 15;
    [SerializeField] float zPlayerOffset = -10;

    private void Update()
    {
        if (Net.IsServer) 
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.15f);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.15f);
            }
        }
        else 
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
}
