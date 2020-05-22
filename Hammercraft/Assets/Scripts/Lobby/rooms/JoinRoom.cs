using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private TMP_Text _errorMsg;

    public void on_click_join_room()
    {
        PhotonNetwork.JoinRoom(_name.text);
    }

    public override void OnJoinedRoom()
    {
        print("Room joined succesfully");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _errorMsg.gameObject.SetActive(true);
        _errorMsg.text = message;
    }
}
