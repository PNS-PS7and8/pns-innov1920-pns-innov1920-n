using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Canvas mainCanvas;
    [SerializeField]
    private Canvas roomCanvas;

    public void on_click_leave_room()
    {
        PhotonNetwork.LeaveRoom();
        mainCanvas.gameObject.SetActive(true);
        roomCanvas.gameObject.SetActive(false);
    }
}
