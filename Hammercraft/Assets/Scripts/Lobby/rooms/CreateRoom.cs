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
    private TMP_InputField _roomField = null;
    [SerializeField]
    private TMP_Text ErrorName = null;
    private bool IsPrivate = false;
    [SerializeField]
    private GameObject _roomCanvas = null;
    [SerializeField]
    private TMP_Dropdown _dropDown = null;
    [SerializeField]
    private TMP_Text _selectedGameMode = null; //_selectedGameMode.text pour avoir le mode sélectionné
    private GameModes mode;
    
    public override void OnEnable()
    {
        _dropDown.ClearOptions();
        base.OnEnable();
        _roomField.text = PhotonNetwork.NickName + "'s Game";
        FillDropDown();
    }

    private void FillDropDown() {
        _dropDown.AddOptions(new List<string>(Enum.GetNames(typeof(GameModes))));
    }
    public void CreateRoom_button()
    {
        
        string name = _roomField.text;
        if (name.Length > 0 && PhotonNetwork.IsConnected)
        {
            if (Enum.TryParse<GameModes>(_selectedGameMode.text, out mode)) {
                RoomOptions RoomOptions = new RoomOptions();
                RoomOptions.MaxPlayers = 2;
                RoomOptions.IsVisible = !IsPrivate;
                                
                PhotonNetwork.CreateRoom(name, RoomOptions, TypedLobby.Default);
            }
        }
        return;
    }

    public override void OnCreatedRoom()
    {
        PhotonNetwork.CurrentRoom.SetCustomProperties(
            new ExitGames.Client.Photon.Hashtable { {"GameMode", (int) mode}}
        );
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
        _roomCanvas.SetActive(true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ErrorName.gameObject.SetActive(true);
        ErrorName.text = message;
    }

}
