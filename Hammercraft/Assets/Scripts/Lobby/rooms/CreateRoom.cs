using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text _roomName;
    [SerializeField]
    private TMP_Text ErrorName;
    [SerializeField]
    private Toggle IsPrivate;

    public void CreateRoom_button()
    {
        if (_roomName.text.Length > 0 && PhotonNetwork.IsConnected)
        {
            print("creating room...");
            RoomOptions RoomOptions = new RoomOptions();
            RoomOptions.MaxPlayers = 2;
            RoomOptions.IsVisible = !IsPrivate.enabled;
            PhotonNetwork.JoinOrCreateRoom(_roomName.text, RoomOptions, TypedLobby.Default);
        } else
        {
            return;
        }
    }

    public override void OnCreatedRoom()
    {
        print("Created room successfully");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ErrorName.gameObject.SetActive(true);
        ErrorName.text = message;
    }

}
