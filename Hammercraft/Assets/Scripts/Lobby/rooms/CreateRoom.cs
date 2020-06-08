using System;
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
    private TMP_Text _roomNamePlaceholder;
    [SerializeField]
    private TMP_Text ErrorName;
    private bool IsPrivate = false;
    [SerializeField]
    private GameObject _roomCanvas;
    [SerializeField]
    private TMP_Dropdown _dropDown;
    [SerializeField]
    private TMP_Text _selectedGameMode; //_selectedGameMode.text pour avoir le mode sélectionné

    public override void OnEnable()
    {
        _dropDown.ClearOptions();
        base.OnEnable();
        _roomNamePlaceholder.text = PhotonNetwork.NickName + "'s Game";
        FillDropDown();
    }

    private void FillDropDown() {
        _dropDown.AddOptions(new List<string>(Enum.GetNames(typeof(GameModes))));
    }
    public void CreateRoom_button()
    {
        if (_roomName.text.Length > 0 && PhotonNetwork.IsConnected)
        {
            print("creating room...");
            RoomOptions RoomOptions = new RoomOptions();
            RoomOptions.MaxPlayers = 2;
            RoomOptions.IsVisible = !IsPrivate;
            PhotonNetwork.CreateRoom(_roomName.text, RoomOptions, TypedLobby.Default);
        } else
        {
            return;
        }
    }

    public override void OnCreatedRoom()
    {
        print("Created room successfully");
    }

    public void on_click_change_private()
    {
        IsPrivate = !IsPrivate;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ErrorName.gameObject.SetActive(true);
        ErrorName.text = message;
    }

    public override void OnJoinedRoom()
    {
        print("Room joined succesfully");
        _roomCanvas.SetActive(true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ErrorName.gameObject.SetActive(true);
        ErrorName.text = message;
    }

}
