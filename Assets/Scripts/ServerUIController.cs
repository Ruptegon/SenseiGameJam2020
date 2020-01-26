using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ServerUIController : MonoBehaviour
{
    public static ServerUIController instance;

    [SerializeField] RightServerUI rightServerUI;

    public List<(Player, bool)> playersWhoFinished = new List<(Player, bool)>();

    public void ResetMatch()
    {
        Debug.Log("playerWhoFinished.Clear()");
        playersWhoFinished.Clear();
    }

    public void AddPlayerWhoFinished(Player player, bool wasAlive)
    {
        if (playersWhoFinished.Contains((player, true)) || playersWhoFinished.Contains((player, false)))
            return;

        playersWhoFinished.Add((player, wasAlive));
    }

    private void Awake()
    {
        instance = this;

        gameObject.SetActive(Net.IsServer);
        if(Net.IsServer)
        {
            rightServerUI = GetComponentInChildren<RightServerUI>();
            rightServerUI.PlayButton.onClick.AddListener(OnServerStartClick);
            SetCorrectNameOfServerStartButton();
        }
    }

    private void Update()
    {
        rightServerUI.PlayerCountText.text = $"Player Count: {Player.Instances.Count}";
        rightServerUI.ServerPlayerScore.text =  $"Your Score: {ChestScore.ServerScore}";
    }

    private void OnServerStartClick()
    {
        if(GameManager.GameStatus == GameStatusData.GameStatus.BuildAndConnect)
        {
            GameManager.Instance.StartGameplayRunStage();
            SetCorrectNameOfServerStartButton();
        }
        else
        {
            //use only if some error
            GameManager.Instance.StartBuildAndConnectStage();
            SetCorrectNameOfServerStartButton();
        }
    }

    private void SetCorrectNameOfServerStartButton()
    {
        switch (GameManager.GameStatus)
        {
            case GameStatusData.GameStatus.BuildAndConnect:
                rightServerUI.PlayButton.GetComponentInChildren<TMP_Text>().text = "Start Match";
                break;
            case GameStatusData.GameStatus.GameplayRun:
                rightServerUI.PlayButton.GetComponentInChildren<TMP_Text>().text = "((Runing...))";
                break;
            default:
                break;
        }
    }
}
