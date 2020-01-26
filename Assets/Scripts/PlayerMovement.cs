using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private Animator animator;

    enum Rotation { Forward, Backward, Left, Right };
    private Rotation currentRotation = Rotation.Forward;
    private float movementTime = 0.2f;
    private bool isMoving = false;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (isLocalPlayer)
        {
            ClientUIController.Instance.UpArrow.onClick.AddListener(MoveForwardLocal);
            ClientUIController.Instance.DownArrow.onClick.AddListener(MoveBackwardLocal);
            ClientUIController.Instance.LeftArrow.onClick.AddListener(MoveLeftLocal);
            ClientUIController.Instance.RightArrow.onClick.AddListener(MoveRightLocal);
        }
    }

    private void MoveRightLocal()
    {
        switch (currentRotation)
        {
            case Rotation.Forward:
                MoveRight();
                break;
            case Rotation.Backward:
                MoveLeft();
                break;
            case Rotation.Left:
                MoveForward();
                break;
            case Rotation.Right:
                MoveBackward();
                break;
            default:
                break;
        }
    }

    private void MoveLeftLocal()
    {
        switch (currentRotation)
        {
            case Rotation.Forward:
                MoveLeft();
                break;
            case Rotation.Backward:
                MoveRight();
                break;
            case Rotation.Left:
                MoveForward();
                break;
            case Rotation.Right:
                MoveBackward();
                break;
            default:
                break;
        }
    }

    private void MoveBackwardLocal()
    {
        switch (currentRotation)
        {
            case Rotation.Forward:
                MoveBackward();
                break;
            case Rotation.Backward:
                MoveForward();
                break;
            case Rotation.Left:
                MoveRight();
                break;
            case Rotation.Right:
                MoveLeft();
                break;
            default:
                break;
        }
    }

    private void MoveForwardLocal()
    {
        switch (currentRotation)
        {
            case Rotation.Forward:
                MoveForward();
                break;
            case Rotation.Backward:
                MoveBackward();
                break;
            case Rotation.Left:
                MoveLeft();
                break;
            case Rotation.Right:
                MoveRight();
                break;
            default:
                break;
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
                    return new Vector3(0f, 0f, 0f);
                case Rotation.Backward:
                    return new Vector3(0f, 90f, 0f);
                case Rotation.Left:
                    return new Vector3(0f, 180f, 0f);
                case Rotation.Right:
                    return new Vector3(0f, 270f, 0f);
                default:
                    return new Vector3(0f, 0f, 0f);
            }
        }

        currentRotation = (Rotation)rotation;
        var animationMovementTime = movementTime * 0.8f;

        iTween.MoveTo(gameObject, new Vector3(x, 0f, z), animationMovementTime);
        iTween.RotateTo(gameObject, GetRotation(currentRotation), animationMovementTime);
        animator.speed = 1 / animationMovementTime;
        animator.SetTrigger("Jump");

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


