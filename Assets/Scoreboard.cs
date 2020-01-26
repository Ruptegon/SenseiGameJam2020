using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scores;

    private int playersCount = 0;

    private void PrepareRanking()
    {
        scores.text = "";
        var players = Player.Instances;

        //Debug.Log("players count = " + Player.Instances.Count);

        for (int i = 0; i < players.Count; i++)
        {
            for (int j = 0; j < players.Count; j++)
            {
                if (players[i].Gold < players[j].Gold)
                {
                    Player save = players[i];
                    players[i] = players[j];
                    players[j] = save;
                }
            }
        }

        for (int i = 0; i < players.Count; i++)
        {
            scores.text += i + ". " + players[i].PlayerName + " - " + players[i].Gold + " PKT.\n";
        }
    }

    public void Update()
    {
        if (playersCount != Player.Instances.Count) 
        {
            playersCount = Player.Instances.Count;
            PrepareRanking();
        }
    }
}
