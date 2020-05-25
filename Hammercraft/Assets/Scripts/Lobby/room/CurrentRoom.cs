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
    
    [SerializeField]
    private ReadyRPC[] _readyRPC = new ReadyRPC[2];
    [SerializeField]
    private TMP_Text Text_Timer;
    private Coroutine _timer=null;
    

    public override void OnEnable()
    {
        base.OnEnable();
        _roomName.text = PhotonNetwork.CurrentRoom.Name;
        foreach(KeyValuePair<int,Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            _players[player.Key-1].text = player.Value.NickName;
            _readyRPC[player.Key - 1].gameObject.SetActive(true);
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            _players[player.Key - 1].text = player.Value.NickName;
            _readyRPC[player.Key - 1].gameObject.SetActive(true);
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        foreach (TMP_Text playerName in _players)
        {
            playerName.text = "Waiting for player...";
        }
        foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            _players[0].text = player.Value.NickName;
        }
        for (int i = 0; i < 2; i++)
        {
            if (_players[i].text == "Waiting for player...")
            {
                _readyRPC[i].gameObject.SetActive(false);
            }
            else
            {
                _readyRPC[i].gameObject.SetActive(true);
            }
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
        if(PhotonNetwork.LocalPlayer.NickName == _players[0].text)
        {
            _readyRPC[0].ChangeText((isReady) ? "Ready" : "Not Ready");

        } else if(PhotonNetwork.LocalPlayer.NickName == _players[1].text)
        {
            _readyRPC[1].ChangeText((isReady) ? "Ready" : "Not Ready");
        }
        
    }

    public void FixedUpdate()
    {       
        if (_readyRPC[0].Ready && _readyRPC[1].Ready && _timer==null)
        {
           
            Text_Timer.gameObject.SetActive(true);
            _timer = StartCoroutine(Timer());
        } 
        if((!_readyRPC[0].Ready || !_readyRPC[1].Ready) && _timer!=null)
        {
            StopCoroutine(_timer);
            _timer = null;
            Text_Timer.text = "Start in : 5";
            Text_Timer.gameObject.SetActive(false);
        }
    }

    private IEnumerator Timer()
    {
        string text = "Start in : ";
        for(int i=5; i >= 0; i--)
        {
            yield return new WaitForSecondsRealtime(1f);
            print("dans la boucle");
            Text_Timer.text = text + i.ToString();
            
        }
    }



}
