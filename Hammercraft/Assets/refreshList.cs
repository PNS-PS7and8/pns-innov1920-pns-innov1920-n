using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refreshList : MonoBehaviour
{
    public void on_click_refresh_list()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
}
