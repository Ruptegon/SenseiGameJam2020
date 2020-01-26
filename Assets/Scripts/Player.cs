using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;

public class Player : NetworkBehaviour
{
    public static List<Player> Instances = new List<Player>();

    public static string LocalPlayerName = "default";

    public static Player LocalPlayer;

    [SerializeField] Transform cameraTarget;
    public Transform CameraTarget => cameraTarget;

    public string PlayerName;

    private int score = 0;
    public int Gold
    {
        get => score;
        set
        {
            if (Net.IsServer)
            {
                score = value;
                RpcChangeScoreValue(value);
            }
            else if (isLocalPlayer)
            {
                CmdChangeScoreValue(value);
            }
        }
    }

    private int hp = 3;
    public int HP
    {
        get => hp;
        set
        {
            hp = value;
            if (hp < 0)
                OnDeath();
        }
    }
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

    [Command]
    public void CmdChangeScoreValue(int value)
    {
        score = value;
        RpcChangeScoreValue(value);
    }

    [ClientRpc]
    public void RpcChangeScoreValue(int value)
    {
        score = value;
    }

    public void Damage() => HP--;
    public void Heal() => HP++;
    public void Kill() => HP = 0;

    public void OnDeath()
    {
        if (isLocalPlayer)
            CmdSendEndCommunicat(false);
    }

    private void OnEnable()
    {
        Instances.Add(this);
    }

    private void OnDisable()
    {
        Instances.Remove(this);
    }

    public void ResetPlayer()
    {
        var xPosition = (int)(GameManager.World.SizeX / 2f);
        transform.position = new Vector3(xPosition, 0f, 0f);
        HP = 3;
    }

    [Command]
    void CmdSendEndCommunicat(bool isAlive)
    {
        ServerUIController.instance.AddPlayerWhoFinished(this, isAlive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Net.IsServer || !isLocalPlayer)
            return;

        if(other.tag == "GoalChest")
        {
            ClientUIController.Instance.OnGoalChestAchieved();
            CmdSendEndCommunicat(true);
        }
    }
}
