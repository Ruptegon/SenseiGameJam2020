using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InitUIController : MonoBehaviour
{
    [SerializeField] TMP_InputField ipAddressInputField;
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] Button playButton;

    private void Awake()
    {
        if (Net.IsClient)
        {
            playButton.onClick.AddListener(Play);
        }
    }

    private void Play()
    {
        var address = ipAddressInputField.text;
        var playerName = playerNameInputField.text;

        if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(playerName))
            return;

        Net.instance.PlayGame(address);
        Player.LocalPlayerName = playerName;
    }
}
