using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Player : NetworkBehaviour
{
    public static string LocalPlayerName = "default";
    public static Player LocalPlayer;

    public string PlayerName;

    public int HP = 3;
    public int Gold = 0;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (isLocalPlayer)
        {
            LocalPlayer = this;
            CmdSetPlayerName(LocalPlayerName);
        }
    }

    [Command]
    public void CmdSetPlayerName(string name)
    {
        PlayerName = name;
        RpcSetPlayerName(name);
    }

    [ClientRpc]
    public void RpcSetPlayerName(string name)
    {
        PlayerName = name;
    }

    public void Damage() => HP--;
    public void Heal() => HP++;
    public void Restore() => HP = 3;
    public void Kill() => HP = 0;


    public void Update()
    {
        if (HP <= 0)
        {
            RoundOver();
        }
    }

    public void RoundOver()
    {

    }


}
