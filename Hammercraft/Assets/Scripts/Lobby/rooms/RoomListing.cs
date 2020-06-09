using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _name = null;
    [SerializeField]
    private TMP_Text _CountPlayers = null;
    [SerializeField]
    private TMP_Text _gameFull = null;


    public RoomInfo RoomInfo { get; private set; }
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _name.text = roomInfo.Name;
        _CountPlayers.text = RoomInfo.PlayerCount.ToString() + "/2";
        _gameFull.gameObject.SetActive(RoomInfo.PlayerCount == 2);
        
    }

    public void on_click_join_room()
    {
        
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
