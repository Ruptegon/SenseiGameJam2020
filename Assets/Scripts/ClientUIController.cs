using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] private TextMeshProUGUI ranking;

    public static float HorizontalInput;
    public static float VertiaclInput;

    private void Awake()
    {
        Instance = this;
        
        gameObject.SetActive(Net.IsClient);
    }

    private void Update()
    {
        if (waitingRoomCanvas && GameManager.GameStatus == GameStatusData.GameStatus.BuildAndConnect) 
        {
            return;
        }
        else if (waitingRoomCanvas && GameManager.GameStatus == GameStatusData.GameStatus.GameplayRun)
        {
            waitingRoomCanvas.SetActive(false);
        }
        else 
        {
            if (!Player.LocalPlayer)
            {
                return;
            }
            Gold.text = Player.LocalPlayer.Gold.ToString();
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
            if (GameManager.GameStatus == GameStatusData.GameStatus.BuildAndConnect) 
            {
                waitingRoomCanvas.SetActive(true);
            }
        }
    }
}
