using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class testConnect : MonoBehaviourPunCallbacks
{
    
    // Start is called before the first frame update
    void Start()
    {
        print("connecting to server.");
        if (PlayerPrefs.HasKey("username")) {
            PhotonNetwork.NickName = PlayerPrefs.GetString("username");

        }
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("connected to server.");
        print(PhotonNetwork.LocalPlayer.NickName);

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server." + cause.ToString());
        
    }

}
