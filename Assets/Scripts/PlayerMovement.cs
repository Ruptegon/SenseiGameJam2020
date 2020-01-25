using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private Animation jumpAnimation;

    enum Rotation { Forward, Backward, Left, Right };
    private float movementTime = 1f;
    private bool isMoving = false;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (isLocalPlayer)
        {
            ClientUIController.Instance.UpArrow.onClick.AddListener(MoveForward);
            ClientUIController.Instance.DownArrow.onClick.AddListener(MoveBackward);
            ClientUIController.Instance.LeftArrow.onClick.AddListener(MoveLeft);
            ClientUIController.Instance.RightArrow.onClick.AddListener(MoveRight);
        }
    }

    private async void BlockMoving()
    {
        isMoving = true;
        await Task.Delay(TimeSpan.FromSeconds(movementTime));
        isMoving = false;
    }

    private void MoveRight()
    {
        if (isMoving)
            return;

        BlockMoving();

        var xPosition = (int)transform.position.x;
        var zPosition = (int)transform.position.z;
        CmdMoveTo(xPosition + 1, zPosition, (int)Rotation.Right);

    }

    private void MoveLeft()
    {
        if (isMoving)
            return;

        BlockMoving();

        var xPosition = (int)transform.position.x;
        var zPosition = (int)transform.position.z;
        CmdMoveTo(xPosition - 1, zPosition, (int)Rotation.Left);

    }

    private void MoveBackward()
    {
        if (isMoving)
            return;

        BlockMoving();

        var xPosition = (int)transform.position.x;
        var zPosition = (int)transform.position.z;
        CmdMoveTo(xPosition, zPosition - 1, (int)Rotation.Backward);

    }

    private void MoveForward()
    {
        if (isMoving)
            return;

        BlockMoving();

        var xPosition = (int)transform.position.x;
        var zPosition = (int)transform.position.z;
        CmdMoveTo(xPosition, zPosition + 1, (int)Rotation.Forward);

    }

    private void MoveTo(int x, int z, int rotation)
    {
        Vector3 GetRotation(Rotation rot)
        {
            switch (rot)
            {
                case Rotation.Forward:
                    return Vector3.forward;
                case Rotation.Backward:
                    return Vector3.back;
                case Rotation.Left:
                    return Vector3.left;
                case Rotation.Right:
                    return Vector3.right;
                default:
                    return Vector3.forward;
            }
        }

        iTween.MoveTo(gameObject, new Vector3(x, 0f, z), movementTime);
        iTween.RotateTo(gameObject, GetRotation((Rotation)rotation), movementTime);
        jumpAnimation.Play();
    }

    [Command]
    private void CmdMoveTo(int x, int z, int rotation)
    {
        MoveTo(x, z, rotation);
        RpcMoveTo(x, z, rotation);
    }

    [ClientRpc]
    private void RpcMoveTo(int x, int z, int rotation)
    {
        MoveTo(x, z, rotation);
    }
}


