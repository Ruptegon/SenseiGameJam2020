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
    private float animationMovementTime => movementTime * 0.8f;
    private bool isMoving = false;

    public void ResetPlayer()
    {
        currentRotation = Rotation.Forward;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (isLocalPlayer)
        {
            ClientUIController.Instance.UpArrow.onClick.AddListener(MoveForwardLocal);
            ClientUIController.Instance.DownArrow.onClick.AddListener(MoveBackwardLocal);
            ClientUIController.Instance.LeftArrow.onClick.AddListener(LeftRotate);
            ClientUIController.Instance.RightArrow.onClick.AddListener(RightRotate);
        }
    }

    private void LeftRotate()
    {
        Rotation GetRotation()
        {
            switch (currentRotation)
            {
                case Rotation.Forward:
                    return Rotation.Left;
                case Rotation.Backward:
                    return Rotation.Right;
                case Rotation.Left:
                    return Rotation.Backward;
                case Rotation.Right:
                    return Rotation.Forward;
                default:
                    return Rotation.Right;
            }
        }
        CmdRotateTo((int)GetRotation());
    }

    private void RightRotate()
    {
        Rotation GetRotation()
        {
            switch (currentRotation)
            {
                case Rotation.Forward:
                    return Rotation.Right;
                case Rotation.Backward:
                    return Rotation.Left;
                case Rotation.Left:
                    return Rotation.Forward;
                case Rotation.Right:
                    return Rotation.Backward;
                default:
                    return Rotation.Right;
            }
        }
        CmdRotateTo((int)GetRotation());
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
        CmdMoveTo(xPosition + 1, zPosition);
    }

    private void MoveLeft()
    {
        if (isMoving)
            return;

        BlockMoving();

        var xPosition = (int)transform.position.x;
        var zPosition = (int)transform.position.z;
        CmdMoveTo(xPosition - 1, zPosition);
    }

    private void MoveBackward()
    {
        if (isMoving)
            return;

        BlockMoving();

        var xPosition = (int)transform.position.x;
        var zPosition = (int)transform.position.z;
        CmdMoveTo(xPosition, zPosition - 1);
    }

    private void MoveForward()
    {
        if (isMoving)
            return;

        BlockMoving();

        var xPosition = (int)transform.position.x;
        var zPosition = (int)transform.position.z;
        CmdMoveTo(xPosition, zPosition + 1);
    }

    private void MoveTo(int x, int z)
    {
        iTween.MoveTo(gameObject, new Vector3(x, 0f, z), animationMovementTime);
        //animator.speed = 1 / animationMovementTime;
        animator.SetTrigger("Jump");
    }

    [Command]
    private void CmdMoveTo(int x, int z)
    {
        if (!GameManager.World.IsPassable(x, z))
            return;

        MoveTo(x, z);
        RpcMoveTo(x, z);
    }

    [ClientRpc]
    private void RpcMoveTo(int x, int z)
    {
        MoveTo(x, z);
    }

    private void RotateTo(int rotation)
    {
        Vector3 GetRotation(Rotation rot)
        {
            switch (rot)
            {
                case Rotation.Forward:
                    return new Vector3(0f, 0f, 0f);
                case Rotation.Backward:
                    return new Vector3(0f, 180f, 0f);
                case Rotation.Left:
                    return new Vector3(0f, 270, 0f);
                case Rotation.Right:
                    return new Vector3(0f, 90, 0f);
                default:
                    return new Vector3(0f, 0f, 0f);
            }
        }

        currentRotation = (Rotation)rotation;
        iTween.RotateTo(gameObject, GetRotation(currentRotation), animationMovementTime);
    }

    [Command]
    private void CmdRotateTo(int rotation)
    {
        RotateTo(rotation);
        RpcRotateTo(rotation);
    }

    [ClientRpc]
    private void RpcRotateTo(int rotation)
    {
        RotateTo(rotation);
    }
}


