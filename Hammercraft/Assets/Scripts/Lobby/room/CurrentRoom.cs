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
    private TMP_Text _OtherPlayer;
    [SerializeField]
    private TMP_Text Text_Timer;
    private Coroutine _timer=null;
    private Coroutine _dots = null;
    [SerializeField]
    private RawImage StrangeNebula;
    [SerializeField]
    private TMP_Text ThreeDots;
    

    public override void OnEnable()
    {
        base.OnEnable();
        _dots = StartCoroutine(Dots());
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                if(player.Value.NickName != PhotonNetwork.NickName)
                    _OtherPlayer.text = player.Value.NickName;
                    ThreeDots.gameObject.SetActive(false);
            }
        } 
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            print("MES GROU");
            if (player.Value.NickName != PhotonNetwork.NickName)
                _OtherPlayer.text = player.Value.NickName;
                ThreeDots.gameObject.SetActive(false);
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        _OtherPlayer.text = "Waiting for player";

        ThreeDots.gameObject.SetActive(true);
    }
        
    public void FixedUpdate()
    {
        //print(PhotonNetwork.CurrentRoom.Players.Count.ToString());
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2 && _timer==null)
        {
             
            Text_Timer.gameObject.SetActive(true);
            _timer = StartCoroutine(Timer());
            StrangeNebula.gameObject.SetActive(false);
        } 
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2 && _timer != null)
        {
            StopCoroutine(_timer);
            _timer = null;
            Text_Timer.text = "5";
            Text_Timer.gameObject.SetActive(false);
            StrangeNebula.gameObject.SetActive(true);
        }
        StrangeNebula.transform.Rotate(new Vector3(0, 0, -2));
    }

    private IEnumerator Timer()
    {
        for(int i=5; i >= 0; i--)
        {
            
            Text_Timer.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
            
        }
    }

    private IEnumerator Dots()
    {
        while (true)
        {
            if(ThreeDots.text.Equals("..."))
            {
                ThreeDots.text = "";
            } else
            {
                ThreeDots.text = ThreeDots.text + ".";
            }
            yield return new WaitForSecondsRealtime(0.7f);

        }
    }



}
