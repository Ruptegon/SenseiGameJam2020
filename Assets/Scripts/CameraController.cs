using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float xPosition = 3;
    float yPosition = 9;
    float movementSpeed = 10f;
    float xRotation = 80f;

    private void Start()
    {
        transform.position = new Vector3(xPosition, yPosition, transform.position.z);
        transform.rotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void Update()
    {
        if (Net.IsClient)
        {
            var cameraTarget = Player.LocalPlayer?.CameraTarget;

            if (cameraTarget)
            {
                transform.position = cameraTarget.position;
                transform.rotation = cameraTarget.rotation;
            }
        }
        else if (Net.IsServer)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.position = new Vector3(xPosition, yPosition, transform.position.z + movementSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.position = new Vector3(xPosition, yPosition, transform.position.z - movementSpeed * Time.deltaTime);
            }

            if (GameManager.CameraXMovement)
            {
                xPosition = transform.position.x;
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    transform.position = new Vector3(xPosition + movementSpeed * Time.deltaTime, yPosition, transform.position.z);
                }
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.position = new Vector3(xPosition - movementSpeed * Time.deltaTime, yPosition, transform.position.z);
                }
            }
        }

    }
}
