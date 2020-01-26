using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ClientUIController : MonoBehaviour
{

    public static ClientUIController Instance;

    [SerializeField] public Button UpArrow;
    [SerializeField] public Button DownArrow;
    [SerializeField] public Button RightArrow;
    [SerializeField] public Button LeftArrow;
    [SerializeField] private TextMeshProUGUI Gold;
    [SerializeField] private RawImage heart1;
    [SerializeField] private RawImage heart2;
    [SerializeField] private RawImage heart3;

    [SerializeField] private GameObject waitingRoomCanvas;
    [SerializeField] private GameObject waitingForFinishCanvas;

    [SerializeField] private TextMeshProUGUI ranking;
    [SerializeField] private TextMeshProUGUI placement;

    public static float HorizontalInput;
    public static float VertiaclInput;

    private bool impossibleRunning = false;

    private void Awake()
    {
        Instance = this;

        gameObject.SetActive(Net.IsClient);
    }

    private void Update()
    {
        if (impossibleRunning && GameManager.GameStatus == GameStatusData.GameStatus.GameplayRun)
        {
            waitingForFinishCanvas.SetActive(true);
            placement.text = "";
            var playersWhoFinished = Player.PlayersWhoFinished;
            for (int i = 0; i < playersWhoFinished.Count; i++)
            {
                var player = playersWhoFinished[i].Item1;
                var isAlive = playersWhoFinished[i].Item2 == true ? "" : " [+]";
                placement.text += i + ". " + player.PlayerName + " - " + player.Score + " Pnts." + isAlive + "\n";
            }
        }
        else
        {
            if (waitingRoomCanvas.activeSelf && GameManager.GameStatus == GameStatusData.GameStatus.BuildAndConnect)
            {
                waitingForFinishCanvas.SetActive(false);
                impossibleRunning = false;
                return;
            }
            else if (waitingRoomCanvas.activeSelf && GameManager.GameStatus == GameStatusData.GameStatus.GameplayRun)
            {
                waitingRoomCanvas.SetActive(false);
            }
            else
            {
                if (GameManager.GameStatus == GameStatusData.GameStatus.BuildAndConnect)
                {
                    waitingRoomCanvas.SetActive(true);
                }
                if (Player.LocalPlayer)
                {
                    Gold.text = Player.LocalPlayer.Score.ToString();
                    if (Player.LocalPlayer.HP < 3)
                    {
                        heart1.enabled = false;
                        if (Player.LocalPlayer.HP < 2)
                        {
                            heart2.enabled = false;
                            if (Player.LocalPlayer.HP < 1)
                            {
                                heart3.enabled = false;
                            }
                        }
                    }
                }
            }
        }
    }


    public void OnRuningEnd()
    {
        //TODO: Kacper, ogarnij... jak dojdziemy do końca skrzyni.
        impossibleRunning = true;
        Debug.Log("onGoalActive!");
    }
}
