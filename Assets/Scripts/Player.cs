using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Player : NetworkBehaviour
{
    public static Player LocalPlayer;

    [Header("HP")]
    private int hp = 3;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (isLocalPlayer)
            LocalPlayer = this;

    }

    public void Damage() 
    {
        hp -= 1;
    }

    public void Heal() 
    {
        hp += 1;
    }

    public void Restore() 
    {
        hp = 3;
    }

    public void Kill() 
    {
        hp = 0;
    }

    public void Update()
    {
        if (hp <= 0) 
        {
            RoundOver();
        }
    }

    public void RoundOver() 
    {
    
    }

    /*void FixedUpdate()
    {
        if (!isLocalPlayer || characterController == null) return;

        var horizontal = ClientUIController.HorizontalInput;
        var vertical = ClientUIController.VertiaclInput;

        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        direction = Vector3.ClampMagnitude(direction, 1f);
        direction = transform.TransformDirection(direction);
        direction *= moveSpeed;

        characterController.SimpleMove(direction);
    }*/
}
