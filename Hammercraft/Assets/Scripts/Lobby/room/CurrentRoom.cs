using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text[] _players = new TMP_Text[2];
    [SerializeField]
    private TMP_Text _roomName;
    [SerializeField]
    private Button ReadyUpButton;
    [SerializeField]
    private TMP_Text ReadyUpText;
    private bool isReady = false;

    public override void OnEnable()
    {
        base.OnEnable();
        _roomName.text = PhotonNetwork.CurrentRoom.Name;
        foreach(KeyValuePair<int,Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            _players[player.Key-1].text = player.Value.NickName;
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            _players[player.Key - 1].text = player.Value.NickName;
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        foreach(TMP_Text playerName in _players)
        {
            playerName.text = "Waiting for players...";
        }
        foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            _players[player.Key - 1].text = player.Value.NickName;
        }
    }

    public void on_click_ready_up()
    {
        var colors = ReadyUpButton.colors;
        colors.normalColor = (!isReady) ? new Color(206, 0,0) : new Color(255, 192, 0);
        colors.pressedColor = (!isReady) ? new Color(107, 0, 10) : new Color(217,162, 61);
        colors.selectedColor = (!isReady) ? new Color(206,0, 0) : new Color(255, 192, 0);
        colors.highlightedColor = (!isReady) ? new Color(107, 0,10) : new Color(217,162,61);
        ReadyUpButton.colors = colors;
        ReadyUpText.text = (!isReady) ? "Unready" : "Ready";
        isReady = !isReady;
    }

}
