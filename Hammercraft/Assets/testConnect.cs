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
        if (PlayerPrefs.HasKey("username")) {
            PhotonNetwork.NickName = PlayerPrefs.GetString("username");

        }
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print(PhotonNetwork.LocalPlayer.NickName);

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
}
